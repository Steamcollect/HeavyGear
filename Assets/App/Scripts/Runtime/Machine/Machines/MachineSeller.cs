using UnityEngine;
public class MachineSeller : MachineTemplate
{
    [Space(10), Header("Internal Settings")]
    [SerializeField] int currentStorage;
    [SerializeField] int maxStorage;

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
        currentStorage = 0;
        EndAction();
    }

    public override void OnCooldownEnd()
    {
        // Don nothing
    }

    void AddStorage()
    {
        if(currentState == MachineState.Idle && currentStorage < maxStorage)
        {
            currentStorage++;
            conveyorBelt.RemoveItem(conveyorBelt.GetFirstItem());
        }
    }
}