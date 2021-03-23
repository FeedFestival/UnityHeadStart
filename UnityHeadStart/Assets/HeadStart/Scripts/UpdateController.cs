using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateController : MonoBehaviour
{
    static UpdateController _updateController;
    public static UpdateController _ { get { return _updateController; } }

    void Awake()
    {
        _updateController = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
