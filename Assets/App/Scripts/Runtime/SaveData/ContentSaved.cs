using System;
using System.Collections.Generic;
using BigFloatNumerics;

namespace BT.Save
{
    [System.Serializable]
    public class ContentSaved
    {
        //Machines
        public FactoryUpgradeData factoryUpgradeData;
        public List<InventoryMachineData> inventoryMachineData = new List<InventoryMachineData>();
        public MachineStatisticsUpgrade machineStatisticsUpgrade;
        public Dictionary<int, int> machineButtonsUpgrades = new Dictionary<int, int>();

        // Coins
        public string coinAmount = "0e0";
        public string coinPerMin = "0e0";
        public string lastDateTimeQuit;
        public int idleDelay = 300; // In minutes

        //Level Factory
        public string currentFactory;
        public int currentCoinLevel;
    }
}