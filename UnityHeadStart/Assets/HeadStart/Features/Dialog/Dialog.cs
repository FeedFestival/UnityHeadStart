using UnityEngine;

namespace Assets.HeadStart.Features.Dialog
{
    public class Dialog : MonoBehaviour, IDependency, IDialog
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.1.0";
#pragma warning restore 0414 //
        public GameObject DialogHelperWCP;
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

            go = Instantiate(
                DialogHelperWCP,
                Vector3.zero,
                Quaternion.identity
            );

            // we need to look insode the dialogHelper
            WorldCanvasPoint dialogWCP = go.transform.GetChild(0).gameObject.GetComponent<WorldCanvasPoint>();
            _dialogCanvas.SetPosition(dialogWCP);
            Destroy(go);
            dialogWCP = null;
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
