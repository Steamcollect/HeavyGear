using UnityEngine.Serialization;

[System.Serializable]
public class InventoryMachineData
{
    public SSO_MachinePlacementData machine;
    public int amountAquired,maxAmount;
    public int machineLevel;
}