using BT.Save;
using UnityEngine;
public class FactoryUpgradeManager : MonoBehaviour
{
    //[Header("Settings")]

    //[Header("References")]

    [Space(10)]
    // RSO
    public RSO_UpgradeData rsoUpgradeData;

    // RSF
    // RSP

    //[Header("Input")]
    [Header("Output")]
    [SerializeField] RSE_CommandEvent rseSaveData;

    private void OnEnable()
    {
        rsoUpgradeData.OnChanged += SaveData;
    }
    private void OnDisable()
    {
        rsoUpgradeData.OnChanged -= SaveData;
    }

    void SaveData()
    {
        rseSaveData.Call();
    }
}