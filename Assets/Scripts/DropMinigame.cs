using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMinigame : MonoBehaviour
{

    [SerializeField]
    GameObject DropObj;

    bool dropped = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !dropped)
        {
            drop();
        }
    }

    void drop()
    {
        dropped = true;
        DropObj.transform.SetParent(null);
        DropObj.GetComponent<Rigidbody>().isKinematic = false;
    }

}
