using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceMiniGame : MonoBehaviour
{

    [SerializeField]
    int Lives;

    Rigidbody rb;

    [SerializeField]
    Rigidbody TrashRB;

    Vector3 LocalTrashPos;

    [SerializeField]
    Rigidbody WeightRB;

    bool GameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        LocalTrashPos = TrashRB.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector3.right, ForceMode.Acceleration);
        if (Input.GetMouseButton(0) && !GameOver)
        {
            if (Input.mousePosition.x < Screen.width / 2)
            {
                rb.AddTorque(Vector3.forward * 2, ForceMode.Acceleration);
            }
            if (Input.mousePosition.x > Screen.width / 2)
            {
                rb.AddTorque(Vector3.back * 2, ForceMode.Acceleration);
            }
        }

        if (transform.rotation.z > 0.6f && !GameOver|| transform.rotation.z < -0.6f && !GameOver)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX;
            TrashRB.isKinematic = false;
            GameOver = true;
            Lives--;
            Invoke("ResetMinigame", 2);
        }
    }

    public void ResetMinigame()
    {
        if (Lives <= 0)
        {

        }else
        {
            GameOver = false;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            transform.position = TrashRB.transform.localPosition;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            WeightRB.transform.rotation = Quaternion.Euler(0, 0, 0);

            TrashRB.isKinematic = true;
            TrashRB.transform.rotation = Quaternion.Euler(0, 0, 0);
            TrashRB.transform.localPosition = LocalTrashPos;

        }
    }
}
