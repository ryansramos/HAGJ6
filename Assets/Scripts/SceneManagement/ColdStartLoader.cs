using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColdStartLoader : MonoBehaviour
{
#if UNITY_EDITOR
    void OnEnable()
    {
        LoadPersistentManagers();
    }

    void LoadPersistentManagers()
    {
        if (SceneManager.GetSceneByName("PersistentManagers").isLoaded)
        {   
            return;
        }   
        AsyncOperation loadOp = SceneManager.LoadSceneAsync("PersistentManagers", LoadSceneMode.Additive);
    }
#endif
}
