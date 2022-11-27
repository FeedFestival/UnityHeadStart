using GameScrypt.Interfaces;
using UnityEngine;

namespace GameScrypt.GSUnit
{
    public class GSSignalController : MonoBehaviour
    {
        public GameObject InteractTriggerSignal;
        private ITriggerSignal _interactTriggerSignal;

        internal void Init(GSUnit unit)
        {
            this.loadSignalsComponents();

            _interactTriggerSignal.Init(unit);
        }

        private void loadSignalsComponents()
        {
            _interactTriggerSignal = InteractTriggerSignal.GetComponent<ITriggerSignal>();
        }
    }
}