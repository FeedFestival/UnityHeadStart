using Assets.HeadStart.Core;
using UnityEngine;

public class InputNameView : MonoBehaviour, IUiView
{
    public GameButton ButtonBack;
    public GameButton ButtonPlay;
    public Transform TLPoint;
    public Transform BRPoint;
    private InputNameCanvas _inputNameCanvas;
    public InputNameSettings InputNameSettings;
    private bool _isInitialized;

    private void Init()
    {
        ButtonBack.OnClick(() =>
        {
            _inputNameCanvas.gameObject.SetActive(false);
            _inputNameCanvas.CancelChallenge();
            MenuEnvironment._.ClearHotseatSession();
            MenuEnvironment._.Back();
        });
        ButtonPlay.OnClick(() =>
        {
            _inputNameCanvas.CancelChallenge();
            _inputNameCanvas.gameObject.SetActive(false);
            MenuEnvironment._.SwitchView(VIEW.GameSession);
        });

        var go = Instantiate(
            InputNameSettings.InputNameConvas,
            Vector3.zero,
            Quaternion.identity,
            parent: Main._.CoreCamera.Views
        );
        (go.transform as RectTransform).localPosition = Vector3.zero;
        _inputNameCanvas = go.GetComponent<InputNameCanvas>();

        _isInitialized = true;
    }

    GameObject IUiView.GO()
    {
        return gameObject;
    }

    public void Focus()
    {
        if (_isInitialized == false)
        {
            Init();
        }

        ButtonPlay.Interactable = false;

        __.Time.RxWait(() =>
        {
            _inputNameCanvas.gameObject.SetActive(true);
            _inputNameCanvas.Show(TLPoint, BRPoint, (object obj) =>
            {
                bool isValid = obj != null;
                ButtonPlay.Interactable = isValid;
                if (isValid)
                {
                    MenuEnvironment._.SetHotseatSession(obj as SessionOpts);
                }
            });

            ButtonPlay.Interactable = true;

            if (TLPoint == null)
            {
                return;
            }
            Destroy(TLPoint.gameObject);
            TLPoint = null;
            Destroy(BRPoint.gameObject);
            BRPoint = null;
        }, MenuEnvironment._.MOVE_CAMERA_TIME);
    }
}
