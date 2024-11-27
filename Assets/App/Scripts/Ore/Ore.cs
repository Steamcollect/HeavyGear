using BigFloatNumerics;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow;

public class Ore : MonoBehaviour
{
    [Title("Settings")]
    [SerializeField] private float currentOreValue;
    private BiggerFloat test = new(3.0e10);
    public BigNumber test2 = new(3.245f,3);

    [ValueDropdown("GetOreType",HideChildProperties = true)]
    [SerializeField] private OreData oreType;

    [Title("References")]
    [Required][SerializeField] private SSO_OreData oreData;

    [SerializeField] private OreVisual oreVisual;
    [SerializeField] private OreAnimation oreAnimation;
    [SerializeField] private OreEffects oreEffects;

    private IEnumerable<ValueDropdownItem<OreData>> GetOreType()
    {
        if(oreData == null) yield break;

        foreach(var ore in oreData.oreData)
        {
            yield return new ValueDropdownItem<OreData>(ore.stats.name, ore);
        }
    }

    public float CurrentOreValue
    {
        get => currentOreValue;
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

    private void Initialize()
    {
        currentOreValue = oreType.stats.baseValue;
        UpdateOre();
    }

    private void Start()
    {
        Debug.Log(test.ToString());
        Debug.Log(test2.ToString());
        Initialize();
    }

    public void MultiplyValue(float value)
    {
        test2.Multiply(test2);
        Debug.Log(test2.ToString());
    }

    private void UpdateOre()
    {
        for(int i = oreType.stats.index; i < oreData.oreData.Count; i++)
        {
            if (currentOreValue >= oreData.oreData[i].stats.baseValue && oreType != oreData.oreData[i])
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