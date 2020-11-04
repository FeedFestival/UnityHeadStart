#pragma warning disable 0414 // private field assigned but not used.
using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    public static readonly string _version = "1.0.1";
    private static Timer _Timer;
    public static Timer _ { get { return _Timer; } }

    private void Awake()
    {
        _Timer = this;
        DontDestroyOnLoad(gameObject);
    }

    public delegate void InternalWaitCallback();
    private InternalWaitCallback _internalWait;
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

    public void InternalWait(InternalWaitCallback internalWait, float? seconds = null)
    {
        if (seconds == null)
        {
            _waitOneFrame = true;
        }
        else
        {
            _seconds = seconds.Value;
        }
        _internalWait = internalWait;
        StartCoroutine(InternalWaitFunction());
    }

    private IEnumerator InternalWaitFunction()
    {
        if (_waitOneFrame)
        {
            _waitOneFrame = false;
            yield return 0;
        }
        else
        {
            yield return new WaitForSeconds(_seconds);
        }
        _internalWait();
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
