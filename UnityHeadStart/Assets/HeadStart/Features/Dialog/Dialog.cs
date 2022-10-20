using UnityEngine;

namespace Assets.HeadStart.Features.Dialog
{
    public class Dialog : MonoBehaviour, IDependency, IDialog
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.2.0";
#pragma warning restore 0414 //
        public DialogCanvas DialogCanvas;

        void IDependency.Init()
        {
        }

        public void Show(DialogOptions options)
        {
            DialogCanvas.Show(options);
        }

        public void OnContinue()
        {
            Main.S.Game.GoToMainMenu();
        }

        public void OnRestart()
        {
            Main.S.Game.Restart();
        }
    }
}
