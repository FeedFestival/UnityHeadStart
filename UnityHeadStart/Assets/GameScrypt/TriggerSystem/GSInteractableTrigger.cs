using GameScrypt.Interfaces;
using UnityEngine;

namespace GameScrypt.TriggerSystem
{
    public class GSInteractableTrigger : GSBaseTrigger
    {
        private int? _lockedByUnitId;

        public override void DestroyTrigger()
        {
            _lockedByUnitId = null;
            base.DestroyTrigger();
        }

        public override void DisableTrigger()
        {
            _lockedByUnitId = null;
            base.DisableTrigger();
        }

        public void EnableTrigger()
        {
            this.gameObject.SetActive(true);
        }

        protected override void onTriggerEnter(Collider other)
        {
            if (_lockedByUnitId.HasValue) return;

            bool isOtherAUnit = other.gameObject.CompareTag(base._tag);
            if (isOtherAUnit == false) return;

            var unit = other.gameObject.GetComponent<ITriggerSignal>().GetUnit();
            _lockedByUnitId = unit.GetUnitId();
            base._onTriggerEntered?.Invoke(unit);
        }

        protected override void onTriggerExit(Collider other)
        {
            if (_lockedByUnitId.HasValue == false) return;

            bool isOtherAnUnit = other.gameObject.CompareTag(base._tag);
            if (isOtherAnUnit == false) return;

            int unitId = other.gameObject.GetComponent<ITriggerSignal>().GetUnit().GetUnitId();
            if (_lockedByUnitId.Value == unitId)
            {
                _lockedByUnitId = null;
                base._onTriggerExit?.Invoke();
            }
        }
    }
}