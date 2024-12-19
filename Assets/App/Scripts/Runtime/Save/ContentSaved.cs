using System;
using System.Collections.Generic;
using BigFloatNumerics;

namespace BT.Save
{
    [System.Serializable]
    public class ContentSaved
    {
        public FactoryUpgradeData factoryUpgradeData;
        public List<InventoryMachineData> inventoryMachineData = new List<InventoryMachineData>();

        // Coins
        public string coinAmount;
        public string coinPerMin;
        public string lastDateTimeQuit;
        public int idleDelay = 300; // In minutes
    }
}