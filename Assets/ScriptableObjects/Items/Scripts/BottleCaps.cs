using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Bottle caps", menuName = "Inventory system/Items/Bottle caps")]
public class BottleCaps : ItemObject
{
    public void Awake()
    {
        type = TrashObjs.BottleCap;
    }
}
