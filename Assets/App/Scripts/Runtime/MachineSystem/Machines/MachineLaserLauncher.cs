using BigFloatNumerics;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;

public class MachineLaserLauncher : InteractiveMachineTemplate
{
    [Header("Internal Settings")]
    [SerializeField] CalculType calculType;
    [SerializeField] string value;

    BigNumber Value => new BigNumber(value);

    [Space(10)]
    [SerializeField] MachineCollider oreCollider;

    bool isActive = false;

    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    [Header("Output")]
    [SerializeField] private UnityEvent onMachineAction;
    [SerializeField] private UnityEvent onMachineEndAction;

    public override void OnObjEnable()
    {
        oreCollider.onItemEnter += OnItemTouch;
    }
    public override void OnObjDisable()
    {
        oreCollider.onItemEnter -= OnItemTouch;
    }

    public override void OnActionStart()
    {
        onMachineAction.Invoke();
        StartCoroutine(ActionDelay());
    }
    void OnItemTouch(Ore ore)
    {
        if (isActive)
        {
            switch (calculType)
            {
                case CalculType.Add:
                    ore.AddValue(Value);
                    break;

                case CalculType.Remove:
                    ore.RemoveValue(Value);
                    break;
                case CalculType.Multiply:
                    ore.MultiplyValue((float)Value);
                    break;
            }
        }
    }

    IEnumerator ActionDelay()
    {
        isActive = true;
        yield return new WaitForSeconds(statistics.duration);
        isActive = false;

        onMachineEndAction.Invoke();
        EndAction();
    }

    public override void OnCooldownEnd()
    {
        
    }
    public override void OnIdleStart()
    {
        
    }

    public override void SetupChildRequirement(MachineSlotSettings settings)
    {

    }

    public override float CooldownMultiplier()
    {
        return rsoStatisticsUpgrades.Value.polisherSpeedMultiplier;
    }
    public override bool CanDoAction()
    {
        return true;
    }
}