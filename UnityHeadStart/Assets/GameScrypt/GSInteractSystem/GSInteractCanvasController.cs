using GameScrypt.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameScrypt.GSInteractSystem
{
    public class GSInteractCanvasController : MonoBehaviour
    {
        public Image XButton;
        public GSSMallText GSSMallText;
        public UnityAction ActionPerformed;

        private RectTransform _xButtonRt;
        private RectTransform _smallTextRt;
        private Transform _followT;
        private Transform _smallTextFollowT;
        private Vector2 _screenSize;

        private void Update()
        {
            if (_followT != null)
            {
                this.followInteractableWithX();

                if (Input.GetKeyUp(KeyCode.X))
                {
                    ActionPerformed?.Invoke();
                }
            }

            if (_smallTextFollowT != null)
            {
                this.followInteractableWithSmallText();
            }
        }

        internal void Init()
        {
            this.setupCanvasVariables();
            this.hideXButton();
            this.HideSmallText();
        }

        internal void ShowButtonForInteractable(IInteractable interactable)
        {
            this.setFollowTransform(interactable);
            this.showXButton();
        }

        internal void HideButtonForInteractable()
        {
            this.stopFollowingWithX();
            this.hideXButton();
        }

        internal void ShowSmallText(string text = null)
        {
            _smallTextFollowT = _followT;
            GSSMallText.Show(text);

            __.Timeout.Seconds(() =>
            {
                HideSmallText();
            }, 2);
        }

        internal void HideSmallText()
        {
            _smallTextFollowT = null;
            GSSMallText.gameObject.SetActive(false);
        }

        private void setupCanvasVariables()
        {
            _xButtonRt = (XButton.transform as RectTransform);
            _smallTextRt = (GSSMallText.transform as RectTransform);
            _screenSize = (this.transform as RectTransform).sizeDelta;
        }

        private void hideXButton()
        {
            XButton.gameObject.SetActive(false);
        }
        private void showXButton()
        {
            XButton.gameObject.SetActive(true);
        }

        private void setFollowTransform(IInteractable interactable)
        {
            this._followT = interactable.IndicatorTransform;
        }

        private void stopFollowingWithX()
        {
            this._followT = null;
        }

        private void followInteractableWithX()
        {
            GSInteractCanvasController.ShowOnScreen(ref _xButtonRt, _followT.position, _screenSize, isAtCenter: true);
        }

        private void followInteractableWithSmallText()
        {
            GSInteractCanvasController.ShowOnScreen(ref _smallTextRt, _smallTextFollowT.position, _screenSize, isAtCenter: true);
        }

        public static void ShowOnScreen(
            ref RectTransform rt,
            Vector3 worldPosition,
            Vector2 canvasSize,
            bool isAtCenter = true
        )
        {
            rt.anchoredPosition = GetWorldObjScreenPos(worldPosition, canvasSize, isAtCenter);
        }

        public static Vector2 GetWorldObjScreenPos(Vector3 worldPosition, Vector2 canvasSize, bool isAtCenter = true)
        {
            var viewportPosition = Camera.main.WorldToViewportPoint(worldPosition);
            Vector2 worldObjectScreenPosition = new Vector2(
                ((viewportPosition.x * canvasSize.x) - (canvasSize.x * 0.5f)),
                ((viewportPosition.y * canvasSize.y) - (canvasSize.y * 0.5f))
            );
            if (isAtCenter == false)
            {
                worldObjectScreenPosition = new Vector2(
                    ((viewportPosition.x * canvasSize.x)),
                    ((viewportPosition.y * canvasSize.y))
                );
            }
            return worldObjectScreenPosition;
        }
    }
}
