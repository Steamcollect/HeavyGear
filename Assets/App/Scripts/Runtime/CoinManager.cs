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

    [Header("Output")]
    [SerializeField] RSE_LoadNewScene rseLoadNewScene;
    [SerializeField] RSE_CommandEvent rseSaveData;

    private void OnEnable()
    {
        rseAddCoin.action += AddCoin;
    }
    private void OnDisable()
    {
        rseAddCoin.action -= AddCoin;
    }

    private void Start()
    {
        StartCoroutine(LateStart());
    }
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(.1f);

        AddCoin(rsoContentSaved.Value.coinAmount);

        // Calculate idle delay
        TimeSpan timeBetween = (DateTime.Now - rsoContentSaved.Value.lastDateTimeQuit);
        int totMin = (int)timeBetween.TotalMinutes;

        if (totMin > rsoContentSaved.Value.idleDelay) totMin = rsoContentSaved.Value.idleDelay;
        AddCoin(rsoContentSaved.Value.coinPerMin * totMin);
    }

    void AddCoin(BigNumber coinToAdd)
    {
        coins += coinToAdd;
        StartCoroutine(CoinPerMinDelay(coinToAdd));

        // Set save
        rsoContentSaved.Value.coinAmount = coins;

        rsoCoins.Value = coins;
        if (coinLevel == null) return;
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

    IEnumerator CoinPerMinDelay(BigNumber coin)
    {
        coinPerMin += coin;
        yield return new WaitForSeconds(60);
        coinPerMin -= coin;
    }

    private void OnApplicationQuit()
    {
        rsoContentSaved.Value.coinAmount = coins;
        rsoContentSaved.Value.coinPerMin = coinPerMin;
        rsoContentSaved.Value.lastDateTimeQuit = DateTime.Now;

        rseSaveData.Call();
    }
}