using System;
using System.Collections;
using System.Globalization;
using BigFloatNumerics;
using BT.Save;
using UnityEngine;
using UnityEngine.SceneManagement;
using WebSocketSharp;

public class CoinManager : MonoBehaviour
{
    [Header("References")]
    BigNumber coins = new (0);
    BigNumber coinPerMin = new (0);

    [Space(10)]
    [SerializeField] RSO_Coins rsoCoins;
    [SerializeField] RSO_ContentSaved rsoContentSaved;
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_AddCoin rseAddCoin;
    [SerializeField] RSE_RemoveCoin rseRemoveCoin;

    [Header("Output")]
    [SerializeField] RSE_SaveData rseSaveData;

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

        CheckMoneyReceveidWhenAFK();
    }

    private void CheckMoneyReceveidWhenAFK()
    {
        if (rsoContentSaved.Value.coinAmount != "0e0" && !rsoContentSaved.Value.coinAmount.IsNullOrEmpty())
        {
            AddCoin(new BigNumber(rsoContentSaved.Value.coinAmount));
        }

        if (DateTime.TryParse(rsoContentSaved.Value.lastDateTimeQuit, out var lastDateTimeQuit))
        {
            // Calculate idle delay
            TimeSpan varTime = DateTime.Now - lastDateTimeQuit;
            double fractionalMinutes = varTime.TotalMinutes;
            int totMin = (int)fractionalMinutes;

            if (totMin > rsoContentSaved.Value.idleDelay) totMin = rsoContentSaved.Value.idleDelay;
            AddCoin(new BigNumber(rsoContentSaved.Value.coinPerMin) * totMin);
            // print("Show value : " + new BigNumber(rsoContentSaved.Value.coinPerMin) * totMin);
        }
    }

    void AddCoin(BigNumber coinToAdd)
    {
        coins += coinToAdd;
        rsoCoins.Value = coins;
        StartCoroutine(CoinPerMinDelay(coinToAdd));

        // Set save
        rsoContentSaved.Value.coinAmount = coins.ToString();
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

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            rsoContentSaved.Value.coinAmount = coins.ToString();
            rsoContentSaved.Value.coinPerMin = coinPerMin.ToString();
            rsoContentSaved.Value.lastDateTimeQuit = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        }
    }
}