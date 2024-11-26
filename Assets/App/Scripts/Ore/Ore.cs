using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ore : MonoBehaviour
{
    [BoxGroup("Settings")]
    [SerializeField] private float currentOreValue;

    [BoxGroup("Settings")]
    [ValueDropdown("GetOreType")]
    [SerializeField] private OreData oreType;

    [BoxGroup("References")]
    [Required][SerializeField] private SSO_OreData oreData;
    [BoxGroup("References")]
    [SerializeField] private OreVisual oreVisual;
    [BoxGroup("References")]
    [SerializeField] private OreAnimation oreAnimation;
    [BoxGroup("References")]
    [SerializeField] private OreEffects oreEffects;

    private IEnumerable<ValueDropdownItem<OreData>> GetOreType()
    {
        if(oreData == null || oreData.oreData == null)
        {
            yield break;
        }

        foreach(var ore in oreData.oreData)
        {
            yield return new ValueDropdownItem<OreData>(ore.name, ore);
        }
    }

    public float CurrentOreValue
    {
        get
        {
            return currentOreValue;
        }
        set
        {
            currentOreValue = value;
            UpdateOre();
        }
    }

    public OreData OreType
    {
        get => oreType;
        set => oreType = value;
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        currentOreValue = oreType.baseValue;
        UpdateOre();
    }

    private void UpdateOre()
    {
       for(int i = oreType.index; i < oreData.oreData.Count; i++)
        {
            if (currentOreValue >= oreData.oreData[i].baseValue)
            {
                oreType = oreData.oreData[i];
                UpdateOreVisual();
            }
        }
    }

    private void UpdateOreVisual()
    {
        if(oreVisual != null) oreVisual.UpdateVisual(oreType.visual);

        //Play Animation
        //Play Particle
    }
}