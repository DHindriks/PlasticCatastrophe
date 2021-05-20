using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrash : MonoBehaviour
{
    [SerializeField]
    Vector3 Range;
    [SerializeField]
    bool SpawnBurst = true;
    [SerializeField]
    int SpawnAmount;
    [SerializeField]
    GameObject TrashPrefab;
    // Start is called before the first frame update
    void Start()
    {
        if (SpawnBurst)
        {
            InvokeRepeating("SpawnRandom", 0, 0.1f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Range);
    }

    void SpawnRandom()
    {
        Vector3 pos = transform.position + new Vector3(Random.Range(-Range.x / 2, Range.x / 2), 0, Random.Range(-Range.z / 2, Range.z / 2));
        Instantiate(TrashPrefab, pos, Quaternion.identity);
        if(SpawnBurst && --SpawnAmount <= 0)
        {
            CancelInvoke("SpawnRandom");
        }
    }
}
