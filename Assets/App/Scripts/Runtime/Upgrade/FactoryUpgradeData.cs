using UnityEngine;

[System.Serializable]
public class FactoryUpgradeData
{
    public float coinsMultiplier = 1;
    public float giftSpawnProbability = 0;

    [Space(7)]
    public float automationPercentage = 0;
    public float machinePowerMultiplier = 1;

    [Space(7)]
    public float sellerCapacityMultiplier = 1;
    public float sellerSpeedMultiplier = 1;

    [Space(7)]
    public float minerSpeedMultiplier = 1;

    [Space(7)]
    public float conveyorSpeedMultiplier = 1;
}