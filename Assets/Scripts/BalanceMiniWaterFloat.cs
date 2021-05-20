using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceMiniWaterFloat : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            other.GetComponent<Rigidbody>().AddForce(Vector3.up * 0.5f, ForceMode.VelocityChange);
        }
    }
}
