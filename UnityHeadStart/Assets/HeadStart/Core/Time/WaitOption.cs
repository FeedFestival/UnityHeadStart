
using System;
using System.Collections;
using UniRx;

namespace Assets.HeadStart.Time
{
    public class WaitOption
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.0.8";
#pragma warning restore 0414 //
        public InternalWaitCallback WaitCallback { get; set; }
        public IEnumerator WaitFunc { get; set; }
        public bool WaitOneFrame { get; set; }
        public float Seconds { get; set; }

        public WaitOption(InternalWaitCallback waitCallback, float? seconds)
        {
            WaitCallback = waitCallback;
            WaitOneFrame = !seconds.HasValue;
            if (WaitOneFrame == false)
            {
                Seconds = seconds.Value;
            }
        }

        public WaitOption()
        {
        }
    }

    public class WaitOptionRx
    {
        public Subject<bool> WaitSubject__ { get; set; }
        private InternalWaitCallback _waitCallback { get; set; }
        private InternalWaitCallback _disposeCallback { get; set; }

        public WaitOptionRx(InternalWaitCallback waitCallback, InternalWaitCallback disposeCallback, float? seconds)
        {
            _waitCallback = waitCallback;
            _disposeCallback = disposeCallback;
            WaitSubject__ = new Subject<bool>();

            if (seconds.HasValue)
            {
                WaitSubject__
                    .Delay(TimeSpan.FromSeconds(seconds.Value))
                    .Subscribe(Subscription);
            }
            else
            {
                WaitSubject__
                    .Delay(TimeSpan.FromMilliseconds(1))
                    .Subscribe(Subscription);
            }

            WaitSubject__.OnNext(true);
        }

        private void Subscription(bool obj)
        {
            _waitCallback();
            _disposeCallback();
            WaitSubject__.OnCompleted();
            WaitSubject__.Dispose();
        }
    }
}
