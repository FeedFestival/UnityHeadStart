using System.Collections;
using System.Collections.Generic;
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
}
