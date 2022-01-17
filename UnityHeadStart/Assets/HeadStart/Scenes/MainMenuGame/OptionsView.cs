using System.Collections;
using System.Collections.Generic;
using Assets.HeadStart.Core;
using UnityEngine;

public class OptionsView : MonoBehaviour, IUiView
{
    GameObject IUiView.GO()
    {
        return gameObject;
    }
    public void Focus()
    {
    }

    public void OnFocussed()
    {
        __.Time.RxWait(() =>
        {
            // ButtonHighscore.Interactable = false;
        }, 1f);
    }
}
