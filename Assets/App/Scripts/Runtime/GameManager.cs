using System.Globalization;
using UnityEngine;
using WebSocketSharp;

public class GameManager : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private string defaultLevelName;
    [SerializeField] private string endLevelName;

    [Header("References")] 
    [SerializeField] private RSO_Coins rsoCoins;
    [SerializeField] private RSO_ContentSaved rsoContentSaved;

    [Header("Output")]
    [SerializeField] private RSE_LoadNewScene rseLoadNewScene;
    
    private void Start()
    {
        LoadLastLevel();
    }

    private void LoadLastLevel()
    {
        if (rsoContentSaved.Value.currentFactory.IsNullOrEmpty())
        {
            if (defaultLevelName != "")
            {
                rsoContentSaved.Value.currentFactory = defaultLevelName;
                rseLoadNewScene.Call(defaultLevelName);
            }
            
        }
        else
        {
            rseLoadNewScene.Call(rsoContentSaved.Value.currentFactory);
        }
    }
}