using UnityEngine;

namespace Assets.HeadStart.Core
{
    public class CoreSession : MonoBehaviour
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.2.0";
#pragma warning restore 0414 //
        private static CoreSession _coreSession;
        public static CoreSession S { get { return _coreSession; } }
        void Awake()
        {
            DontDestroyOnLoad(this);
            _coreSession = this;
        }
        public SessionOpts SessionOpts;
    }
}
