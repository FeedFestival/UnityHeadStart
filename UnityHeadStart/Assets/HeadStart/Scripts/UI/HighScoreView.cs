using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreView : MonoBehaviour, IUiView
{
    public GameObject Gobject { get { return this.gameObject; } }
    public void OnShow()
    {
        UIController._.MainMenu.SetHeaderView(HEADVIEW.Back);
    }
}
