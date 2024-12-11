using BigFloatNumerics;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] SSO_CoinLevel coinLevel;
    BigNumber coins = new BigNumber(0);

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
        coins += coinToAdd;
        rsoCoins.Value = coins;

        if (coins >= coinLevel.nextFactoryLevel)
        {
            Debug.Log("Next factory Unlock");
        }

        for (int i = 0; i < coinLevel.rebirthLevels.Count; i++)
        {
            if (coins >= coinLevel.rebirthLevels[i])
            {
                Debug.Log("Rebirth " + i + ", at " + coinLevel.rebirthLevels[i] + "possible");
            }
        }
    }
}