using System.Collections.Generic;
using UnityEngine;

public class EffectsPool : MonoBehaviour
{
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
