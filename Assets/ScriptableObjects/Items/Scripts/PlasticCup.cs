using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Plastic cup", menuName = "Inventory system/Items/Plastic cup")]

public class PlasticCup : ItemObject
{
    public void Awake()
    {
        type = TrashObjs.PCup;
    }
}
