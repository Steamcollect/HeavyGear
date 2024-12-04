using BigFloatNumerics;
using UnityEngine;
public class MachinePress : InteractiveMachineTemplate
{
    [Header("Internal Settings")]
    [SerializeField] CalculType calculType;
    [SerializeField] string value;
    
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

        }
    }

    public override void OnCooldownEnd()
    {
        
    }

    public override bool CanDoAction()
    {
        return true;
    }

    private void OnValidate()
    {
        Value = new BigNumber(value);
    }
}