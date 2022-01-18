using System.Collections.Generic;
using UnityEngine;


public enum ParticleType
{
    Test
}

public class EffectsPool : MonoBehaviour
{
    public Dictionary<ParticleType, List<ParticleController>> ParticleControllers;

    public virtual void GenerateParticleControllers()
    {
    }

    public ParticleController GetParticle(ParticleType particleType)
    {
        int index = ParticleControllers[particleType].FindIndex(pc => pc.AvailableInPool == true);
        ParticleControllers[particleType][index].AvailableInPool = false;
        return ParticleControllers[particleType][index];
    }
}
