using Assets.HeadStart.Core;
using UnityEngine;

public class InitialSetup : MonoBehaviour, IUiView
{
    public bool SkipCameraSetup;
    public CameraHelper CameraHelper;
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

    public void OnFocussed()
    {
    }
}
