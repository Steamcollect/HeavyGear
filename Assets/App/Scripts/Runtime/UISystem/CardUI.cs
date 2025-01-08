using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CardUI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private MachineRarity machineRarity;
    [Space(10)]
    [SerializeField] private Image background;
    [SerializeField] private Image border;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI cardNumber;
    [SerializeField] private TextMeshProUGUI cardLevel;
    [SerializeField] private TextMeshProUGUI cardName;
    [Space(10)]
    [SerializeField] private Image cardIcon;
    [Space(10)]
    [SerializeField] private Slider slider;

    [Header("References")]
    [SerializeField] private SSO_CardVisual commonCard;
    [SerializeField] private SSO_CardVisual rareCard;
    [SerializeField] private SSO_CardVisual legendaryCard;

    [HideInInspector] public InventoryMachineData machine;

    public void SetCard(InventoryMachineData machine)
    {
        this.machine = machine;

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

        cardIcon.sprite = machine.machine.machineVisual;
    }

    private void SetCardVisual(SSO_CardVisual cardVisual)
    {
        background.color = cardVisual.firstColor;
        border.color = cardVisual.secondColor;
    }
}