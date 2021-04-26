using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(0, -pos.x * dragSpeed, 0);

        rb.AddTorque(move, ForceMode.Force);
    }


}