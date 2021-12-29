using UnityEngine;

public class InitialSetup : MonoBehaviour, IUiView
{
    public bool NoCameraSetup;
    public CameraHelper CameraHelper;
    void IUiView.Focus()
    {
        if (NoCameraSetup)
        {
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
}
