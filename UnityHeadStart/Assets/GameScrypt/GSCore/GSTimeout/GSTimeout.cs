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

        private void Awake()
        {
            _timersQueue = new Queue<GSTimer>();
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
