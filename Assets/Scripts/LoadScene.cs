using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public GameObject loadingScreen;
    public void Awake()
    {


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
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                yield return null;
            }
        }

        loadingScreen.gameObject.SetActive(false);
    }
}

