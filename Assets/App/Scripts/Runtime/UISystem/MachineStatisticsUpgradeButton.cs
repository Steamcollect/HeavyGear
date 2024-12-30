using System;
using BigFloatNumerics;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
public class MachineStatisticsUpgradeButton : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float valueGiven;
    [SerializeField] string[] prices;

    int currentLevel;
    [SerializeField] int maxLevel;

    int currentIndex;
    public Action<int, int> OnLevelChange;

    [Space(10)]
    [SerializeField] UnityEvent<float> upgradeEvent;

    [Header("References")]
    [SerializeField] TMP_Text levelTxt;
    [SerializeField] TMP_Text priceTxt;

    [SerializeField] MachineStatisticsUpgradeButton[] upgradesToShowOnComplete;

    [Space(10)]
    // RSO
    [SerializeField] RSO_Coins rsoCoin;
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_RemoveCoin rseRemoveCoin;


    //[Header("Output")]

    private void Start()
    {
        if (prices.Length < maxLevel)
            Debug.LogError(gameObject.name + "have'nt enough prices compare to his max level");
    }

    public void OnClick()
    {
        if (currentLevel < maxLevel && rsoCoin.Value >= new BigNumber(prices[currentLevel]))
        {
            rseRemoveCoin.Call(new BigNumber(prices[currentLevel]));
            currentLevel++;
            OnLevelChange(currentLevel, currentIndex);

            upgradeEvent.Invoke(valueGiven);

            SetupVisual();

            // On max level reached
            if(currentLevel >= maxLevel)
            {
                foreach (MachineStatisticsUpgradeButton upgrade in upgradesToShowOnComplete)
                {
                    upgrade.Show();
                }
            }
        }
    }

    void SetupVisual()
    {
        levelTxt.text = $"LVL {currentLevel}/{maxLevel}";
        priceTxt.text = currentLevel >= maxLevel ? "MAX" : $"{prices[currentLevel]}$";
    }

    public void Setup(int level, int index)
    {
        currentLevel = level;
        currentIndex = index;
        SetupVisual();
    }
    public void Show()
    {
        transform.localScale = Vector3.one;
    }
    public void Hide()
    {
        transform.localScale = Vector3.zero;
    }
}