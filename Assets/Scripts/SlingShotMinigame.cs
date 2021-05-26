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
            GameManager.Instance.overworldCam.ResetCam();
            GameManager.Instance.EndMinigame(false);
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }else
        {
            GameObject TrashObj = Instantiate(TrashPrefab);
            SceneManager.MoveGameObjectToScene(TrashObj, gameObject.scene);
            TrashObj.GetComponent<SlingShotScript>().cam = camera;
            TrashObj.GetComponent<SlingShotScript>().OriginRB = HookRB;
            TrashObj.GetComponent<SpringJoint>().connectedBody = HookRB;
            TrashObj.GetComponent<SlingShotScript>().MinigameManager = this;
        }


    }
  
}
