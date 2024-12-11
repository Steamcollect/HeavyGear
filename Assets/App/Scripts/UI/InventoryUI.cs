using UnityEngine;
public class InventoryUI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject commonCardVisual;
    [SerializeField] private GameObject rareCardVisual;
    [SerializeField] private GameObject legendaryCardVisual;

    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    public void UpdateInventoryUI(SSO_MachinePlacementData[] machines)
    {
        Debug.Log(machines[0].machineName);
    }
}