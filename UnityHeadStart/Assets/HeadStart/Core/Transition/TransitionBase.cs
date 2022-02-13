using Assets.HeadStart.Core;
using UnityEngine;

public enum Transition
{
    START, END
}

public class TransitionBase : MonoBehaviour, IDependency, ITransition
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "2.0.8";
#pragma warning restore 0414 //

    public Color StartColor;
    public Color EndColor;
    internal Transition Transition;
    private CoreCallback _onTransitionEnd;
    private int? _fadeAnimationId;
    internal readonly float TRANSITION_TIME = 0.44f;

    public void Init()
    {
    }

    public void PitchBlack()
    {
        Do(Transition.START, instant: true);
    }

    public void Do(Transition transition, bool instant = false, CoreCallback onTransitionEnd = null)
    {
        Transition = transition;

        if (!Main._.CoreCamera.LoadingOverlay) { return; }

        var newColor = GetTransitionColor();

        if (instant)
        {
            Main._.CoreCamera.LoadingOverlay.color = newColor;
            return;
        }

        _onTransitionEnd = onTransitionEnd;
        Main._.CoreCamera.LoadingOverlay.color = GetTransitionColor(reverse: true);
        _fadeAnimationId = LeanTween.color(
            Main._.CoreCamera.LoadingOverlay.gameObject.GetComponent<RectTransform>(),
            newColor,
            TRANSITION_TIME
        ).id;
        LeanTween.descr(_fadeAnimationId.Value).setEase(LeanTweenType.easeInOutQuart);
        if (_onTransitionEnd != null)
        {
            LeanTween.descr(_fadeAnimationId.Value).setOnComplete(() =>
            {
                _onTransitionEnd();
                _onTransitionEnd = null;
            });
        }
    }

    void ITransition.Do(Transition transition)
    {
        Do(transition);
    }

    void ITransition.Do(Transition transition, bool instant)
    {
        Do(transition, instant);
    }

    void ITransition.Do(Transition transition, CoreCallback onTransitionEnd)
    {
        Do(transition, false, onTransitionEnd);
    }

    private Color GetTransitionColor(bool reverse = false)
    {
        return (reverse ? Transition == Transition.END : Transition == Transition.START)
            ? StartColor
            : EndColor;
    }

    float ITransition.GetTime()
    {
        return TRANSITION_TIME;
    }
}
