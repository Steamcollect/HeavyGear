using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
public class MachineAutoTest : InteractiveMachineTemplate
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


    public int indexPath;
    ConveyorBelt conveyorBelt;

    public override void OnObjEnable()
    {
        //DoNothing
    }

    public override void OnObjDisable()
    {
        //DoNothing
    }

    public override void OnIdleStart()
    {
        //DoNothing
    }

    public override void OnActionStart()
    {
        conveyorBelt.ChangePathPointAvailable(indexPath, true);
        StartCoroutine(Utils.Delay(1f,() =>
        {
            Ore ore = rsoOreManager.Value.InstantiateOre();
            ore.gameObject.SetActive(true);
            ore.Initialize(oreType);
            conveyorBelt.AddItem(ore, indexPath);
            
        }));
        StartCoroutine(Utils.Delay(2f, () =>
        {
            conveyorBelt.ChangePathPointAvailable(indexPath, false);
            EndAction();
        }));
    }

    public override bool CanDoAction()
    {
        return true;
    }

    public override void OnCooldownEnd()
    {
        //DoNothing
    }

    public override void SetupChildRequirement(MachineSlotSettings settings)
    {
        conveyorBelt = settings.conveyorsExit[0];
    }
}