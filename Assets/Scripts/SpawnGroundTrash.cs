using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGroundTrash : MonoBehaviour
{
    [SerializeField]
    GameObject Prefab;

    [SerializeField]
    int MaxGround;

    ParticleSystem Ps;
    List<ParticleSystem.Particle> Enter;

    List<GameObject> CurrentGroundTrash = new List<GameObject>();
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

            GameObject NewTrash = Instantiate(Prefab, particle.position, Quaternion.identity);
            CurrentGroundTrash.Add(NewTrash);

            if (CurrentGroundTrash.Count > MaxGround)
            {
                for (int j = 0; j < CurrentGroundTrash.Count - MaxGround; j++)
                {
                    if (CurrentGroundTrash[j] != null)
                    {
                        CurrentGroundTrash[j].GetComponentInChildren<Animator>().SetBool("Despawning", true);
                        CurrentGroundTrash.RemoveAt(j);
                    }
                }
                        Debug.Log("despawning " + (CurrentGroundTrash.Count - MaxGround) + " Trash");
            }
        }
    }
}
