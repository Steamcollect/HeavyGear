using System.Collections;
using UnityEngine;
public class MachineConveyorChanger : MonoBehaviour
{
    [Header("Settings")]
    public int currentStorage;

    bool canDropItem = true;

    [Header("References")]

    [SerializeField] GameObject itemPrefab;

    [Space(10)]
    [SerializeField] ConveyorBelt[] conveyorEnter;
    [SerializeField] ConveyorBelt conveyorExit;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    private void OnEnable()
    {
        foreach (var conveyor in conveyorEnter)
        {
            conveyor.onObjectTouchTheEnd += AddItem;
        }

        conveyorExit.onConveyorGetSpace += DropItem;
    }
    private void OnDisable()
    {
        foreach (var conveyor in conveyorEnter)
        {
            conveyor.onObjectTouchTheEnd -= AddItem;
        }

        conveyorExit.onConveyorGetSpace -= DropItem;
    }

    void AddItem(ConveyorBelt conveyor)
    {
        currentStorage++;
        conveyor.RemoveItem(conveyor.GetFirstItem());

        DropItem(null);
    }

    void DropItem(ConveyorBelt conveyor)
    {
        if (currentStorage > 0 && canDropItem && conveyorExit.haveSpaceToAddItem)
        {
            currentStorage--;
            conveyorExit.AddItem(Instantiate(itemPrefab).transform);
        }
    }
}