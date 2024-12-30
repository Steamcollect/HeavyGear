using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    //[Header("Settings")]
    private string sceneToLoad;
    private string currentSceneLoaded;
    private bool isLoadingScene;

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
        rseLoadNewScene.action += LoadAdditiveNewScene;
    }

    private void OnDisable()
    {
        rseLoadNewScene.action -= LoadAdditiveNewScene;
    }

    void LoadAdditiveNewScene(string levelName)
    {
        if (isLoadingScene)
        {
            Debug.LogWarning("You try to load a new scene while the process is already runing!");
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
        bool sceneFound = false;
        
        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (sceneToLoad == System.IO.Path.GetFileNameWithoutExtension(scene.path)) sceneFound = true;
        }

        if (!sceneFound)
        {
            OnSceneLoadFailed();
            return;
        }
        
        StartCoroutine(Utils.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive, OnSceneLoaded));
    }

    void OnSceneLoadFailed()
    {
        isLoadingScene = false;
        currentSceneLoaded = "";
        Debug.LogWarning($"Scene {sceneToLoad} is not loaded cause not exist");
    }

    void OnSceneLoaded()
    {
        isLoadingScene = false;
        currentSceneLoaded = sceneToLoad;
        print($"Scene {sceneToLoad} Loaded");
    }
}