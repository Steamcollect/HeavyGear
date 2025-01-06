using BigFloatNumerics;
using UnityEngine;

[CreateAssetMenu(fileName = "SSO_Exp", menuName = "ScriptableObject/Math/SSO_Exp")]
public class SSO_Exp : SSO_MathFormule
{
    public override BigNumber Compute(int x, int maxX)
    {
        return new BigNumber(x*x);
    }
}