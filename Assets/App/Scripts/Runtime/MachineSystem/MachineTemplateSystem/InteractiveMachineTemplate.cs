using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using DG.Tweening;

public abstract class InteractiveMachineTemplate : MonoBehaviour
{
    [Header("References")]
    public SSO_MachineData SSO_statistics;
    [HideInInspector] public MachineData statistics;
    [SerializeField] MachineType machineType;

    public RSO_MachinesStatisticsUpgrades rsoStatisticsUpgrades;

    Clickable clickable;

    [HideInInspector] public MachineState currentState;

    [FormerlySerializedAs("rsoUpgradeData")] public RSO_FactoryUpgradeData rsoFactoryUpgradeData; 

    public void SetupParentRequirement(Clickable newClickable) // On Enable
    {
        clickable = newClickable;
        clickable.onClickUp += Interact;

        currentState = MachineState.Idle;

        OnObjEnable();
    }
    public abstract void OnObjEnable();
    private void OnDisable()
    {
        clickable.onClickUp -= Interact;

        OnObjDisable();
    }
    public abstract void OnObjDisable();

    private void Start()
    {
        statistics = SSO_statistics.data.Copy();
    }

    void Interact()
    {
        if(currentState == MachineState.Idle && CanDoAction())
        {
            transform.BumpVisual();
            StartAction();
        }
    }

    #region IDLE
    void StartIdle()
    {
        currentState = MachineState.Idle;

        OnIdleStart();
    }
    public abstract void OnIdleStart();
    #endregion

    #region ACTION
    void StartAction()
    {
        currentState = MachineState.Action;

        OnActionStart();
    }
    public abstract void OnActionStart();

    public void EndAction()
    {
        StartCoroutine(ActionCooldown());
        StartCoroutine(AutomationCooldown());
        currentState = MachineState.Cooldown;
    }

    public abstract bool CanDoAction();
    #endregion

    #region COOLDOWN
    void EndCooldown()
    {
        StartIdle();
    }
    public abstract void OnCooldownEnd();

    IEnumerator ActionCooldown()
    {
        yield return new WaitForSeconds(statistics.cooldown * CooldownMultiplier());
        EndCooldown();
    }
    public abstract float CooldownMultiplier();
    #endregion

    #region Automation
    IEnumerator AutomationCooldown()
    {
        float automationMultipier = 1;
        switch (machineType)
        {
            case MachineType.Miner:
                automationMultipier = rsoStatisticsUpgrades.Value.minerAutomation;
                break;

            case MachineType.Polisher:
                automationMultipier = rsoStatisticsUpgrades.Value.polisherAutomation;
                break;

            case MachineType.Seller:
                automationMultipier = rsoStatisticsUpgrades.Value.sellerAutomation;
                break;
        }

        if (automationMultipier == 0) yield break;

        yield return new WaitForSeconds((statistics.cooldown * CooldownMultiplier()) / automationMultipier);
        Interact();
    }
    #endregion

    public abstract void SetupChildRequirement(MachineSlotSettings settings);
}