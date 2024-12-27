using BigFloatNumerics;
using UnityEngine;
public class CoinTextUpdater : TextUpdater<BigNumber>
{
    protected override string GetValueTextShow()
    {
        return rso.Value.ToString();
    }
}