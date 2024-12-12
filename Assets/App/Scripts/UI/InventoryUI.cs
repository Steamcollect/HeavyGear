using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class InventoryUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject cardVisual;
    [SerializeField] private GameObject list;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    private List<CardUI> cards = new List<CardUI>();

    public void UpdateInventoryUI(SSO_MachinePlacementData[] machines)
    {
        for(int i = 0; i < machines.Length; i++)
        {
            GameObject card = Instantiate(cardVisual, list.transform);
            card.GetComponent<CardUI>().SetCard(machines[i]);
        }
    }

    public void SortInTransformList()
    {
        for(int i = 0; i < cards.Count; i++)
        {
            for(int j = 0; j < cards.Count; j++)
            {
                switch (cards[i].GetMachineRarity())
                {
                    case MachineRarity.Common:
                        cards[i].transform.SetSiblingIndex(j);
                        break;

                }
            }
        }
    }
}