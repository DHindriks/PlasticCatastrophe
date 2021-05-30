using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShotScript : MonoBehaviour
{
    Rigidbody ballRB;
    [SerializeField]
    public Rigidbody OriginRB;

    [SerializeField]
    public Camera cam;

    public SlingShotMinigame MinigameManager;

    bool IsShot = false;

    void Start()
    {
        ballRB = GetComponent<Rigidbody>();
    }

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
        }else if (ballRB.position.y < -10)
        {
            MinigameManager.RespawnBall();
            Destroy(gameObject);
        }

        //shoots the object
        if (Input.GetMouseButtonUp(0) && Vector3.Distance(ballRB.position, OriginRB.position) > 0.3f)
        {
            IsShot = true;
            Invoke("DestroyJoint", 0.2f);
            ballRB.isKinematic = false;
            ballRB.AddRelativeTorque(new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10)), ForceMode.VelocityChange);
        }
    }

    void DestroyJoint()
    {
        Destroy(GetComponent<SpringJoint>());
    }
}
