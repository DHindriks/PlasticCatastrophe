using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Location;

public class TrashObjectScript : MonoBehaviour
{
    [SerializeField]
    int Maxdistance;

    public List<ItemObject> ItemList;
    [Space(20)]
    public ItemObject Containsitem;

    void Start()
    {
        Containsitem = ItemList[Random.Range(0, ItemList.Count)];
        InvokeRepeating("CheckDespawn", 10, 20);
    }

    //Checks if object instance is too far from the player, if so, it will delete itself.
    void CheckDespawn()
    {
        if (Vector3.Distance(transform.position, GameManager.Instance.player.transform.position) >= Maxdistance)
        {
            Destroy(this.gameObject);
        }
    }

}
