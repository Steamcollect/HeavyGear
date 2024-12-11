using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Settings")]
    public SSO_MachinePlacementData[] machines;

    //[Header("References")]
    List<InventoryMachineData> content = new List<InventoryMachineData>();

    [Space(10)]
    // RSO
    [SerializeField] RSO_ContentSaved rsoContentSaved;
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_AddMachineToInventory rseAddSlot;
    //[Header("Output")]

    private void OnEnable()
    {
        rseAddSlot.action += AddMachine;
    }
    private void OnDisable()
    {
        rseAddSlot.action -= AddMachine;
    }

    private void Start()
    {
        content = rsoContentSaved.Value.inventoryMachineData;
    }

    void AddMachine(SSO_MachinePlacementData machine)
    {
        InventoryMachineData slot = content.Find(x => x.machine == machine);
        if (slot != null)
        {
            slot.amount++;

            // Level up
            if(slot.amount >= slot.maxAmount) LevelUp(slot);
        }
        else
        {
            content.Add(new InventoryMachineData { machine = machine, amount = 1, machineLevel = 1 });
        }
    }

    void LevelUp(InventoryMachineData slot)
    {
        slot.amount = 1;
        slot.machineLevel++;
        // Upgrade statistics
    }
}