using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Assets.HeadStart.Core
{
    public class CoreEvent
    {
        private object s_stationLock = new object();
        private object s_intervalLock = new object();
        private Dictionary<Evt, List<Passenger>> e_station = new Dictionary<Evt, List<Passenger>>();
        private Dictionary<string, List<Passenger>> s_station = new Dictionary<string, List<Passenger>>();
        private List<IDisposable> s_intervals = new List<IDisposable>();

        public CoreEvent() { }

        public void On(Evt eventId, Action handler)
        {
            OnEmpty(eventId, handler, Scheduler.MainThread);
        }

        public void On(Evt eventId, Action<bool> handler)
        {
            On(eventId, handler, Scheduler.MainThread);
        }

        public void On(Evt eventId, Action<object> handler)
        {
            On(eventId, handler, Scheduler.MainThread);
        }

        public void On(Evt eventId, Action<bool> handler, IScheduler runOn)
        {
            // create new slot for this bus
            var newPassenger = new Passenger
            {
                evt = eventId
            };
            newPassenger.disposable = newPassenger.observableBool
                .ObserveOn(runOn)
                .Subscribe(obj => handler(obj));

            lock (e_station)
            {
                // check if we already add this bus
                if (e_station.ContainsKey(eventId))
                {
                    e_station[eventId].Add(newPassenger);
                }
                else
                {
                    e_station[eventId] = new List<Passenger>() { newPassenger };
                }
            }
        }

        public void On(Evt eventId, Action<object> handler, IScheduler runOn)
        {
            // create new slot for this bus
            var newPassenger = new Passenger
            {
                evt = eventId
            };
            newPassenger.disposable = newPassenger.observable
                .ObserveOn(runOn)
                .Subscribe(obj => handler(obj));

            lock (e_station)
            {
                // check if we already add this bus
                if (e_station.ContainsKey(eventId))
                {
                    e_station[eventId].Add(newPassenger);
                }
                else
                {
                    e_station[eventId] = new List<Passenger>() { newPassenger };
                }
            }
        }

        private void OnEmpty(Evt eventId, Action emptyHandler, IScheduler runOn)
        {
            // create new slot for this bus
            var newPassenger = new Passenger
            {
                evt = eventId
            };
            newPassenger.disposable = newPassenger.observable
                .ObserveOn(runOn)
                .Subscribe(obj => emptyHandler());

            lock (e_station)
            {
                // check if we already add this bus
                if (e_station.ContainsKey(eventId))
                {
                    e_station[eventId].Add(newPassenger);
                }
                else
                {
                    e_station[eventId] = new List<Passenger>() { newPassenger };
                }
            }
        }

        public void Register(object passenger, string eventId, Action<object, object> handler)
        {
            Register(passenger, eventId, handler, Scheduler.MainThread);
        }

        public void Register(object passenger, string eventId, Action<object, object> handler, IScheduler runOn)
        {
            // create new slot for this bus
            var newPassenger = new Passenger
            {
                id = passenger.GetType().FullName,
                busId = eventId
            };
            newPassenger.disposable = newPassenger.observable
                .ObserveOn(runOn)
                .Subscribe(obj => handler(passenger, obj));

            lock (s_stationLock)
            {
                // check if we already add this bus
                if (s_station.ContainsKey(eventId))
                {
                    var passengers = s_station[eventId];
                    var shouldAdd = true;
                    foreach (var op in passengers)
                    {
                        // there is no glich on matrix that 2 version of passenger on bus
                        // at same time
                        if (op.id == newPassenger.id)
                        {
                            shouldAdd = false;
                            break;
                        }
                    }
                    if (shouldAdd)
                    {
                        passengers.Add(newPassenger);
                    }
                }
                else
                {
                    s_station[eventId] = new List<Passenger>() { newPassenger };
                }
            }
        }

        public void Unregister(string eventId)
        {
            lock (s_stationLock)
            {
                if (s_station.ContainsKey(eventId))
                {
                    var passengers = s_station[eventId];
                    passengers.ForEach(p => p.disposable?.Dispose());
                    passengers.Clear();
                    s_station.Remove(eventId);
                }
                else
                {
                    Debug.LogWarning("[Event Bus] try to unregister event id [" + eventId + "] but not found");
                }
            }
        }

        public void Unregister(object passenger)
        {
            lock (s_stationLock)
            {
                foreach (var eventId in s_station.Keys)
                {
                    var passengers = s_station[eventId];
                    foreach (var p in passengers)
                    {
                        if (p.id == passenger.GetType().FullName)
                        {
                            p.disposable?.Dispose();
                            passengers.Remove(p);
                            break;
                        }
                    }
                    if (passengers.Count == 0)
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
                    var passengers = s_station[eventId];
                    foreach (var p in passengers)
                    {
                        p.disposable?.Dispose();
                    }
                    passengers.Clear();
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
                    var passengers = e_station[eventId];
                    passengers.ForEach(p => p.subjectBool.OnNext(data));
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
                    var passengers = e_station[eventId];
                    passengers.ForEach(p => p.subject.OnNext(data));
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
                    var passengers = s_station[eventId];
                    passengers.ForEach(p => p.subject.OnNext(data));
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

    class Passenger
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