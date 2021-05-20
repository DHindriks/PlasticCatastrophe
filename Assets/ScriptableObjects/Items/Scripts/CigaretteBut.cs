using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Cigarette But", menuName = "Inventory system/Items/Cigarette But")]
public class CigaretteBut : ItemObject
{
    public void Awake()
    {
        type = TrashObjs.CigaretteBut;
    }
}
