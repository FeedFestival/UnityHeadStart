using Assets.HeadStart.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonWrapper : MonoBehaviour
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "2.1.0";
#pragma warning restore 0414 //
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
