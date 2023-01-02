using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializationLoader : MonoBehaviour
{
    [SerializeField]
    private string _sceneToLoad;
    public LoadingScreen loadingScreen;
    void Start()
    {
        AsyncOperation pmLoad = SceneManager.LoadSceneAsync("PersistentManagers", LoadSceneMode.Additive);
        pmLoad.completed += LoadTitle;
    }

    void LoadTitle(AsyncOperation operation) => StartCoroutine(LoadTitleRoutine());
    IEnumerator LoadTitleRoutine()
    {
        AsyncOperation titleLoad = SceneManager.LoadSceneAsync(_sceneToLoad, LoadSceneMode.Additive);
        loadingScreen.Play();
        while (!titleLoad.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_sceneToLoad));
        loadingScreen.Stop();
        UnloadInitialization();
    }

    void UnloadInitialization()
    {
        SceneManager.UnloadSceneAsync("Initialization");
    }
}
