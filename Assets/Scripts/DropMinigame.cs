using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DropMinigame : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    GameObject DropObj;

    [SerializeField]
    GameObject followtrgt;

    [SerializeField]
    GameObject Trashcan;

    int lives = 3;

    bool dropped = false;

    Vector3 OriginalTrashPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        OriginalTrashPos = DropObj.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (DropObj.transform.position.y < -5)
        {
            lives--;
            if (lives < 0)
            {
                //unload this scene.
                GameManager.Instance.overworldCam.ResetCam();
                GameManager.Instance.EndMinigame(false);
                SceneManager.UnloadSceneAsync(gameObject.scene);
            }else
            {
                dropped = false;
                DropObj.transform.SetParent(transform);
                DropObj.GetComponent<Rigidbody>().isKinematic = true;
                DropObj.transform.localPosition = OriginalTrashPos;
                DropObj.transform.localEulerAngles = Vector3.zero;
            }
        }

        rb.AddRelativeForce(Vector3.forward * 750 * Time.deltaTime);
        followtrgt.transform.position = Vector3.Lerp(transform.position, Trashcan.transform.position, 0.65f);
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
