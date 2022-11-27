using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameScrypt.Interfaces
{
    public interface IInteractSystemController
    {
        public UnityAction ActionPerformed { get; set; }
        public void ShowXButton(IInteractable interactable);
        public void HideXButton();
        public void ShowSmallText(string text = null);
    }

    public interface IInteractableDefinition
    {
        public Dictionary<object, IInteractable> Interactables { get; set; }
        public void Init(IInteractSystemController interactSystemController);
    }

    public interface IInteractable
    {
        public UnityAction<IUnit> InteractionCallback { get; set; }
        public Transform IndicatorTransform { get; }
        public void Init(IInteractSystemController interactSystemController);
        public void DisableInteract();
        public void DestroyInteractableBehaviour();
    }

}