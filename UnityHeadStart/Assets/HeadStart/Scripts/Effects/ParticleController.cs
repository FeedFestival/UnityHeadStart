using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using System;
using Assets.HeadStart.Core;
using UniRx;
using GameScrypt.GSUtils;

public class ParticleController : MonoBehaviour
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "2.2.0";
#pragma warning restore 0414 //
    [Range(0.0f, 200.0f)]
    public float Size = 100.0f;
    public bool Set3DSize;
    public List<ParticleSystem> ParticleSystems;
    public List<float> OriginalSizes;
    public List<Vector3> Original3DSizes;
    public List<Burst> OriginalEmits;
    public int Id;
    public bool AvailableInPool;
    public float TimeLength;
    //
    private Subject<bool> _playSub__ = new Subject<bool>();

    void Start()
    {
        ChangeSize(init: true);
    }

    public void Init()
    {
        Id = 1; // Main.S.Game.GetUniqueId();
        AvailableInPool = true;
        gameObject.name = gameObject.name + "    " + Id;

        _playSub__
            .Delay(TimeSpan.FromMilliseconds(10))
            .Subscribe((bool _) =>
            {
                InternalPlay();
            });
    }

    public void ChangeSize(bool init = false)
    {
        if (init)
        {
            TimeLength = 0f;
            OriginalSizes = new List<float>();
            Original3DSizes = new List<Vector3>();
            OriginalEmits = new List<Burst>();
        }
        foreach (ParticleSystem ps in ParticleSystems)
        {
            MainModule module = ps.main;
            if (TimeLength < module.duration)
            {
                TimeLength = module.duration;
            }

            SetEmission(ps.emission);

            if (module.startSize3D)
            {
                if (Set3DSize == false)
                {
                    continue;
                }
                SetParticle3DSize(module, init);
            }
            else
            {
                if (init)
                {
                    OriginalSizes.Add(module.startSize.constant);
                }
                float newSize = GSPercent.Find(Size, module.startSize.constant);
                module.startSize = newSize;
            }
        }
    }

    public void SetAutoplay(bool on)
    {
        if (on == false)
        {
            foreach (ParticleSystem ps in ParticleSystems)
            {
                MainModule module = ps.main;
                module.loop = false;
                EmissionModule emModule = ps.emission;
                emModule.enabled = false;
            }
        }
    }

    public virtual void Play()
    {
        _playSub__.OnNext(true);
    }

    public virtual void Play(Vector2 point)
    {
        transform.position = new Vector3(point.x, point.y, 0);
        _playSub__.OnNext(true);
    }

    public void InternalPlay()
    {
        int index = 0;
        foreach (ParticleSystem ps in ParticleSystems)
        {
            ps.Emit(OriginalEmits[index].maxCount);
            index++;
        }

        __.Timeout.RxSeconds(() =>
        {
            AvailableInPool = true;
        }, TimeLength);
    }

    private void SetParticle3DSize(MainModule module, bool init)
    {
        if (module.startSize.mode == ParticleSystemCurveMode.TwoConstants)
        {
            if (init)
            {
                Original3DSizes.Add(new Vector3(module.startSize.constantMax, module.startSize.constantMax, module.startSize.constantMax));
                Original3DSizes.Add(new Vector3(module.startSize.constantMin, module.startSize.constantMin, module.startSize.constantMin));
            }
            float newSizeMax = GSPercent.Find(Size, module.startSize.constantMax);
            float newSizeMin = GSPercent.Find(Size, module.startSize.constantMin);
            module.startSizeX = new MinMaxCurve(newSizeMax, newSizeMin);
            module.startSizeY = new MinMaxCurve(newSizeMax, newSizeMin);
            module.startSizeZ = new MinMaxCurve(newSizeMax, newSizeMin);
        }
    }

    private void SetEmission(EmissionModule emModule)
    {
        Burst burst = emModule.GetBurst(0);
        OriginalEmits.Add(burst);
    }
}
