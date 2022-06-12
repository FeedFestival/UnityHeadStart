using Assets.HeadStart.Core;
using UnityEngine;
using UnityEngine.Events;

public class InitialSetup : MonoBehaviour, IUiView
{
    UnityAction IUiView.UiViewFocussed { get => uiViewFocussed; }
    public event UnityAction uiViewFocussed;

    private void Init()
    {
        uiViewFocussed += onFocussed;
    }

    void IUiView.Focus()
    {
        Main.S._EnvironmentReady__.OnNext(true);
    }

    GameObject IUiView.GO()
    {
        return gameObject;
    }

    public void onFocussed()
    {
        // TODO: no class should be forced to implement a function it doesn't use
    }
}
