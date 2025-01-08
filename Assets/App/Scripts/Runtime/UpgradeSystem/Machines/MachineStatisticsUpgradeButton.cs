using System;
using BigFloatNumerics;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MachineStatisticsUpgradeButton : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float valueGivenEachLevel;
    [SerializeField] private SSO_MathFormule ssoFormule;
    [SerializeField] int maxLevel;
    int currentLevel;
    
    int currentIndex;
    public Action<int, int> OnLevelChange;

    [Space(10)]
    [SerializeField] UnityEvent<float> onUpgradeBuy;

    [Header("References")]
    [SerializeField] TMP_Text levelTxt;
    [SerializeField] TMP_Text priceTxt;

    [SerializeField] MachineStatisticsUpgradeButton[] upgradesToShowOnComplete;

    [Space(10)]
    [SerializeField] RSO_Coins rsoCoin;
    
    [Header("Input")]
    [SerializeField] RSE_RemoveCoin rseRemoveCoin;

    private BigNumber currentCost = new(0);
    public bool canBuy;


    private void OnEnable()
    {
        rsoCoin.OnChanged += CheckCanBuy;
    }

    private void OnDisable()
    {
        rsoCoin.OnChanged -= CheckCanBuy;
    }

    private void CheckCanBuy()
    {
        canBuy = rsoCoin.Value.CompareTo(currentCost) is 0 or 1;
    }
    
    private void Start()
    {
        if (currentLevel <= maxLevel) 
            Debug.Log(gameObject.name + "have'nt enough prices compare to his max level");
        InitComponent();
        CheckCanBuy();
    }

    private void InitComponent()
    {
        currentCost = ssoFormule.Evaluate(currentLevel, maxLevel);
        SetupVisual();
    }

    public void UpgradeBuy()
    {
        if (currentLevel >= maxLevel) return;
        if (rsoCoin.Value < currentCost) return;
        
        rseRemoveCoin.Call(currentCost);
        currentLevel++;
        
        OnLevelChange(currentLevel, currentIndex);
        
        if (currentLevel < maxLevel) currentCost = ssoFormule.Evaluate(currentLevel, maxLevel);
        
        onUpgradeBuy.Invoke(valueGivenEachLevel);
        SetupVisual();
        
        if(currentLevel >= maxLevel)
        {
            foreach (MachineStatisticsUpgradeButton upgrade in upgradesToShowOnComplete)
            {
                upgrade.Show();
            }
        }
    }

    void SetupVisual()
    {
        levelTxt.text = $"LVL {currentLevel}/{maxLevel}";
        priceTxt.text = currentLevel >= maxLevel ? "MAX" : $"{currentCost}$";
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