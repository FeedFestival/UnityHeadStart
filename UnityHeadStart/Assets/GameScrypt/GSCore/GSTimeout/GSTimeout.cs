using GameScrypt.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameScrypt.Core.Timeout
{
    public class GSTimeout: MonoBehaviour, ITimeout
    {
        private Queue<GSTimer> _timersQueue;
        private Queue<GSRxTimer> _rxTimersQueue;

        private void Awake()
        {
            _timersQueue = new Queue<GSTimer>();
            _rxTimersQueue = new Queue<GSRxTimer>();
        }

        public void Milliseconds(UnityAction callback, int? milliseconds = null)
        {
            float? seconds = milliseconds.HasValue ? ((float)milliseconds * 0.001f) : null;
            Seconds(callback, seconds);
        }

        public void Seconds(UnityAction callback, float? seconds = null)
        {
            var timer = new GSTimer(callback, seconds);
            timer.WaitFunc = this.timeout(timer);
            _timersQueue.Enqueue(timer);
            StartCoroutine(timer.WaitFunc);
        }

        public void RxMilliseconds(UnityAction callback, int? milliseconds = null)
        {
            float? seconds = milliseconds.HasValue ? ((float)milliseconds * 0.001f) : null;
            RxSeconds(callback, seconds);
        }

        public void RxSeconds(UnityAction callback, float? seconds = null)
        {
            var rxTimer = new GSRxTimer(callback, DequeueRx, seconds);
            _rxTimersQueue.Enqueue(rxTimer);
        }

        private void DequeueRx()
        {
            _rxTimersQueue.Dequeue();
        }

        private IEnumerator timeout(GSTimer waitOption)
        {
            if (waitOption.WaitOneFrame)
            {
                yield return 0;
            }
            else
            {
                yield return new WaitForSeconds(waitOption.Seconds);
            }
            waitOption.Callback.Invoke();
            _timersQueue.Dequeue();
        }
    }
}
