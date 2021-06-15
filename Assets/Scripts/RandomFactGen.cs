using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomFactGen : MonoBehaviour
{
    [SerializeField]
    List<Sprite> FactList;

    [SerializeField]
    Image img;

    // Start is called before the first frame update
    void Start()
    {
        img.sprite = FactList[Random.Range(0, FactList.Count)];
    }
}
