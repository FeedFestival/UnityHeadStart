using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.HeadStart.Core;
using Assets.Scripts.utils;

namespace Assets.HeadStart.Time
{
    public delegate void InternalWaitCallback();
    public delegate void AsyncForEachCallbackIndex(int index);
    public delegate void AsyncForEachCallback();
    public delegate void AsyncForEachCallbackT(object obj);

    public class TimeBase : MonoBehaviour, IDependency, ITime
    {
#pragma warning disable 0414 //
        public static readonly string _version = "2.0.8";
#pragma warning restore 0414 //
        private AsyncForEachCallback _asyncForEach;
        private AsyncForEachCallbackIndex _asyncForEachIndex;
        private AsyncForEachCallbackT _asyncForEachT;
        private float _seconds;
        private int _lenght;
        private int _index;
        private bool _waitOneFrameIteration;
        private string _fileName;
        private Queue<WaitOption> _internalWaits;
        private Queue<WaitOptionRx> _internalWaitsRx;
        private bool _asyncIsRunning;
        private IEnumerator _asyncForEachEnm;

        public void Init()
        {
            _internalWaits = new Queue<WaitOption>();
            _internalWaitsRx = new Queue<WaitOptionRx>();
        }

        public void RxWait(InternalWaitCallback internalWaitCallback, float? seconds = null)
        {
            var waitOptionRx = new WaitOptionRx(internalWaitCallback, DequeueRx, seconds);
            _internalWaitsRx.Enqueue(waitOptionRx);
        }

        private void DequeueRx()
        {
            _internalWaitsRx.Dequeue();
        }

        public void Wait(InternalWaitCallback internalWaitCallback, float? seconds = null)
        {
            var waitOption = new WaitOption(internalWaitCallback, seconds);
            waitOption.WaitFunc = InternalWaitFunction(waitOption);
            _internalWaits.Enqueue(waitOption);
            StartCoroutine(waitOption.WaitFunc);
        }

        private IEnumerator InternalWaitFunction(WaitOption waitOption)
        {
            if (waitOption.WaitOneFrame)
            {
                yield return 0;
            }
            else
            {
                yield return new WaitForSeconds(waitOption.Seconds);
            }
            waitOption.WaitCallback();
            _internalWaits.Dequeue();
        }

        public void AsyncForEach(int length, AsyncForEachCallbackIndex asyncForEach, float? seconds = null)
        {
            InitAsync(length, seconds);
            _asyncForEachIndex = asyncForEach;
            _asyncForEachEnm = DoAsyncIteration();
            StartCoroutine(_asyncForEachEnm);
        }

        public void AsyncForEach(int length, AsyncForEachCallback asyncForEach, float? seconds = null)
        {
            InitAsync(length, seconds);
            _asyncForEach = asyncForEach;
            _asyncForEachEnm = DoAsyncIteration();
            StartCoroutine(_asyncForEachEnm);
        }

        public void AsyncForEach<T>(List<T> list, AsyncForEachCallbackT asyncForEach, float? seconds = null)
        {
            InitAsync(list.Count, seconds);
            _asyncForEachT = asyncForEach;
            StartCoroutine(DoAsyncIteration(list));
        }

        public void AsyncForEach<T>(T[] array, AsyncForEachCallbackT asyncForEach, float? seconds = null)
        {
            InitAsync(array.Length, seconds);
            _asyncForEachT = asyncForEach;
            StartCoroutine(DoAsyncIteration(array));
        }

        private void InitAsync(int length, float? seconds)
        {
            if (_asyncIsRunning)
            {
                Debug.LogError("No more then one async operation is allowed");
            }
            _asyncIsRunning = true;

            if (seconds == null)
            {
                _waitOneFrameIteration = true;
            }
            else
            {
                _seconds = seconds.Value;
            }
            _lenght = length;
            _index = 0;
        }

        IEnumerator DoAsyncIteration()
        {
            if (_waitOneFrameIteration)
            {
                yield return 0;
            }
            else
            {
                yield return new WaitForSeconds(_seconds);
            }

            AsyncForeachCallBack();
            _asyncForEachEnm = DoAsyncIteration();
            AfterAsyncIteration(_asyncForEachEnm);
        }

        IEnumerator DoAsyncIteration<T>(List<T> list)
        {
            if (_waitOneFrameIteration)
            {
                yield return 0;
            }
            else
            {
                yield return new WaitForSeconds(_seconds);
            }

            _asyncForEachT(list[_index]);
            AfterAsyncIteration(DoAsyncIteration(list));
        }

        IEnumerator DoAsyncIteration<T>(T[] array)
        {
            if (_waitOneFrameIteration)
            {
                yield return 0;
            }
            else
            {
                yield return new WaitForSeconds(_seconds);
            }

            _asyncForEachT(array[_index]);
            AfterAsyncIteration(DoAsyncIteration(array));
        }

        private void AfterAsyncIteration(IEnumerator doAsyncAgain)
        {
            if (_index < _lenght - 1)
            {
                _index++;
                StartCoroutine(doAsyncAgain);
            }
            else
            {
                _asyncIsRunning = false;

                _asyncForEach = null;
                _asyncForEachIndex = null;
                _asyncForEachT = null;
                _waitOneFrameIteration = false;
            }
        }

        private void AsyncForeachCallBack()
        {
            if (_asyncForEach != null)
            {
                _asyncForEach();
            }
            else if (_asyncForEachIndex != null)
            {
                _asyncForEachIndex(_index);
            }
        }
    }
}
