using System;
using System.Threading.Tasks;
using BT.Save;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Settings")] [SerializeField] private string defaultLevelName;
    [SerializeField] private string endLevelName;

    [Header("References")] [SerializeField]
    private RSO_Coins rsoCoins;

    [SerializeField] private RSO_ContentSaved rsoContentSaved;

    [Header("Input")] 
    [SerializeField] private RSE_NextLevelReached rseNextLevelReached;
    [SerializeField] private RSE_NextFactoryReached rseNextFactoryReached;
    [SerializeField] private RSE_LastFactoryReached rseLastFactoryReached;

    [Header("Output")] [SerializeField] private RSE_SaveData rseSaveData;
    [SerializeField] private RSE_LoadNewScene rseLoadNewScene;


    private void OnEnable()
    {
        rseNextLevelReached.action += OnNextLevelReached;
        rseLastFactoryReached.action += OnNextFactoryReached;
        rseNextFactoryReached.action += OnNextFactoryReached;
    }

    private void OnDisable()
    {
        rseNextLevelReached.action -= OnNextLevelReached;
        rseLastFactoryReached.action -= OnNextFactoryReached;
        rseNextFactoryReached.action -= OnNextFactoryReached;
    }

    private void Start()
    {
        LoadLastLevel();
    }

    private void LoadLastLevel()
    {
        if (rsoContentSaved.Value.currentFactory == "")
        {
            if (defaultLevelName != "") rseLoadNewScene.Call(defaultLevelName);
        }
        else
        {
            rseLoadNewScene.Call(rsoContentSaved.Value.currentFactory);
        }
    }

    private void OnNextFactoryReached()
    {
        // rsoCoins.Value = new(0);

    }

    private void OnNextLevelReached()
    {
        // rsoCoins.Value = new(0);

    }

    private async void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            await Task.Delay(100);
            rseSaveData.Call();
        }
    }

    private void OnApplicationQuit()
    {
        rseSaveData.Call();
    }
}