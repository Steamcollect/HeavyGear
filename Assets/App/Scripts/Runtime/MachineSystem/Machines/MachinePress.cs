using BigFloatNumerics;
using UnityEngine;
using UnityEngine.Events;

public class MachinePress : InteractiveMachineTemplate
{
    [Header("Internal Settings")]
    [SerializeField] CalculType calculType;
    [SerializeField] string value;

    BigNumber Value => new BigNumber(value);

    [Space(10)]
    [SerializeField] MachineCollider oreCollider;

    [Header("Output")] 
    [SerializeField] private UnityEvent onMachineAction;
    [SerializeField] private UnityEvent onMachineEndAction;

    public override void OnObjEnable()
    {
        // Do nothing
    }

    public override void OnObjDisable()
    {
        // Do nothing
    }

    public override void OnIdleStart()
    {
        // Do nothing
    }

    public override void OnActionStart()
    {
        onMachineAction.Invoke();
        StartCoroutine(Utils.Delay(statistics.speed, () =>
        {
            foreach (Ore ore in oreCollider.oresInCollision)
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
            onMachineEndAction.Invoke();
            EndAction();
        }));
    }

    public override void OnCooldownEnd()
    {
        // Do nothing
    }

    public override void SetupChildRequirement(MachineSlotSettings settings)
    {
        // Do nothing
    }

    public override bool CanDoAction()
    {
        return true;
    }

    public override float CooldownMultiplier()
    {
        return 1;
    }
}