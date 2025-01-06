using BigFloatNumerics;
using UnityEngine;

public abstract class SSO_MathFormule : ScriptableObject
{
    public BigNumber costStart;
    
    public abstract BigNumber Compute(int x, int maxX);
}