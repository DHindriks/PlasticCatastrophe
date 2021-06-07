using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public GameObject Modelprefab;
    public Sprite sprite;
    public TrashObjs type;
    [TextArea(15,20)]
    public string description;
}
