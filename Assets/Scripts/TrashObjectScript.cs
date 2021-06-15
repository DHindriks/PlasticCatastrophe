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

    public bool MarkForDestroy = false;

    void Start()
    {
        Containsitem = ItemList[Random.Range(0, ItemList.Count)];
        if (Containsitem.Modelprefab != null)
        {
            Instantiate(Containsitem.Modelprefab, transform);
            transform.GetChild(0).gameObject.SetActive(false);
        }
        InvokeRepeating("CheckDespawn", 10, 20);
    }

    //Checks if object instance is too far from the player, if so, it will delete itself.
    void CheckDespawn()
    {
        if (Vector3.Distance(transform.position, GameManager.Instance.player.transform.position) >= Maxdistance || MarkForDestroy)
        {
            Destroy(transform.parent.gameObject);
        }
    }

}
