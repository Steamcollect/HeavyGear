using BigFloatNumerics;
using UnityEngine;
public class CoinManager : MonoBehaviour
{
    [Header("References")]
    BigNumber coins;

    [Space(10)]
    // RSO
    [SerializeField] RSO_Coins rsoCoins;
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_AddCoin rseAddCoin;

    //[Header("Output")]

    private void OnEnable()
    {
        rseAddCoin.action += AddCoin;
    }
    private void OnDisable()
    {
        rseAddCoin.action -= AddCoin;
    }

    void AddCoin(BigNumber coinToAdd)
    {
        coins.Add(coinToAdd);
        rsoCoins.Value = coins;
    }
}