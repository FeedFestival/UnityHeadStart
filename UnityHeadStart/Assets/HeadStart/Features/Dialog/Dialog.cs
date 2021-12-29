using UnityEngine;

namespace Assets.HeadStart.Features.Dialog
{
    public class Dialog : MonoBehaviour, IDependency, IDialog
    {
        [SerializeField]
        private GameObject DialogCanvasPrefab;
        private DialogCanvas _dialogCanvas;

        void IDependency.Init()
        {
            var go = Instantiate(
                DialogCanvasPrefab,
                Vector3.zero,
                Quaternion.identity,
                Main._.CoreCamera.Views
            );
            (go.transform as RectTransform).localPosition = Vector3.zero;
            _dialogCanvas = go.GetComponent<DialogCanvas>();
        }

        public void Show(DialogOptions options)
        {
            _dialogCanvas.Show(options);
        }

        public void OnContinue()
        {
            Main._.Game.GoToMainMenu();
        }

        public void OnRestart()
        {
            Main._.Game.Restart();
        }
    }
}
