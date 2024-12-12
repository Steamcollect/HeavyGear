using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    //[Header("Settings")]
    string levelToLoad;
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
        levelToLoad = levelName;
        UnloadCurrentScene();
    }

    void UnloadCurrentScene()
    {
        Utils.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadNewScene);
    }

    void LoadNewScene()
    {
        Utils.LoadSceneAsync(levelToLoad, LoadSceneMode.Additive, OnSceneLoaded);
    }

    void OnSceneLoaded()
    {
        isLoadingScene = false;
        print($"Scene {levelToLoad} Loaded");
    }
}