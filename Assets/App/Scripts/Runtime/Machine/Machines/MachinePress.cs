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

    }

    public override void OnObjDisable()
    {

    }

    public override void OnIdleStart()
    {
        
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
        
    }

    public override void Setup(MachineSlotSettings settings)
    {
        
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