using GameScrypt.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameScrypt.GSInteractSystem
{
    public class GSInteractSystemController : MonoBehaviour, IInteractSystemController
    {
        public UnityAction ActionPerformed { get => InteractCanvasController.ActionPerformed; set => InteractCanvasController.ActionPerformed = value; }
        public GSInteractCanvasController InteractCanvasController;
        public GSInteractableDefinition InteractableDefinition;

        public virtual void Init()
        {
            InteractCanvasController.Init();
            InteractableDefinition.Init(this);
        }

        public void ShowXButton(IInteractable interactable)
        {
            InteractCanvasController.ShowButtonForInteractable(interactable);
        }

        public void HideXButton()
        {
            InteractCanvasController.HideButtonForInteractable();
        }

        public void ShowSmallText(string text = null)
        {
            InteractCanvasController.ShowSmallText(text);
        }
    }
}
