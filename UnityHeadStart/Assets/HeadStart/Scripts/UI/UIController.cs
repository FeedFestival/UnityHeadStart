using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private static UIController _uIController;
    public static UIController _ { get { return _uIController; } }
    void Awake()
    {
        _uIController = this;
    }
    public DialogController DialogController;
    public UiDataController UiDataController;
    public LoadingController LoadingController;
    public MainMenu MainMenu;

    public void Init()
    {
        LoadingController.Init();

        if (UIController._.UiDataController != null)
        {
            UIController._.UiDataController.Init();
            UIController._.UiDataController.gameObject.SetActive(false);
        }

        if (DialogController != null)
        {
            DialogController.gameObject.SetActive(true);
            DialogController.ShowDialog(false);
        }
    }

    public void InitMainMenu(bool isLevelMainMenu)
    {
        if (MainMenu != null)
        {
            if (isLevelMainMenu)
            {
                MainMenu.Init(showMenu: true, hasSavedGame: Game._.User.HasSavedGame);
            }
            else
            {
                Destroy(MainMenu.gameObject);
            }
        }
    }
}
