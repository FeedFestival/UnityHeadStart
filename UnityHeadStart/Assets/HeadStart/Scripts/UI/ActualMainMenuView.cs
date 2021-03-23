using UnityEngine;

public class ActualMainMenuView : MonoBehaviour, IUiView
{
    public GameObject Gobject { get { return this.gameObject; } }
    public void OnShow()
    {
        UIController._.MainMenu.SetHeaderView(HEADVIEW.Cog);
    }
}
