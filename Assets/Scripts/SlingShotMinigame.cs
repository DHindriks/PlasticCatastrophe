using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlingShotMinigame : MonoBehaviour
{
    [SerializeField]
    int Lives;

    [SerializeField]
    GameObject TrashPrefab;

    [SerializeField]
    Rigidbody HookRB;

    [SerializeField]
    Camera camera;

    public void RespawnBall()
    {
        Lives--;
        if (Lives <= 0)
        {
            //unload this scene.
            SceneManager.UnloadSceneAsync(gameObject.scene);
            GameManager.Instance.overworldCam.ResetCam();
        }else
        {
            GameObject TrashObj = Instantiate(TrashPrefab);
            TrashObj.GetComponent<SlingShotScript>().cam = camera;
            TrashObj.GetComponent<SlingShotScript>().OriginRB = HookRB;
            TrashObj.GetComponent<SpringJoint>().connectedBody = HookRB;
            TrashObj.GetComponent<SlingShotScript>().MinigameManager = this;
        }


    }
  
}
