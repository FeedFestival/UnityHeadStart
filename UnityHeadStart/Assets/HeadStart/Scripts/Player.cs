using UnityEngine;

namespace Assets.HeadStart.Core.Player
{
    public class Player : MonoBehaviour
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.0.8";
#pragma warning restore 0414 //
        void Start()
        {
            // Core.Inject(Dependency.SFX);
        }

        public PlayerMouse PlayerMouse;
        public PlayerIntention PlayerIntention;

        public virtual void Init()
        {

        }
    }

    public enum PlayerMouse
    {
        None
    }

    public enum PlayerIntention
    {
        None
    }
}
