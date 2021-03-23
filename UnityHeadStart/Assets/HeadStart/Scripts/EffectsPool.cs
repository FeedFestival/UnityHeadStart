using System.Collections.Generic;
using UnityEngine;

public class EffectsPool : MonoBehaviour
{
    public List<ParticleController> ParticleControllers;
    private int _particleCount;

    public void GenerateParticleControllers()
    {
        _particleCount = 3;

        if (ParticleControllers == null)
        {
            ParticleControllers = new List<ParticleController>();
        }

        for (int i = 0; i < _particleCount; i++)
        {
            // var go = HiddenSettings._.GetAnInstantiated(PrefabBank._.SmallHitParticle);
            // ParticleController pc = go.GetComponent<ParticleController>();
            // pc.Init();
            // pc.transform.SetParent(transform);
            // pc.SetAutoplay(on: false);
            // ParticleControllers.Add(pc);
        }
    }

    public ParticleController GetParticle()
    {
        int index = ParticleControllers.FindIndex(pc => pc.AvailableInPool == true);
        ParticleControllers[index].AvailableInPool = false;
        return ParticleControllers[index];
    }
}
