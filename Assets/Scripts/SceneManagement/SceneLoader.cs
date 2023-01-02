using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Listening to: ")]
    [SerializeField]
    private StringEventChannelSO _loadRequestEvent;
    public LoadingScreen loadingScreen;
    private bool _isLoading;
    private IEnumerator _coroutine;
    [SerializeField]
    private ScreenFader _fader;

    void OnEnable()
    {
        _loadRequestEvent.OnEventRaised += OnLoadRequested;
    }

    void OnDisable()
    {
        _loadRequestEvent.OnEventRaised -= OnLoadRequested;
    }

    void Start()
    {
        loadingScreen.gameObject.SetActive(false);
        _fader.gameObject.SetActive(false);
        _isLoading = false;
    }
    
    void OnLoadRequested(Scene scene)
    {
        OnLoadRequested(scene.name);
    }
    void OnLoadRequested(string name)
    {
        if (_isLoading)
        {
            return;
        }

        _isLoading = true;
        _coroutine = LoadScene(name);
        StartCoroutine(_coroutine);
    }

    IEnumerator LoadScene(string name)
    {
        _fader.gameObject.SetActive(true);
        _fader.FadeOut();
        while (!_fader.IsComplete)
        {
            yield return null;
        }
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        PlayLoadingScreen();
        while (!loadOp.isDone)
        {
            yield return null;
        }
        StopLoadingScreen();
        _fader.FadeIn();
        while (!_fader.IsComplete)
        {
            yield return null;
        }
        _fader.gameObject.SetActive(false);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));
        _isLoading = false;
    }

    void PlayLoadingScreen()
    {
        loadingScreen.gameObject.SetActive(true);
        loadingScreen.Play();
    }

    void StopLoadingScreen()
    {
        loadingScreen.Stop();
        loadingScreen.gameObject.SetActive(false);
    }
}
