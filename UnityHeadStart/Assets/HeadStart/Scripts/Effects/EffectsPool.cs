using System.Collections.Generic;
using UnityEngine;

public class EffectsPool : MonoBehaviour
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "2.2.0";
#pragma warning restore 0414 //
    public Dictionary<int, List<ParticleController>> ParticleControllers;

    public virtual void GenerateParticleControllers()
    {
    }

    public virtual ParticleController GetParticle(int particleType)
    {
        int index = ParticleControllers[particleType].FindIndex(pc => pc.AvailableInPool == true);
        ParticleControllers[particleType][index].AvailableInPool = false;
        return ParticleControllers[particleType][index];
    }
}
