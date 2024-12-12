using BigFloatNumerics;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("Output")]
    [SerializeField] RSE_LoadNewScene rseLoadNewScene;

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

            // Changer de scene si le joueur le souhaite
            //rseLoadNewScene.Call(coinLevel.nextFactorySceneName);
        }

        for (int i = 0; i < coinLevel.rebirthLevels.Count; i++)
        {
            if (coins >= coinLevel.rebirthLevels[i])
            {
                Debug.Log("Rebirth " + i + ", at " + coinLevel.rebirthLevels[i] + "possible");

                // Rebirth, charger la scene actuelle si le joueur le souhaite
                //rseLoadNewScene.Call( <Faut trouver le nom de la scene actuelle> );
            }
        }
    }
}