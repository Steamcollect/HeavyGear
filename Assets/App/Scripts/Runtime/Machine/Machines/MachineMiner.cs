using UnityEngine;
public class MachineMiner : InteractiveMachineTemplate
{
    [Space(10), Header("Internal Settings")]
    [SerializeField] GameObject itemPrefab;
    [SerializeField] ConveyorBelt conveyorBelt;

    public override void OnObjEnable()
    {
        // Do nothing
    }

    public override void OnObjDisable()
    {
        // Do nothing
    }

    public override void OnIdleStart()
    {
        // Do nothing
    }
    public override void OnActionStart()
    {
        conveyorBelt.AddItem(Instantiate(itemPrefab.transform));

        EndAction();
    }

    public override void OnCooldownEnd()
    {
        // Do nothing
    }

    public override bool CanDoAction()
    {
        return conveyorBelt.haveSpaceToAddItem;
    }
}