using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MachineSeller : InteractiveMachineTemplate
{
    [Space(10), Header("Internal Settings")]
    List<OreData> currentStorage = new List<OreData>();
    [SerializeField] int maxStorage;

    bool canGetItem = true;

    [Space(10)]
    [SerializeField] ConveyorBelt conveyorBelt;

    public override void OnObjEnable()
    {
        conveyorBelt.onObjectTouchTheEnd += AddStorage;
    }

    public override void OnObjDisable()
    {
        conveyorBelt.onObjectTouchTheEnd -= AddStorage;
    }

    public override void OnIdleStart()
    {
        // Do nothing
    }
    
    public override void OnActionStart()
    {
        currentStorage.Clear();
        EndAction();
    }

    public override void OnCooldownEnd()
    {
        // Don nothing
    }

    void AddStorage(ConveyorBelt conveyor)
    {
        if(currentState == MachineState.Idle && currentStorage.Count < maxStorage && canGetItem)
        {
            currentStorage.Add(conveyorBelt.RemoveItem(conveyorBelt.GetFirstItem()));

            StartCoroutine(GetItemTimer());
        }
    }

    void CheckItemsOnConveyor()
    {
        if (currentState == MachineState.Idle && currentStorage.Count < maxStorage && canGetItem)
        {
            if(conveyorBelt.GetFirstItem() != null && conveyorBelt.GetFirstItem().isAtTheEnd)
            {
                AddStorage(null);
            }
        }
    }

    IEnumerator GetItemTimer()
    {
        canGetItem = false;
        yield return new WaitForSeconds(data.speed);
        canGetItem = true;

        CheckItemsOnConveyor();
    }

    public override bool CanDoAction()
    {
        // Cant interact if there is no item inside
        return currentStorage.Count > 0;
    }
}