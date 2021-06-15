using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float dragSpeed = 2;

    bool ControlsEnabled = true;

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
        GameManager.Instance.overworldCam = this;
    }

    void Update()
    {
        //lerp to target position
        if (FollowTarget != null)
        {
            transform.position = Vector3.Lerp(this.transform.position, FollowTarget.transform.position, 4 * Time.deltaTime);
        }

        // Pinch to zoom
        if (Input.touchCount == 2 && ControlsEnabled)
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
         else if (ControlsEnabled)
         {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            Zoom(scroll, 5);

            if (Input.GetKeyDown(KeyCode.Equals))
            {
                Zoom(1, 5);
            }
            else if (Input.GetKeyDown(KeyCode.Minus))
            {
                Zoom(-1, 5);
            }

            //rotating the camera
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction, Color.green);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "TrashObj" && GameManager.Instance.player.CurrentChar.Energy >= 5 && !GameManager.Instance.MinigamePlaying)
                    {
                        Zoom(40, 1);
                        ControlsEnabled = false;
                        FollowTarget = hit.transform.gameObject;
                        Invoke("StartMinigame", 1);
                        Destroy(hit.transform.gameObject, 2);
                    }else if (GameManager.Instance.player.CurrentChar.Energy < 5)
                    {
                        GameManager.Instance.GenPopup(GameManager.Instance.player.CurrentChar.Name + " is too tired!", GameManager.Instance.player.CurrentChar.Name + " needs time to rest to regain energy.", GameManager.Instance.player.CurrentChar.Portrait);
                    }
                }
                dragOrigin = Input.mousePosition;
                return;
            }

            if (!Input.GetMouseButton(0)) return;

            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(0, -pos.x * dragSpeed, 0);

            rb.AddTorque(move, ForceMode.Force);
         }


    }

    void StartMinigame()
    {
        GameManager.Instance.StartMinigame(FollowTarget.GetComponent<TrashObjectScript>().Containsitem);
        cam.enabled = false;
    }

    public void SetControls (bool enabled)
    {
        ControlsEnabled = enabled;
    }

    public void ResetCam()
    {
        cam.enabled = true;
        FollowTarget = GameManager.Instance.player.gameObject;
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