using UnityEngine;

namespace Assets.HeadStart.Core
{
    // public enum SessionState
    // {
    //     Playing, Analizing
    // }

    public class CoreSession : MonoBehaviour
    {
        private static CoreSession _coreSession;
        public static CoreSession _ { get { return _coreSession; } }
        void Awake()
        {
            DontDestroyOnLoad(this);
            _coreSession = this;
        }
        public SessionOpts SessionOpts;
    }
}
