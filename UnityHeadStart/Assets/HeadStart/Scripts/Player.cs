using UnityEngine;

namespace Assets.HeadStart.Core.Player
{

    public class Player : MonoBehaviour
    {
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
