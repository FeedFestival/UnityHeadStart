using Assets.HeadStart.Core;
using UnityEngine;
using UnityEngine.Events;

public class OptionsView : MonoBehaviour, IUiView
{
    UnityAction IUiView.UiViewFocussed { get => uiViewFocussed; }
    public event UnityAction uiViewFocussed;

    GameObject IUiView.GO()
    {
        return gameObject;
    }
    public void Focus()
    {
    }

    // public void onFocussed()
    // {
    //     __.Time.RxWait(() =>
    //     {
    //         // ButtonHighscore.Interactable = false;
    //     }, 1f);
    // }
}
