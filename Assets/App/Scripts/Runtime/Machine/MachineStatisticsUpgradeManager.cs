using System.Collections;
using UnityEngine;
public class MachineStatisticsUpgradeManager : MonoBehaviour
{
    //[Header("Settings")]

    [Header("References")]

    //[Space(10)]
    // RSO
    [SerializeField] RSO_MachinesStatisticsUpgrades rsoUpgrades;
    [SerializeField] RSO_ContentSaved rsoContentSaved;

    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_MachinesUpgrade_MinerSpeedMult rseAddMinerSpeedMult;
    [SerializeField] RSE_MachinesUpgrade_MinerPowerMult rseAddMinerPowerMult;
    [SerializeField] RSE_MachinesUpgrade_MinerGiftProb rseAddMinerGiftProb;
    [SerializeField] RSE_MachinesUpgrade_MinerAutomation rseAddMinerAutomation;

    [SerializeField] RSE_MachinesUpgrade_SellerSpeedMult rseAddSellerSpeedMult;
    [SerializeField] RSE_MachinesUpgrade_SellerCapacityMult rseAddSellerCapacityMult;
    [SerializeField] RSE_MachinesUpgrade_SellerProfitMult rseAddSellerProfitMult;
    [SerializeField] RSE_MachinesUpgrade_SellerAutomation rseAddSellerAutomation;

    [SerializeField] RSE_MachinesUpgrade_PolisherSpeedMult rseAddPolisherSpeedMult;
    [SerializeField] RSE_MachinesUpgrade_PolisherPowerMult rseAddPolisherPowerMult;
    [SerializeField] RSE_MachinesUpgrade_PolisherAutomation rseAddPolisherAutomation;

    [SerializeField] RSE_MachinesUpgrade_ConveyorSpeedMult rseAddConveyorSpeedMult;

    //[Header("Output")]

    private void OnEnable()
    {
        rseAddMinerSpeedMult.action += AddMinerSpeedMult;
        rseAddMinerPowerMult.action += AddMinerPowerMult;
        rseAddMinerGiftProb.action += AddMinerGiftProb;
        rseAddMinerAutomation.action += AddMinerAutomation;

        rseAddSellerSpeedMult.action += AddSellerSpeedMult;
        rseAddSellerProfitMult.action += AddSellerProfitMult;
        rseAddSellerCapacityMult.action += AddSellerCapacityMult;
        rseAddSellerAutomation.action += AddSellerAutomation;

        rseAddPolisherSpeedMult.action += AddPolisherSpeedMult;
        rseAddPolisherPowerMult.action += AddPolisherPowerMult;
        rseAddPolisherAutomation.action += AddPolisherAutomation;

        rseAddConveyorSpeedMult.action += AddConveyorSpeedMult;
    }
    private void OnDisable()
    {
        rseAddMinerSpeedMult.action -= AddMinerSpeedMult;
        rseAddMinerPowerMult.action -= AddMinerPowerMult;
        rseAddMinerGiftProb.action -= AddMinerGiftProb;
        rseAddMinerAutomation.action -= AddMinerAutomation;

        rseAddSellerSpeedMult.action -= AddSellerSpeedMult;
        rseAddSellerProfitMult.action -= AddSellerProfitMult;
        rseAddSellerCapacityMult.action -= AddSellerCapacityMult;
        rseAddSellerAutomation.action -= AddSellerAutomation;

        rseAddPolisherSpeedMult.action -= AddPolisherSpeedMult;
        rseAddPolisherPowerMult.action -= AddPolisherPowerMult;
        rseAddPolisherAutomation.action -= AddPolisherAutomation;

        rseAddConveyorSpeedMult.action -= AddConveyorSpeedMult;
    }

    private void Start()
    {
        StartCoroutine(LateStart());
    }
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(.1f);
        rsoUpgrades.Value = rsoContentSaved.Value.machineStatisticsUpgrade;
    }

    #region Miner
    void AddMinerSpeedMult(float value)
    {
        rsoUpgrades.Value.minerSpeedMultiplier += value;
        rsoContentSaved.Value.machineStatisticsUpgrade = rsoUpgrades.Value;
    }
    void AddMinerPowerMult(float value)
    {
        rsoUpgrades.Value.minerPowerMultiplier += value;
        rsoContentSaved.Value.machineStatisticsUpgrade = rsoUpgrades.Value;
    }
    void AddMinerAutomation(float value)
    {
        rsoUpgrades.Value.minerAutomation += value;
        rsoContentSaved.Value.machineStatisticsUpgrade = rsoUpgrades.Value;
    }
    void AddMinerGiftProb(float value)
    {
        rsoUpgrades.Value.minerGiftluckProbability += value;
        rsoContentSaved.Value.machineStatisticsUpgrade = rsoUpgrades.Value;
    }
    #endregion
    
    #region Seller
    void AddSellerSpeedMult(float value)
    {
        rsoUpgrades.Value.sellerSpeedMultiplier += value;
        rsoContentSaved.Value.machineStatisticsUpgrade = rsoUpgrades.Value;
    }
    void AddSellerCapacityMult(float value)
    {
        rsoUpgrades.Value.sellerCapacityMultiplier += value;
        rsoContentSaved.Value.machineStatisticsUpgrade = rsoUpgrades.Value;
    }
    void AddSellerAutomation(float value)
    {
        rsoUpgrades.Value.sellerAutomation += value;
        rsoContentSaved.Value.machineStatisticsUpgrade = rsoUpgrades.Value;
    }
    void AddSellerProfitMult(float value)
    {
        rsoUpgrades.Value.sellerProfitMultiplier += value;
        rsoContentSaved.Value.machineStatisticsUpgrade = rsoUpgrades.Value;
    }
    #endregion
    
    #region Polisher
    void AddPolisherSpeedMult(float value)
    {
        rsoUpgrades.Value.polisherSpeedMultiplier += value;
        rsoContentSaved.Value.machineStatisticsUpgrade = rsoUpgrades.Value;
    }
    void AddPolisherPowerMult(float value)
    {
        rsoUpgrades.Value.polisherPowerMultiplier += value;
        rsoContentSaved.Value.machineStatisticsUpgrade = rsoUpgrades.Value;
    }
    void AddPolisherAutomation(float value)
    {
        rsoUpgrades.Value.polisherAutomation += value;
        rsoContentSaved.Value.machineStatisticsUpgrade = rsoUpgrades.Value;
    }
    #endregion
    
    #region Conveyor
    void AddConveyorSpeedMult(float value)
    {
        rsoUpgrades.Value.conveyorSpeedMultiplier += value;
        rsoContentSaved.Value.machineStatisticsUpgrade = rsoUpgrades.Value;
    }
    #endregion
}