using UnityEngine.Events;

namespace GameScrypt.Interfaces
{
    public interface ITimeout
    {
        public void Milliseconds(UnityAction callback, int? milliseconds = null);
        public void Seconds(UnityAction callback, float? seconds = null);
        public void RxMilliseconds(UnityAction callback, int? milliseconds = null);
        public void RxSeconds(UnityAction callback, float? seconds = null);
    }
}