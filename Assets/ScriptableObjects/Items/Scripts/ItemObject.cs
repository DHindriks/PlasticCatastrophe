using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public GameObject Modelprefab;
    public TrashObjs type;
    [TextArea(15,20)]
    public string description;
}
