using System.Collections;
using UnityEngine.Events;

namespace GameScrypt.Core.Timeout
{
    public class GSTimer
    {
        public UnityAction Callback { get; set; }
        public IEnumerator WaitFunc { get; set; }
        public bool WaitOneFrame { get; set; }
        public float Seconds { get; set; }

        public GSTimer(UnityAction callback, float? seconds = null)
        {
            Callback = callback;
            WaitOneFrame = !seconds.HasValue;
            if (WaitOneFrame == false)
            {
                Seconds = seconds.Value;
            }
        }
    }
}
