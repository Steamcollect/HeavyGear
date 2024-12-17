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
    [Space(5)]
    [SerializeField] private Image border;
    [SerializeField] private Image borderGlow;
    [SerializeField] private Image borderShadow;
    [Space(5)]
    [SerializeField] private Image sliderBackground;
    [SerializeField] private Image sliderFill;
    [Header("References")]
    [SerializeField] private SSO_CardVisual commonCard;
    [SerializeField] private SSO_CardVisual rareCard;
    [SerializeField] private SSO_CardVisual legendaryCard;

    public MachineRarity GetMachineRarity() => machineRarity;

    public void SetCard(SSO_MachinePlacementData machine)
    {
        switch (machine.machineRarity)
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
    }

    private void SetCardVisual(SSO_CardVisual cardVisual)
    {
        background.color = cardVisual.firstColor;
        border.color = cardVisual.firstColor;
        borderGlow.color = cardVisual.secondColor;
        borderShadow.color = cardVisual.firstColor;
        sliderBackground.color = cardVisual.firstColor;
        sliderFill.color = cardVisual.firstColor;
    }

    private void SetCardStats()
    {

    }
}