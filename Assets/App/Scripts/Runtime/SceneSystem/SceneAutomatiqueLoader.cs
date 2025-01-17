using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAutomatiqueLoader : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string sceneName;
    [SerializeField] private float delayBeforeLoad;

    private void Start()
    {
        StartCoroutine(Utils.Delay(delayBeforeLoad, LoadScene));
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
    
}