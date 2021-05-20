using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceGameCamera : MonoBehaviour
{
    [SerializeField]
    Transform FollowTarget;
    // Update is called once per frame
    void Update()
    {
        if (FollowTarget != null)
        {
            transform.position = Vector3.Lerp(this.transform.position, FollowTarget.position, 4 * Time.deltaTime);
        }
    }
}
