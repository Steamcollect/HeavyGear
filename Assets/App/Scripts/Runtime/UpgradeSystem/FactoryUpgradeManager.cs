using BT.Save;
using UnityEngine;

public class FactoryUpgradeManager : MonoBehaviour
{
    //[Header("Settings")]

    //[Header("References")]

    [Space(10)]
    // RSO
    public RSO_FactoryUpgradeData rsoFactoryUpgradeData;

    // RSF
    // RSP

    //[Header("Input")]
    [Header("Output")]
    [SerializeField] RSE_SaveData rseSaveData;

    private void OnEnable()
    {
        rsoFactoryUpgradeData.OnChanged += SaveDataChanged;
    }
    private void OnDisable()
    {
        rsoFactoryUpgradeData.OnChanged -= SaveDataChanged;
    }

    
    //A changer pour éviter d'écrire souvent sur le téléphone
    private void SaveDataChanged()
    {
        rseSaveData.Call();
    }
}