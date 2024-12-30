using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventoryMachine : MonoBehaviour
{
    [Header("References")]
    // RSO
    [SerializeField] private RSO_ContentSaved rsoContentSaved;
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] private RSE_AddMachineToInventory rseAddMachineInventory;

    [Header("Output")]
    [SerializeField] private RSE_UpdateMachineInventory rseUpdateMachineInventory;

    public List<InventoryMachineData> content;

    public List<InventoryMachineData> Content
    {
        get => content;
        set
        {
            content = SortMachinesByRarity(value);
            rseUpdateMachineInventory.Call(content);
        }
    }

    private void OnEnable()
    {
        rseAddMachineInventory.action += AddMachineToInventory;
    }
    private void OnDisable()
    {
        rseAddMachineInventory.action -= AddMachineToInventory;
    }

    private void Start()
    {
        Content = content;
        //Content = rsoContentSaved.Value.inventoryMachineData;
    }

    void AddMachineToInventory(SSO_MachinePlacementData machine)
    {
        InventoryMachineData data = Content.Find(x => x.machine == machine);
        if (data != null)
        {
            data.amountAquired++;

            if(data.amountAquired >= data.maxAmount) LevelUpMachine(data);
        }
        else
        {
            Content.Add(new InventoryMachineData { machine = machine, amountAquired = 1, machineLevel = 1 });
        }
    }

    private List<InventoryMachineData> SortMachinesByRarity(List<InventoryMachineData> machines)
    {
        return machines.OrderByDescending(x => x.machine.machineRarity).ToList();
    }

    void LevelUpMachine(InventoryMachineData data)
    {
        data.amountAquired = 1;
        data.machineLevel++;
        // Upgrade statistics
    }
}