using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputNameView : MonoBehaviour, IUiView
{
    public InputFieldCustom InputFieldCustom;
    public Button AdvanceButton;
    private string _name;
    public delegate void OnAdvanceCallback(string name);
    public OnAdvanceCallback OnAdvanceDelegate;
    public GameObject Gobject { get { return this.gameObject; } }

    // Start is called before the first frame update
    void Start()
    {
        AdvanceButton.interactable = false;
        InputFieldCustom.OnBlurDelegate = () =>
        {
            _name = InputFieldCustom.InputField.text;
            if (string.IsNullOrWhiteSpace(_name))
            {
                AdvanceButton.interactable = false;
            }
            else
            {
                AdvanceButton.interactable = true;
            }
        };
    }

    public void OnShow()
    {
        UIController._.MainMenu.SetHeaderView(HEADVIEW.Nothing);
    }

    public void OnAdvance()
    {
        OnAdvanceDelegate?.Invoke(_name);
    }
}
