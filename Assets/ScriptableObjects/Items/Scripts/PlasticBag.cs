using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Plastic bag", menuName = "Inventory system/Items/Plastic bag")]

public class PlasticBag : ItemObject
{
    public void Awake()
    {
        type = TrashObjs.PBag;
    }
}
