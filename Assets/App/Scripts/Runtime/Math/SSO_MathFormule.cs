using BigFloatNumerics;
using UnityEngine;

public abstract class SSO_MathFormule : ScriptableObject
{
    public BigNumber costStart;
    
    public abstract BigNumber Evaluate(int x, int maxX);
}