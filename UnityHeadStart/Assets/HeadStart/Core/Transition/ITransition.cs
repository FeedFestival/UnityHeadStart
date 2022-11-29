using UnityEngine.Events;

public interface ITransition
{
    void PitchBlack();
    void Do(Transition transition);
    void Do(Transition transition, bool instant = false);
    void Do(Transition transition, bool instant = false, UnityAction onTransitionEnd = null);
    void Do(Transition transition, UnityAction onTransitionEnd = null);
    float GetTime();
}
