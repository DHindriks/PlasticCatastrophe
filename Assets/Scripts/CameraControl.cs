using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float dragSpeed = 2;

    [SerializeField]
    GameObject FollowTarget;

    private Vector3 dragOrigin;
    Rigidbody rb;

    Camera cam;

    float MaxZoom = 40;
    float MinZoom = 95;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        //lerp to target position
        this.transform.position = Vector3.Lerp(this.transform.position, FollowTarget.transform.position, 4 * Time.deltaTime);

        // Pinch to zoom
        if (Input.touchCount == 2)
         {
            // get current touch positions
            Touch tZero = Input.GetTouch(0);
            Touch tOne = Input.GetTouch(1);

            // get touch position from the previous frame
            Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
            Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;
            float oldTouchDistance = Vector2.Distance(tZeroPrevious, tOnePrevious);
            float currentTouchDistance = Vector2.Distance(tZero.position, tOne.position);

             // get offset
             float deltaDistance = oldTouchDistance - currentTouchDistance;
             Zoom(deltaDistance, 0.1f);
         }
         else
         {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            Zoom(scroll, 5);
         }

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

    void Zoom(float deltaMagnitudeDiff, float speed)
    {
        cam.fieldOfView += deltaMagnitudeDiff * speed;
        // set min and max value of Clamp function upon your requirement
        cam.transform.localEulerAngles = new Vector3(Mathf.Lerp(70, 30, Mathf.InverseLerp(MinZoom, 50, cam.fieldOfView)), 0, 0);
        cam.transform.position = new Vector3(cam.transform.position.x, Mathf.Lerp(100, 30, Mathf.InverseLerp(MinZoom, 50, cam.fieldOfView)), cam.transform.position.z);
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, MaxZoom, MinZoom);
    }

}