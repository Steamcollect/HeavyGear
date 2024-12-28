using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    //[Header("Settings")]
    private string sceneToLoad;
    private string currentSceneLoaded;
    bool isLoadingScene = false;

    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_LoadNewScene rseLoadNewScene;

    //[Header("Output")]

    private void OnEnable()
    {
        rseLoadNewScene.action += SetupNewLevel;
    }

    private void OnDisable()
    {
        rseLoadNewScene.action -= SetupNewLevel;
    }

    void SetupNewLevel(string levelName)
    {
        if (isLoadingScene)
        {
            Debug.LogError("You try to load a new scene while the process is already runing!");
            return;
        }
        
        isLoadingScene = true;
        sceneToLoad = levelName;
        UnloadCurrentScene();
    }

    void UnloadCurrentScene()
    {
        StartCoroutine(Utils.UnloadSceneAsync(currentSceneLoaded, LoadNewScene));
    }

    void LoadNewScene()
    {
        StartCoroutine(Utils.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive, OnSceneLoaded));
    }

    void OnSceneLoaded()
    {
        isLoadingScene = false;
        currentSceneLoaded = sceneToLoad;
        print($"Scene {sceneToLoad} Loaded");
    }
}