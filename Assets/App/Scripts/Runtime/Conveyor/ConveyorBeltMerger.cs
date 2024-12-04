using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
public class ConveyorBeltMerger : MonoBehaviour
{
    [Header("References")]
    public ConveyorBelt[] conveyorsEnter;
    [SerializeField] ConveyorBelt conveyorExit;

    List<ConveyorBeltMergerEntry> entrys = new List<ConveyorBeltMergerEntry>();
    int currentEnterConveyorIndex = 0;

    class ConveyorBeltMergerEntry
    {
        public ConveyorBelt conveyor;
        public bool waitingForEntry = false;

        public ConveyorBeltMergerEntry(ConveyorBelt conveyorEnter)
        {
            this.conveyor = conveyorEnter;
        }
    }

    [Space(10)]
    // RSO
    [SerializeField] RSO_OreManager rsoOreManager;

    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    private void OnEnable()
    {
        foreach (var conveyor in conveyorsEnter)
        {
            conveyor.onObjectTouchTheEnd += ChangeItemConveyor;
        }

        conveyorExit.onConveyorGetSpace += ChangeItemConveyor;
    }
    private void OnDisable()
    {
        foreach (var conveyor in conveyorsEnter)
        {
            conveyor.onObjectTouchTheEnd -= ChangeItemConveyor;
        }

        conveyorExit.onConveyorGetSpace -= ChangeItemConveyor;
    }

    private void Start()
    {
        foreach (var item in conveyorsEnter)
        {
            entrys.Add(new ConveyorBeltMergerEntry(item));
        }
    }

    void ChangeItemConveyor(ConveyorBelt conveyor)
    {
        // Set the conveyor that he is waiting for entry
        if(conveyor != conveyorExit) entrys.Find(x => x.conveyor == conveyor).waitingForEntry = true;

        if (conveyorExit.haveSpaceToAddItem)
        {
            // Check if conveyors are waiting for entry
            foreach (var i in entrys)
            {
                currentEnterConveyorIndex = (currentEnterConveyorIndex + 1) % entrys.Count;
                ConveyorBeltMergerEntry entry = entrys[currentEnterConveyorIndex];

                if (entry.waitingForEntry)
                {
                    Ore ore = rsoOreManager.Value.InstantiateOre();
                    ore.gameObject.SetActive(true);

                    ore.Initialize(entry.conveyor.RemoveItem(entry.conveyor.GetFirstItem()).OreType);
                    conveyorExit.AddItem(ore);

                    entry.waitingForEntry = false;

                    return;
                }
            }
        }
    }
}