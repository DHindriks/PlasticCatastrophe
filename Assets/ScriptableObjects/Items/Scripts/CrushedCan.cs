using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Crashed can", menuName = "Inventory system/Items/Crashed can")]
public class CrushedCan : ItemObject
{
    public void Awake()
    {
        type = TrashObjs.Can;
    }
}
