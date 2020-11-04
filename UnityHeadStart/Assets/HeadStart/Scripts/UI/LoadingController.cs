using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    public static readonly string _version = "1.0.1";
    private static LoadingController _loadingController;
    void Awake()
    {
        _loadingController = this;
        _overlay = GetComponent<Image>();
    }

    private Image _overlay;
    public Text LoadingText;
    public Button ContinueButton;
    public List<GameObject> PlaceholderElements;

    public delegate void OnContinueButtonPress();
    public delegate void OnTransitionEnd();

    [SerializeField]
    private float _timeBetweenFadings = 1.2f;
    [SerializeField]
    private float _fadeInTimeout = 0.5f;
    [SerializeField]
    private string _loadingTextString = "Loading";
    [SerializeField]
    private float _dotsAnimationTimeout = 1f;
    private int _dotCount = 0;
    private IEnumerator _animateDots;
    private int? _fadeTextAnimationId;
    private int? _fadeAnimationId;
    private OnContinueButtonPress _onContinueButtonPress;
    private OnTransitionEnd _onTransitionEnd;

    private bool _isFadeIn;

    public void Init()
    {
        if (ContinueButton != null)
        {
            ContinueButton.gameObject.SetActive(false);
        }
        if (LoadingText != null)
        {
            LoadingText.gameObject.SetActive(false);
        }
    }

    public void TransitionOverlay(bool show = true, bool instant = false, OnTransitionEnd onTransitionEnd = null)
    {
        _isFadeIn = show;
        if (instant == false)
        {
            _onTransitionEnd = onTransitionEnd;
            _overlay.color = show ? HiddenSettings._.BlackColor : HiddenSettings._.TransparentColor;
            _fadeAnimationId = LeanTween.color(_overlay.gameObject.GetComponent<RectTransform>(),
            show ? HiddenSettings._.TransparentColor : HiddenSettings._.BlackColor,
            _timeBetweenFadings).id;
            LeanTween.descr(_fadeAnimationId.Value).setEase(LeanTweenType.linear);
            if (_onTransitionEnd != null)
            {
                LeanTween.descr(_fadeAnimationId.Value).setOnComplete(() => { _onTransitionEnd(); });
            }
            return;
        }
        _overlay.color = show ? HiddenSettings._.TransparentColor : HiddenSettings._.BlackColor;
    }

    public void ShowLoading()
    {
        LoadingText.gameObject.SetActive(true);
        var color = HiddenSettings._.RedColor;
        color.a = 0;
        LoadingText.color = color;

        Fade(true);

        LoadingText.text = _loadingTextString;

        _animateDots = AnimateDots();
        StartCoroutine(_animateDots);
    }

    public void HideLoading(OnContinueButtonPress onContinueButtonPress = null)
    {
        LeanTween.cancel(_fadeTextAnimationId.Value);
        StopCoroutine(_animateDots);

        LoadingText.gameObject.SetActive(false);
        foreach (var item in PlaceholderElements)
        {
            item.SetActive(false);
        }

        if (onContinueButtonPress != null)
        {
            _onContinueButtonPress = onContinueButtonPress;

            ContinueButton.gameObject.SetActive(true);
        }
    }

    IEnumerator AnimateDots()
    {
        yield return new WaitForSeconds(_dotsAnimationTimeout);

        if (_dotCount > 3)
            _dotCount = 1;
        var dots = "";
        for (var i = 0; i < _dotCount; i++)
        {
            dots += ".";
        }
        _dotCount++;
        LoadingText.text = _loadingTextString + dots;

        _animateDots = AnimateDots();
        StartCoroutine(_animateDots);
    }

    public void ContinueButtonDown()
    {
        ContinueButton.gameObject.SetActive(false);
        _onContinueButtonPress();
    }

    public void Fade(bool fadeIn)
    {
        _isFadeIn = fadeIn;

        StartCoroutine(FadeRoutine(_isFadeIn ? _fadeInTimeout : 0f));
    }

    IEnumerator FadeRoutine(float timeToWait)
    {
        var time = HiddenSettings._.GetTime(timeToWait);
        yield return new WaitForSeconds(time);

        byte from = 255;
        float to = 0f;
        LeanTweenType leanTweenType = LeanTweenType.easeInBack;
        if (_isFadeIn)
        {
            from = 0;
            to = 1f;
            leanTweenType = LeanTweenType.easeOutBack;
        }

        var color = HiddenSettings._.RedColor;
        color.a = from;

        LoadingText.color = color;
        _fadeTextAnimationId = LeanTween.alphaText(LoadingText.gameObject.GetComponent<RectTransform>(), to, _timeBetweenFadings).id;
        LeanTween.descr(_fadeTextAnimationId.Value).setEase(leanTweenType);

        if (_isFadeIn)
        {
            LeanTween.descr(_fadeTextAnimationId.Value).setOnComplete(OnFadeInComplete);
        }
        else
        {
            LeanTween.descr(_fadeTextAnimationId.Value).setOnComplete(OnFadeOutComplete);
        }
    }

    public System.Action OnFadeInComplete = delegate ()
    {
        _loadingController.Fade(false);
    };

    public System.Action OnFadeOutComplete = delegate ()
    {
        _loadingController.Fade(true);
    };

    public System.Action OnTransitionComplete = delegate ()
    {
        // _loadingController._overlay
    };
}