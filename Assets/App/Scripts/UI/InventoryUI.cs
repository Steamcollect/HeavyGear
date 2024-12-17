using UnityEngine;
public class InventoryUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject cardVisual;
    [SerializeField] private GameObject list;
    [Space(10)]
    [SerializeField] private RSO_MachinesListUI rsoMachinesListUI;

    public void UpdateInventoryUI()
    {
        for (int i = 0; i < rsoMachinesListUI.Value.Count; i++)
        {
            GameObject card = Instantiate(cardVisual, list.transform);
            card.GetComponent<CardUI>().SetCard(rsoMachinesListUI.Value[i].machine);
        }
    }
}