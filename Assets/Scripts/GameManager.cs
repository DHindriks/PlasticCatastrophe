

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance = null;
    public GameObject loadingScreen;
    [HideInInspector]
    public bool MinigamePlaying = false;

    [SerializeField]
    GameObject DBGWindow;
    bool DBGToggle = true;

    [HideInInspector]
    public PlayerScript player;

    [HideInInspector]
    public CameraControl overworldCam;

    [SerializeField]
    GameObject canvas;

    [SerializeField]
    GameObject PopupPrefab;

    [HideInInspector]
    public PollutionSystem PolSystem;

    public List<character> CharList;

    public InventoryObject inventory;

    public GameObject Sidebar;

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
        MinigamePlaying = true;
        MinigameItem = item;
        if (player.CurrentChar.CurrentPerk != null && player.CurrentChar.CurrentPerk.modifier == PerkModifiers.EnergyEfficiencyModifier)
        {
            player.TakeDamage((5 / 100) * 100 - (player.CurrentChar.CurrentPerk.value));

        }else
        {
            player.TakeDamage(5);
        }
        if (Sidebar.GetComponent<SideBarscript>().Opened)
        {
            Sidebar.GetComponent<SideBarscript>().ToggleSideBar();
        }
        Sidebar.GetComponent<SideBarscript>().SetSideButton(false);

        if (player.CurrentChar.MinigameSceneName != "")
        {
            SceneManager.LoadScene(player.CurrentChar.MinigameSceneName, LoadSceneMode.Additive);
        }
    }

    //adds item that was temporarely stored to the players inventory
    public void EndMinigame(bool GiveItem)
    {
        if (GiveItem)
        {
            GenPopup("Found a " + MinigameItem.name, MinigameItem.description, MinigameItem.sprite);
            if (player.CurrentChar.CurrentPerk != null && player.CurrentChar.CurrentPerk.modifier == PerkModifiers.TrashPickupModifier)
            {
                inventory.AddItem(MinigameItem, Mathf.RoundToInt(player.CurrentChar.CurrentPerk.value));
            }else
            {
                inventory.AddItem(MinigameItem, 1);
            }
            PolSystem.AddPollution(-10);
        }
        Sidebar.GetComponent<SideBarscript>().SetSideButton(true);
        MinigamePlaying = false;
        MinigameItem = null;
    }

    public void GenPopup(string Title, string Description, Sprite sprite)
    {
        overworldCam.SetControls(false);
        GameObject Popup = Instantiate(PopupPrefab, canvas.transform);
        Popup.GetComponent<Popup>().SetTitle(Title);
        Popup.GetComponent<Popup>().SetDesc(Description);
        Popup.GetComponent<Popup>().SetIcon(sprite);
        Popup.GetComponent<Popup>().Btn.onClick.AddListener(delegate () { overworldCam.SetControls(true); });
    }

    public void RemoveSaves()
    {
        SaveSystem.ResetSaves();
    }

    private void OnApplicationQuit()
    {
        SaveSystem.SaveAnimalData(player.CurrentChar);
    }

     /*public void Awake()
     {
         Instance = this;


         SceneManager.LoadSceneAsync((int)SceneIndexes.LoadScene, LoadSceneMode.Additive);

     }
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
     public void LoadGame()
     {
        loadingScreen.gameObject.SetActive(true);
         scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.LoadScene));
         scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.Ingame, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
     }

    public IEnumerator GetSceneLoadProgress()
    {
        for(int i=0; i<scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                yield return null;
            }
        }

        loadingScreen.gameObject.SetActive(false);
    }*/
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
    public Sprite Portrait;
    public string MinigameSceneName;
    public float Energy;
    public int LastUsed;
    [Space(5)]
    public int Level;
    public int NextLevelReq;
    public int Exp;
    [Space(5)]
    public int BaseLevelReq;
    public int GrowthRate;
    public List<ItemXPData> XpList;
    public Perk CurrentPerk;
    public List<Perk> PerkList;
}

public enum PerkModifiers
{
    None,
    XPModifier,
    EnergyRecoveryModifier,
    EnergyEfficiencyModifier,
    TrashPickupModifier,
    EnergyModifier
}

[Serializable]
public class Perk
{
    public int ID;
    public string Name;
    public string Desc;
    public PerkModifiers modifier;
    public bool Unlocked = false;
    public InventoryObject Cost;
    public float value;
    public Sprite Icon;
}
