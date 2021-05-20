using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Plastic Wrapper", menuName = "Inventory system/Items/Plastic wrapper")]
public class PlasticWrapper : ItemObject
{
    public void Awake()
    {
        type = TrashObjs.PWrapper;
    }
}
