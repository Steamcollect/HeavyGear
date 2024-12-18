using System.Collections.Generic;
using UnityEngine;
public class InventoryUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject cardVisual;
    [SerializeField] private GameObject list;
    [Space(10)]
    [SerializeField] private RSE_UpdateMachineInventory rseUpdateMachineInventory;

    private void OnEnable()
    {
        rseUpdateMachineInventory.action += UpdateInventoryUI;
    }

    private void OnDisable()
    {
        rseUpdateMachineInventory.action -= UpdateInventoryUI;
    }

    public void UpdateInventoryUI(List<InventoryMachineData> machines)
    {
        Debug.Log("test");
        for (int i = 0; i < machines.Count; i++)
        {
            GameObject card = Instantiate(cardVisual, list.transform);
            CardUI cardUI = card.GetComponent<CardUI>();
            cardUI.SetCard(machines[i]);
        }
    }
}