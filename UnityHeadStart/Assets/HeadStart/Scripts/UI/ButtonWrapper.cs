using Assets.HeadStart.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonWrapper : MonoBehaviour
{
    public int Id;
    public Button Btn;
    public TextMeshProUGUI Txt;
    private CoreIdCallback _coreIdCallback;
    public void Subscribe(CoreIdCallback coreIdCallback)
    {
        _coreIdCallback = coreIdCallback;
        Btn.onClick.AddListener(() =>
        {
            _coreIdCallback(Id);
        });
    }
    public void Unsubscribe()
    {
        Btn.onClick.RemoveAllListeners();
    }
}
