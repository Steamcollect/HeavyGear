using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class CardUI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private MachineRarity machineRarity;
    [Space(10)]
    [SerializeField] private SSO_CardVisual commonCard;
    [SerializeField] private SSO_CardVisual rareCard;
    [SerializeField] private SSO_CardVisual legendaryCard;

    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    private void Update()
    {
        
    }
}