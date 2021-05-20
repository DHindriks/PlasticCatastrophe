using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShotScript : MonoBehaviour
{
    [SerializeField]
    Rigidbody ballRB;
    [SerializeField]
    Rigidbody OriginRB;

    [SerializeField]
    Camera cam;

    bool IsShot = false;
    // Update is called once per frame
    void Update()
    {
        //calculate mouse position on the screen
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Vector3.Distance(cam.transform.position, ballRB.position) - cam.nearClipPlane;
        mousePos = cam.ScreenToWorldPoint(mousePos);

        //if ball hasn't been shot yet, move the ball to player's mouse location, max 2 units from the slingshot origin.
        if (Input.GetMouseButton(0) && !IsShot)
        {
            if (Vector3.Distance(mousePos, OriginRB.position) > 2)
            {
                ballRB.position = OriginRB.position + (mousePos - OriginRB.position).normalized * 2;
            }else
            {
                ballRB.position = mousePos;
            }
        }else if (IsShot && Vector3.Distance(ballRB.position, OriginRB.position) < 0.5f)
        {
            //destroy the joint after the object has been shot
            Destroy(GetComponent<SpringJoint>());
        }

        //shoots the object
        if (Input.GetMouseButtonUp(0) && Vector3.Distance(ballRB.position, OriginRB.position) > 0.3f)
        {
            IsShot = true;
            ballRB.isKinematic = false;
        }
    }
}
