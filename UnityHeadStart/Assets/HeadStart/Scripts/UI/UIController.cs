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
        UiDataController.gameObject.SetActive(false);
        LoadingController.Init(totalBlackness: true);
    }
    public DialogController DialogController;
    public UiDataController UiDataController;
    public LoadingController LoadingController;
    public MainMenu MainMenu;

    public void Init()
    {
        if (UiDataController != null)
        {
            UiDataController.gameObject.SetActive(true);
            UiDataController.Init();
        }

        if (DialogController != null)
        {
            DialogController.gameObject.SetActive(true);
            DialogController.ShowDialog(false);
        }

        LoadingController.TransitionOverlay(show: true, instant: false);
    }

    public void ShowInputNameView()
    {
        if (MainMenu == null)
        {
            return;
        }
        MainMenu.Init(showMenu: true, overrideView: VIEW.InputName);
        MainMenu.InputNameView.OnAdvanceDelegate = (string name) =>
        {
            User user = Game._.User;
            user.Name = name;
            user.IsFirstTime = false;
            Game._.DataService.UpdateUser(user);
            InitMainMenu();
        };
    }

    public void InitMainMenu()
    {
        if (MainMenu == null)
        {
            return;
        }
        MainMenu.Init(showMenu: true);
    }

    public void DestroyMainMenu()
    {
        if (MainMenu == null)
        {
            return;
        }
        Destroy(MainMenu.gameObject);
    }
}
