using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class MachineMiner : InteractiveMachineTemplate
{
    [Space(10)]
    [SerializeField] RSO_OreManager rsoOreManager;

    [Space(10), Header("Internal Settings")]
    #region ORE
    [ValueDropdown("GetOreType", HideChildProperties = true)]
    [SerializeField] private OreData oreType;
    [SerializeField] private SSO_OreData oreData;

    private IEnumerable<ValueDropdownItem<OreData>> GetOreType()
    {
        foreach (var ore in oreData.oreData)
        {
            yield return new ValueDropdownItem<OreData>(ore.stats.name, ore);
        }
    }
    #endregion

    [Space(10)]
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
        Ore ore = rsoOreManager.Value.InstantiateOre();
        ore.gameObject.SetActive(true);
        ore.Initialize(oreType);

        conveyorBelt.AddItem(ore);

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