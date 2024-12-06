using BigFloatNumerics;
using UnityEngine;
public class MachinePress : InteractiveMachineTemplate
{
    [Header("Internal Settings")]
    [SerializeField] CalculType calculType;
    [SerializeField] string value;
    float multiplyValue;
    BigNumber Value;

    [Space(10)]
    [SerializeField] MachineCollider oreCollider;

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
                    ore.MultiplyValue(multiplyValue);
                    break;
            }
        }

        EndAction();
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

    private void OnValidate()
    {
        if(calculType == CalculType.Multiply) float.TryParse(value, out multiplyValue);
        else Value = new BigNumber(value);
    }

}