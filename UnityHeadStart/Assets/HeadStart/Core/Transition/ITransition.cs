using Assets.HeadStart.Core;

public interface ITransition
{
    void PitchBlack();
    void Do(Transition transition);
    void Do(Transition transition, bool instant = false);
    void Do(Transition transition, bool instant = false, CoreCallback onTransitionEnd = null);
    void Do(Transition transition, CoreCallback onTransitionEnd = null);
    float GetTime();
}
