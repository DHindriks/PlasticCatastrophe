using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAnimalModel : MonoBehaviour
{
    GameObject Animal;
    [SerializeField]
    Transform Positioner;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance != null)
        {
            Animal = Instantiate(GameManager.Instance.player.CurrentChar.Prefab, transform);
            Animal.transform.position = Animal.transform.parent.position;
            Animal.transform.rotation = Animal.transform.parent.rotation;
            SetLayerRecursively(Animal);

            if (Positioner != null)
            {
                Animal.transform.position = Positioner.transform.position;
                Animal.transform.rotation = Positioner.transform.rotation;
            }
        }
    }

    //sets the layer of the spawned animal object, to make it visible in minigames
    void SetLayerRecursively(GameObject layerobj)
    {
        layerobj.layer = 8;
        foreach(Transform child in layerobj.transform)
        {
            SetLayerRecursively(child.gameObject);
        }
    }
}
