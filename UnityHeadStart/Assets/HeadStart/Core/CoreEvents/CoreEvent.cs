using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Assets.HeadStart.Core
{
    public class CoreEvent
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.0.8";
#pragma warning restore 0414 //
        private object s_stationLock = new object();
        private object s_intervalLock = new object();
        private Dictionary<Evt, List<EvtPackage>> e_station = new Dictionary<Evt, List<EvtPackage>>();
        private Dictionary<string, List<EvtPackage>> s_station = new Dictionary<string, List<EvtPackage>>();
        private List<IDisposable> s_intervals = new List<IDisposable>();

        public CoreEvent() { }

        public EvtPackage On(Evt eventId, Action handler)
        {
            return OnEmpty(eventId, handler, Scheduler.MainThread);
        }

        public EvtPackage On(Evt eventId, Action<bool> handler)
        {
            return On(eventId, handler, Scheduler.MainThread);
        }

        public EvtPackage On(Evt eventId, Action<object> handler)
        {
            return On(eventId, handler, Scheduler.MainThread);
        }

        public EvtPackage On(Evt eventId, Action<bool> handler, IScheduler runOn)
        {
            // create new slot for this bus
            var newEvtPackage = new EvtPackage
            {
                evt = eventId
            };
            newEvtPackage.disposable = newEvtPackage.observableBool
                .ObserveOn(runOn)
                .Subscribe(obj => handler(obj));

            lock (e_station)
            {
                // check if we already add this bus
                if (e_station.ContainsKey(eventId))
                {
                    e_station[eventId].Add(newEvtPackage);
                }
                else
                {
                    e_station[eventId] = new List<EvtPackage>() { newEvtPackage };
                }
            }
            return newEvtPackage;
        }

        public EvtPackage On(Evt eventId, Action<object> handler, IScheduler runOn)
        {
            // create new slot for this bus
            var newEvtPackage = new EvtPackage
            {
                evt = eventId
            };
            newEvtPackage.disposable = newEvtPackage.observable
                .ObserveOn(runOn)
                .Subscribe(obj => handler(obj));

            lock (e_station)
            {
                // check if we already add this bus
                if (e_station.ContainsKey(eventId))
                {
                    e_station[eventId].Add(newEvtPackage);
                }
                else
                {
                    e_station[eventId] = new List<EvtPackage>() { newEvtPackage };
                }
            }
            return newEvtPackage;
        }

        private EvtPackage OnEmpty(Evt eventId, Action emptyHandler, IScheduler runOn)
        {
            // create new slot for this bus
            var newEvtPackage = new EvtPackage
            {
                evt = eventId
            };
            newEvtPackage.disposable = newEvtPackage.observable
                .ObserveOn(runOn)
                .Subscribe(obj => emptyHandler());

            lock (e_station)
            {
                // check if we already add this bus
                if (e_station.ContainsKey(eventId))
                {
                    e_station[eventId].Add(newEvtPackage);
                }
                else
                {
                    e_station[eventId] = new List<EvtPackage>() { newEvtPackage };
                }
            }
            return newEvtPackage;
        }

        public void Register(object evtPackage, string eventId, Action<object, object> handler)
        {
            Register(evtPackage, eventId, handler, Scheduler.MainThread);
        }

        public void Register(object evtPackage, string eventId, Action<object, object> handler, IScheduler runOn)
        {
            // create new slot for this bus
            var newEvtPackage = new EvtPackage
            {
                id = evtPackage.GetType().FullName,
                busId = eventId
            };
            newEvtPackage.disposable = newEvtPackage.observable
                .ObserveOn(runOn)
                .Subscribe(obj => handler(evtPackage, obj));

            lock (s_stationLock)
            {
                // check if we already add this bus
                if (s_station.ContainsKey(eventId))
                {
                    var evtPackages = s_station[eventId];
                    var shouldAdd = true;
                    foreach (var op in evtPackages)
                    {
                        // there is no glich on matrix that 2 version of evtPackage on bus
                        // at same time
                        if (op.id == newEvtPackage.id)
                        {
                            shouldAdd = false;
                            break;
                        }
                    }
                    if (shouldAdd)
                    {
                        evtPackages.Add(newEvtPackage);
                    }
                }
                else
                {
                    s_station[eventId] = new List<EvtPackage>() { newEvtPackage };
                }
            }
        }

        public void Unregister(Evt eventId)
        {
            lock (s_stationLock)
            {
                if (e_station.ContainsKey(eventId))
                {
                    List<EvtPackage> evtPackages = e_station[eventId];
                    evtPackages.ForEach(p => p.disposable?.Dispose());
                    evtPackages.Clear();
                    e_station.Remove(eventId);
                }
                else
                {
                    Debug.LogWarning("[Event Bus] try to unregister event id [" + eventId + "] but not found");
                }
            }
        }

        public void Unregister(Evt eventId, Action handler)
        {
            lock (s_stationLock)
            {
                if (e_station.ContainsKey(eventId))
                {
                    List<EvtPackage> evtPackages = e_station[eventId];
                    evtPackages.ForEach(p => p.disposable?.Dispose());
                    evtPackages.Clear();
                    e_station.Remove(eventId);
                }
                else
                {
                    Debug.LogWarning("[Event Bus] try to unregister event id [" + eventId + "] but not found");
                }
            }
        }

        public void Unregister(string eventId)
        {
            lock (s_stationLock)
            {
                if (s_station.ContainsKey(eventId))
                {
                    var evtPackages = s_station[eventId];
                    evtPackages.ForEach(p => p.disposable?.Dispose());
                    evtPackages.Clear();
                    s_station.Remove(eventId);
                }
                else
                {
                    Debug.LogWarning("[Event Bus] try to unregister event id [" + eventId + "] but not found");
                }
            }
        }

        public void Unregister(object evtPackage)
        {
            lock (s_stationLock)
            {
                foreach (var eventId in s_station.Keys)
                {
                    var evtPackages = s_station[eventId];
                    foreach (var p in evtPackages)
                    {
                        if (p.id == evtPackage.GetType().FullName)
                        {
                            p.disposable?.Dispose();
                            evtPackages.Remove(p);
                            break;
                        }
                    }
                    if (evtPackages.Count == 0)
                    {
                        s_station.Remove(eventId);
                        break;
                    }
                }
            }
        }

        public void UnregisterAll()
        {
            lock (s_stationLock)
            {
                foreach (var eventId in s_station.Keys)
                {
                    var evtPackages = s_station[eventId];
                    foreach (var p in evtPackages)
                    {
                        p.disposable?.Dispose();
                    }
                    evtPackages.Clear();
                }
                s_station.Clear();
            }
        }

        public bool Emit(Evt eventId, bool data)
        {
            lock (e_station)
            {
                if (e_station.ContainsKey(eventId))
                {
                    var evtPackages = e_station[eventId];
                    evtPackages.ForEach(p => p.subjectBool.OnNext(data));
                    return true;
                }
                else
                {
                    Debug.LogWarning("[Event Bus] try to call event id [" + eventId + "] but it's not register in anywhere.");
                }
            }
            return false;
        }

        public bool Emit(Evt eventId, object data = null)
        {
            lock (e_station)
            {
                if (e_station.ContainsKey(eventId))
                {
                    var evtPackages = e_station[eventId];
                    evtPackages.ForEach(p => p.subject.OnNext(data));
                    return true;
                }
                else
                {
                    Debug.LogWarning("[Event Bus] try to call event id [" + eventId + "] but it's not register in anywhere.");
                }
            }
            return false;
        }

        public bool Call(string eventId, object data = null)
        {
            lock (e_station)
            {
                if (s_station.ContainsKey(eventId))
                {
                    var evtPackages = s_station[eventId];
                    evtPackages.ForEach(p => p.subject.OnNext(data));
                    return true;
                }
                else
                {
                    Debug.LogWarning("[Event Bus] try to call event id [" + eventId + "] but it's not register in anywhere.");
                }
            }
            return false;
        }

        public void Call(string eventId, long delay, object data = null)
        {
            Call(delay, () =>
            {
                Call(eventId, data);
            });
        }

        public void Call(long delay, Action callback)
        {
            // create timer dispose after finish call
            var timer = Observable.Create<long>(o =>
            {
                var d = Observable.Timer(new TimeSpan(delay * TimeSpan.TicksPerMillisecond)).Subscribe(o);
                return Disposable.Create(() =>
                {
                    d.Dispose();
                });
            });
            timer.Subscribe(ticks => callback.Invoke());
        }

        public IDisposable Interval(long ticks, Action callback)
        {
            var d = Observable.Interval(new TimeSpan(ticks * TimeSpan.TicksPerMillisecond)).Subscribe(v => callback.Invoke());
            lock (s_intervalLock)
            {
                s_intervals.Add(d);
            }
            return d;
        }

        public void ClearInterval(IDisposable d)
        {
            lock (s_intervalLock)
            {
                s_intervals.Remove(d);
            }
            d.Dispose();
        }

        public void ClearAllInterval()
        {
            lock (s_intervalLock)
            {
                foreach (var d in s_intervals)
                {
                    d.Dispose();
                }
                s_intervals.Clear();
            }
        }
    }

    public class EvtPackage
    {
        internal Subject<object> subject = new Subject<object>();
        internal Subject<bool> subjectBool = new Subject<bool>();
        internal IDisposable disposable = null;
        internal IObservable<object> observable { get { return subject; } }
        internal IObservable<bool> observableBool { get { return subjectBool; } }
        internal string busId;
        internal Evt evt;
        internal string id;
    }
}