using System;
using System.Collections;
using BigFloatNumerics;
using BT.Save;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] SSO_CoinLevel coinLevel;
    BigNumber coins = new BigNumber(0);
    BigNumber coinPerMin = new BigNumber(0);

    [Space(10)]
    // RSO
    [SerializeField] RSO_Coins rsoCoins;
    [SerializeField] RSO_ContentSaved rsoContentSaved;
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_AddCoin rseAddCoin;
    [SerializeField] RSE_RemoveCoin rseRemoveCoin;

    [Header("Output")]
    [SerializeField] RSE_LoadNewScene rseLoadNewScene;
    [SerializeField] RSE_CommandEvent rseSaveData;

    private void OnEnable()
    {
        rseAddCoin.action += AddCoin;
        rseRemoveCoin.action += RemoveCoin;
    }
    private void OnDisable()
    {
        rseAddCoin.action -= AddCoin;
        rseRemoveCoin.action -= RemoveCoin;
    }

    private void Start()
    {
        StartCoroutine(LateStart());
    }
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(.1f);

        AddCoin(new BigNumber(rsoContentSaved.Value.coinAmount));

        // Calculate idle delay
        TimeSpan varTime = DateTime.Now - DateTime.Parse(rsoContentSaved.Value.lastDateTimeQuit);
        double fractionalMinutes = varTime.TotalMinutes;
        int totMin = (int)fractionalMinutes;

        if (totMin > rsoContentSaved.Value.idleDelay) totMin = rsoContentSaved.Value.idleDelay;
        AddCoin(new BigNumber(rsoContentSaved.Value.coinPerMin) * totMin);
        print("Show value : " + new BigNumber(rsoContentSaved.Value.coinPerMin) * totMin);
    }

    void AddCoin(BigNumber coinToAdd)
    {
        coins += coinToAdd;
        rsoCoins.Value = coins;
        StartCoroutine(CoinPerMinDelay(coinToAdd));

        // Set save
        rsoContentSaved.Value.coinAmount = coins.ToString();

        if (coinLevel == null) return;
        if (coins >= coinLevel.nextFactoryLevel)
        {
            //Debug.Log("Next factory Unlock");

            // Changer de scene si le joueur le souhaite
            //rseLoadNewScene.Call(coinLevel.nextFactorySceneName);
        }

        for (int i = 0; i < coinLevel.rebirthLevels.Count; i++)
        {
            if (coins >= coinLevel.rebirthLevels[i])
            {
                //Debug.Log("Rebirth " + i + ", at " + coinLevel.rebirthLevels[i] + "possible");

                // Rebirth, charger la scene actuelle si le joueur le souhaite
                //rseLoadNewScene.Call( <Faut trouver le nom de la scene actuelle> );
            }
        }
    }
    void RemoveCoin(BigNumber coinToRemove)
    {
        coins -= coinToRemove;
        rsoCoins.Value = coins;
        rsoContentSaved.Value.coinAmount = coins.ToString();
    }

    IEnumerator CoinPerMinDelay(BigNumber coin)
    {
        coinPerMin += coin;
        yield return new WaitForSeconds(60);
        coinPerMin -= coin;
    }

    private void OnApplicationQuit()
    {
        rsoContentSaved.Value.coinAmount = coins.ToString();
        rsoContentSaved.Value.coinPerMin = coinPerMin.ToString();
        rsoContentSaved.Value.lastDateTimeQuit = DateTime.Now.ToString();

        rseSaveData.Call();
    }
}