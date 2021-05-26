using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTrash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.MinigameItem.Modelprefab)
        {
            GameObject Trash = Instantiate(GameManager.Instance.MinigameItem.Modelprefab, transform);
            SetLayerRecursively(Trash);
        }
    }

    //sets the layer of the spawned animal object, to make it visible in minigames
    void SetLayerRecursively(GameObject layerobj)
    {
        layerobj.layer = 8;
        foreach (Transform child in layerobj.transform)
        {
            SetLayerRecursively(child.gameObject);
        }
    }
}
