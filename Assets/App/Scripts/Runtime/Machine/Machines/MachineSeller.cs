using System.Collections;
using UnityEngine;
public class MachineSeller : MachineTemplate
{
    [Space(10), Header("Internal Settings")]
    [SerializeField] int currentStorage;
    [SerializeField] int maxStorage;

    float recuperationTimer;

    bool canRecupItem = true;

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
        if (canRecupItem)
        {
            currentStorage = 0;
            EndAction();
        }
    }

    public override void OnCooldownEnd()
    {
        // Don nothing
    }

    void AddStorage()
    {
        if(currentState == MachineState.Idle && currentStorage < maxStorage && canRecupItem)
        {
            currentStorage++;
            conveyorBelt.RemoveItem(conveyorBelt.GetFirstItem());

            StartCoroutine(GetItemTimer());
        }
    }

    void CheckItemsOnConveyor()
    {
        if (currentState == MachineState.Idle && currentStorage < maxStorage && canRecupItem)
        {
            if(conveyorBelt.GetFirstItem() != null && conveyorBelt.GetFirstItem().isAtTheEnd)
            {
                AddStorage();
            }
        }
    }

    IEnumerator GetItemTimer()
    {
        canRecupItem = false;
        yield return new WaitForSeconds(data.speed);
        canRecupItem = true;

        CheckItemsOnConveyor();
    }
}