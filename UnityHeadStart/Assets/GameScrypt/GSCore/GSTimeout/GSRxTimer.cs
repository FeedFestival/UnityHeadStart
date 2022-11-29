using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace GameScrypt.Core.Timeout
{
    public class GSRxTimer : MonoBehaviour
    {
        public Subject<bool> WaitSubject__ { get; set; }
        private UnityAction _waitCallback { get; set; }
        private UnityAction _disposeCallback { get; set; }

        public GSRxTimer(UnityAction waitCallback, UnityAction disposeCallback, float? seconds)
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