using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomFactGen : MonoBehaviour
{
    [SerializeField]
    List<string> FactList;

    [SerializeField]
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = FactList[Random.Range(0, FactList.Count)];
    }
}
