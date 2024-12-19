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
        public BigNumber coinAmount;
        public BigNumber coinPerMin;
        public DateTime lastDateTimeQuit;
        public int idleDelay = 300; // In minutes
    }
}