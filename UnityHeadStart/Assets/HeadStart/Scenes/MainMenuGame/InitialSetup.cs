using Assets.HeadStart.Core;
using UnityEngine;
using UnityEngine.Events;

public class InitialSetup : MonoBehaviour, IUiView
{
    public bool SkipCameraSetup;
    public CameraHelper CameraHelper;
    UnityAction IUiView.UiViewFocussed { get => uiViewFocussed; }
    public event UnityAction uiViewFocussed;
    
    void IUiView.Focus()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        SkipCameraSetup = false;
#endif
        if (SkipCameraSetup)
        {
            Main._.CoreCamera.DestroyLogo();
            Main._.Game.StartGame();
            return;
        }

        Main._.CoreCamera.InitSetup(CameraHelper, () =>
        {
            Main._.Game.StartGame();
        });
    }

    GameObject IUiView.GO()
    {
        return gameObject;
    }

    public void onFocussed()
    {
    }
}
