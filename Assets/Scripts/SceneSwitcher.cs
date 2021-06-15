using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public GameObject loadingScreenObj;
    public Slider slider;

    AsyncOperation async;

    void Start()
    {
        Invoke("LoadIngame", 5);
    }

    void LoadIngame()
    {
        LoadScreen(1);
    }

    public void LoadScreen(int LVL)
    {
        StartCoroutine(LoadingScreen(LVL));
    }

    IEnumerator LoadingScreen(int lvl)
    {
        //loadingScreenObj.SetActive(true);
        async = SceneManager.LoadSceneAsync(lvl);
        async.allowSceneActivation = false;

        while (async.isDone == false)
        {
            slider.value = async.progress;
            if (async.progress == 0.9f)
            {
                slider.value = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    /*[SerializeField]
    string Scenename;

    void Start()
    {
        if (Scenename != "")
        {
            StartCoroutine(LoadGame());
        }
    }

    IEnumerator LoadGame()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Scenename);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }


    public void SwitchScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void SwitchSceneAdditive(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }*/

}

