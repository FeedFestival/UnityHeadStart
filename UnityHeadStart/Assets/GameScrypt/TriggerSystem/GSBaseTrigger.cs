using GameScrypt.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace GameScrypt.TriggerSystem
{
    public class GSBaseTrigger : MonoBehaviour
    {
        protected UnityAction<IUnit> _onTriggerEntered;
        protected UnityAction _onTriggerExit;
        protected string _tag;

        public void Init(int layer, string tag, UnityAction<IUnit> onTriggerEntered = null, UnityAction onTriggerExit = null)
        {
            this.setLayer(layer);
            this.setTag(tag);
            _onTriggerEntered = onTriggerEntered;
            _onTriggerExit = onTriggerExit;
        }

        public virtual void DestroyTrigger()
        {
            Destroy(this.gameObject);
        }

        public virtual void DisableTrigger()
        {
            this.gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            this.onTriggerEnter(other);
        }

        private void OnTriggerExit(Collider other)
        {
            this.onTriggerExit(other);
        }

        private void OnDestroy()
        {
            _onTriggerEntered = null;
            _onTriggerExit = null;
        }

        protected virtual void onTriggerEnter(Collider other)
        {
            var unit = other.gameObject.GetComponent<ITriggerSignal>().GetUnit();
            _onTriggerEntered?.Invoke(unit);
        }

        protected virtual void onTriggerExit(Collider other)
        {
            _onTriggerExit?.Invoke();
        }

        private void setLayer(int layer)
        {
            var isCorrectLayer = this.gameObject.layer == layer;
            if (isCorrectLayer == false)
            {
                this.gameObject.layer = layer;
            }
        }

        private void setTag(string tag)
        {
            _tag = tag;
            this.gameObject.tag = tag;
        }
    }
}
