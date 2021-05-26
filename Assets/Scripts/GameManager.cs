

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance = null;

    [SerializeField]
    GameObject DBGWindow;
    bool DBGToggle = true;

    [HideInInspector]
    public PlayerScript player;

    [HideInInspector]
    public CameraControl overworldCam;

    public List<character> CharList;

    public InventoryObject inventory;

    [HideInInspector]
    public ItemObject MinigameItem;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.C))
        {

            ToggleDB();
        }
    }


    public void ToggleDB()
    {
        if (DBGToggle)
        {
            DBGWindow.SetActive(true);
        }
        else
        {
            DBGWindow.SetActive(false);
        }
        DBGToggle = !DBGToggle;
    }

    public void SwitchScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    //decides which minigame the game starts, based on player character
    public void StartMinigame(ItemObject item)
    {
        MinigameItem = item;

        if (player.CurrentChar.MinigameSceneName != null)
        {
            SceneManager.LoadScene(player.CurrentChar.MinigameSceneName, LoadSceneMode.Additive);
        }
        //switch (player.CurrentChar.ID)
        //{
        //    case 0:
        //        SceneManager.LoadScene("MinigameSlingShot", LoadSceneMode.Additive);
        //        break;

        //    case 1:
        //        SceneManager.LoadScene("MinigameBalance", LoadSceneMode.Additive);
        //        break;

        //    case 2:
        //        SceneManager.LoadScene("MinigameDrop", LoadSceneMode.Additive);
        //        break;

        //    default:
        //        Debug.LogError("Animal ID: " + player.CurrentChar.ID + " not found, starting slingshot minigame.");
        //        SceneManager.LoadScene("MinigameSlingShot", LoadSceneMode.Additive);
        //        break;
        //}
    }

    //adds item that was temporarely stored to the players inventory
    public void PickupItem()
    {
        inventory.AddItem(MinigameItem, 1);
        MinigameItem = null;
    }

    public void RemoveSaves()
    {
        SaveSystem.ResetSaves();
    }
}

public enum TrashObjs
{
    PWrapper,
    CigaretteBut,
    PBottle,
    Can,
    PBag,
    PCup,
    BottleCap
}
[Serializable]
public struct ItemXPData
{
    public TrashObjs obj;
    public int XPGained;
}

[Serializable]
public class character
{
    public string Name;
    public int ID;
    public GameObject Prefab;
    public string MinigameSceneName;
    [Space(5)]
    public int Level;
    public int NextLevelReq;
    public int Exp;
    [Space(5)]
    public int BaseLevelReq;
    public int GrowthRate;
    public List<ItemXPData> XpList;
}
