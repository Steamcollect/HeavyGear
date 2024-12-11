using System.Collections.Generic;

namespace BT.Save
{
    [System.Serializable]
    public class ContentSaved
    {
        public FactoryUpgradeData factoryUpgradeData;
        public List<InventoryMachineData> inventoryMachineData = new List<InventoryMachineData>();
    }
}