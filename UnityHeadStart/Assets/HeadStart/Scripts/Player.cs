using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Start()
    {
        // Core.Inject(Dependency.SFX);
    }

    public PlayerMouse PlayerMouse;
    public PlayerIntention PlayerIntention;
}

public enum PlayerMouse
{
    None
}

public enum PlayerIntention
{
    None
}
