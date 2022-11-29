using Assets.HeadStart.Core;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonWrapper : MonoBehaviour
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "2.2.0";
#pragma warning restore 0414 //
    public int Id;
    public Button Btn;
    public TextMeshProUGUI Txt;
    private UnityAction<int> _coreIdCallback;
    public void Subscribe(UnityAction<int> coreIdCallback)
    {
        _coreIdCallback = coreIdCallback;
        Btn.onClick.AddListener(() =>
        {
            _coreIdCallback?.Invoke(Id);
        });
    }
    public void Unsubscribe()
    {
        Btn.onClick.RemoveAllListeners();
    }
}
