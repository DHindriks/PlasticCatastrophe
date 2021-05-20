using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Plastic Bottle", menuName = "Inventory system/Items/Plastic bottle")]
public class PlasticBottle : ItemObject
{
    public void Awake()
    {
        type = TrashObjs.PBottle;
    }
}
