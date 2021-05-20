using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGroundTrash : MonoBehaviour
{
    [SerializeField]
    GameObject Prefab;

    ParticleSystem Ps;
    List<ParticleSystem.Particle> Enter;
    void Awake()
    {
        Ps = GetComponent<ParticleSystem>();
        Enter = new List<ParticleSystem.Particle>();
    }

    void OnValidate()
    {
        Ps = GetComponent<ParticleSystem>();
        var trigger = Ps.trigger;
        if (trigger.enter != ParticleSystemOverlapAction.Callback)
        {
            trigger.enter = ParticleSystemOverlapAction.Callback;
        }
    }

    void OnParticleTrigger()
    {
        // get
        int numInside = Ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, Enter);

        // on enter
        for (int i = 0; i < numInside; i++)
        {
            ParticleSystem.Particle particle = Enter[i];

            Instantiate(Prefab, particle.position, Quaternion.identity);
        }
    }
}
