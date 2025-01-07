using BigFloatNumerics;
using UnityEngine;

[CreateAssetMenu(fileName = "SSO_BaseCost", menuName = "ScriptableObject/Math/SSO_BaseCost")]
public class SSO_BaseFormule : SSO_MathFormule
{
    public override BigNumber Evaluate(int x, int maxX)
    {
        return new BigNumber(costStart.Add(new BigNumber(x*1.5f)));
    }
}