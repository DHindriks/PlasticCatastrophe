using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance = null;

    [SerializeField]
    GameObject DBGWindow;
    bool DBGToggle = true;

    [HideInInspector]
    public PlayerScript player;

    public List<character> CharList;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.C))
        {
            if (DBGToggle)
            {
                DBGWindow.SetActive(true);
            }else
            {
                DBGWindow.SetActive(false);
            }
            DBGToggle = !DBGToggle;
        }
    }

    public void SwitchScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}

[Serializable]
public class character
{
    public string Name;
    public int ID; 
    public GameObject Prefab;
}
