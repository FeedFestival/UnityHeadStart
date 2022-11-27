using GameScrypt.Interfaces;
using GameScrypt.TriggerSystem;
using UnityEngine;
using UnityEngine.Events;

namespace GameScrypt.GSInteractSystem
{
    public class GSInteractable : MonoBehaviour, IInteractable
    {
        public Transform ButtonIndicatorTransform;
        public string Tooltip;
        [HideInInspector]
        public Transform IndicatorTransform { get { return ButtonIndicatorTransform; } }
        public UnityAction<IUnit> InteractionCallback { get; set; }
        [Header("Triggers")]
        public GSInteractableTrigger StaticTrigger;

        protected IInteractSystemController _interactSystemController;
        private IUnit _unit;

        public virtual void Init(IInteractSystemController interactSystemController)
        {
            _interactSystemController = interactSystemController;

            StaticTrigger.Init(GSInteractSystemService.INTERACT_LAYER, GSInteractSystemService.INTERACT_UNIT_TAG, unitEntered, unitExited);
        }

        public virtual void DisableInteract()
        {
            this.unitExited();
            StaticTrigger.DisableTrigger();
        }

        public virtual void EnableInteract()
        {
            StaticTrigger.EnableTrigger();
        }

        public virtual void DestroyInteractableBehaviour()
        {
            this.unitExited();
            this.destroyTrigger();
            this.destroyInteractableBehaviour();
        }

        protected void showTooltip()
        {
            if (string.IsNullOrWhiteSpace(Tooltip)) { return; }

            _interactSystemController.ShowSmallText(Tooltip);
        }

        protected virtual void actionPerformed()
        {
            _interactSystemController.HideXButton();
            this.unregisterForAction();
        }

        protected virtual void emitInteraction()
        {
            InteractionCallback?.Invoke(_unit);
        }

        private void unitEntered(IUnit unit)
        {
            _unit = unit;
            _interactSystemController.ShowXButton(this);
            this.registerForAction();
        }

        private void unitExited()
        {
            _unit = null;
            _interactSystemController.HideXButton();
            this.unregisterForAction();
        }

        private void registerForAction()
        {
            _interactSystemController.ActionPerformed += actionPerformed;
        }

        private void unregisterForAction()
        {
            _interactSystemController.ActionPerformed -= actionPerformed;
        }

        private void destroyTrigger()
        {
            StaticTrigger.DestroyTrigger();
        }

        private void destroyInteractableBehaviour()
        {
            Destroy(gameObject.GetComponent<GSInteractable>());
        }
    }
}
