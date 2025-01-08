using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedCardInfo : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Window window;
    [SerializeField] private Image backgroundBanner;
    [SerializeField] private Image cardBackground;
    [SerializeField] private Image cardBorder;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI cardNumber;
    [SerializeField] private TextMeshProUGUI cardLevel;
    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private TextMeshProUGUI cardDescription;
    [Space(10)]
    [SerializeField] private Image cardIcon;
    [Space(10)]
    [SerializeField] private Slider slider;

    [Header("References")]
    [SerializeField] private SSO_CardVisual commonCard;
    [SerializeField] private SSO_CardVisual rareCard;
    [SerializeField] private SSO_CardVisual legendaryCard;

    [Header("Input")]
    [SerializeField] private RSE_OpenAdvancedPanel rseOpenAdvancedPanel;

    private void OnEnable()
    {
        rseOpenAdvancedPanel.action += OpenAdvancedCard;
    }

    private void OnDisable()
    {
        rseOpenAdvancedPanel.action -= OpenAdvancedCard;
    }

    private void Start()
    {
        CloseAdvancedCard();
    }

    public void OpenAdvancedCard(CardUI card)
    {
        window.WindowFadeIn();
        window.SetWindowActiveTrue();

        InventoryMachineData machine = card.machine;

        switch (machine.machine.machineRarity)
        {
            case MachineRarity.Common:
                SetCardVisual(commonCard);
                break;
            case MachineRarity.Rare:
                SetCardVisual(rareCard);
                break;
            case MachineRarity.Legendary:
                SetCardVisual(legendaryCard);
                break;
            default:
                Debug.LogWarning("No machine rarity set");
                break;
        }

        cardLevel.text = machine.machineLevel.ToString();
        cardName.text = machine.machine.machineName.ToString();
        cardNumber.text = machine.amountAquired + "/" + machine.maxAmount;
        slider.value = (float)machine.amountAquired / (float)machine.maxAmount;
        cardDescription.text = machine.machine.machineDescription.ToString();

        cardIcon.sprite = machine.machine.machineVisual;
    }

    public void CloseAdvancedCard()
    {
        window.WindowFadeOut();
        window.SetWindowActiveFalse();
    }

    private void SetCardVisual(SSO_CardVisual cardVisual)
    {
        backgroundBanner.color = cardVisual.firstColor;
        cardBackground.color = cardVisual.firstColor;
        cardBorder.color = cardVisual.secondColor;
    }
}