using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlingShotGoal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerObj")
        {
            //TODO: add xp based on character used, xp list can be found in GameManager>Charlist>XPlist 
            foreach (ItemXPData data in GameManager.Instance.player.CurrentChar.XpList)
            {
                if (data.obj == GameManager.Instance.MinigameItem.type)
                {
                    GameManager.Instance.player.AddExp(data.XPGained);
                    Debug.Log(GameManager.Instance.player.CurrentChar.Name + " gained " + data.XPGained + " XP for picking up: " + data.obj.ToString());
                    break;
                }
            }
            GameManager.Instance.EndMinigame(true);
            EndLevel();

        }
            

    }

    void EndLevel()
    {
        //unload this scene.
        SceneManager.UnloadSceneAsync(gameObject.scene);

        GameManager.Instance.overworldCam.ResetCam();
    }
}
