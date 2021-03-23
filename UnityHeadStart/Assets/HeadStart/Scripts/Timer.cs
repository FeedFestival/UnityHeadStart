using UnityEngine;
using System.Collections;
using static Timer;
using System.Collections.Generic;

public class Timer : MonoBehaviour
{
#pragma warning disable 0414 //
    public static readonly string _version = "1.0.3";
#pragma warning restore 0414 //
    private static Timer _Timer;
    public static Timer _ { get { return _Timer; } }

    private void Awake()
    {
        _Timer = this;
        DontDestroyOnLoad(gameObject);
        _internalWaits = new Queue<WaitOption>();
    }

    public delegate void InternalWaitCallback();
    private InternalWaitCallback _debounceWait;
    public delegate void AsyncForEachCallback(int index);
    private AsyncForEachCallback _asyncForEach;
    private float _seconds;
    private int _lenght;
    private int _index;
    private bool _waitOneFrame;
    private bool _waitOneFrameDebounce;
    private bool _waitOneFrameIteration;
    private Texture2D _currentLoadedPicture;
    private string _fileName;
    private Queue<WaitOption> _internalWaits;

    public void InternalWait(InternalWaitCallback internalWaitCallback, float? seconds = null)
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
        // Debug.Log(__utils.DebugQueue<WaitOption>(_internalWaits, "_internalWaits"));
    }

    public void Debounce(InternalWaitCallback debounceWait, float? seconds = null)
    {
        if (seconds == null)
        {
            _waitOneFrameDebounce = true;
        }
        _debounceWait = debounceWait;
        StartCoroutine(DebounceFunction(seconds));
    }

    private IEnumerator DebounceFunction(float? seconds)
    {
        if (_waitOneFrameDebounce)
        {
            _waitOneFrameDebounce = false;
            yield return 0;
        }
        else
        {
            yield return new WaitForSeconds(seconds.Value);
        }
        _debounceWait();
    }

    public void AsyncForEach(int length, AsyncForEachCallback asyncForEach, float? seconds = null)
    {
        if (seconds == null)
        {
            _waitOneFrameIteration = true;
        }
        else
        {
            _seconds = seconds.Value;
        }
        _lenght = length;
        _asyncForEach = asyncForEach;
        _index = 0;
        StartCoroutine(DoAsyncIteration());
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

        _asyncForEach(_index);
        if (_index < _lenght - 1)
        {
            _index++;
            StartCoroutine(DoAsyncIteration());
        }
        else
        {
            _waitOneFrameIteration = false;
        }
    }
}

public class WaitOption
{
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
