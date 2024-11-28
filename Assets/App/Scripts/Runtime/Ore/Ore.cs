using BigFloatNumerics;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow;

public class Ore : MonoBehaviour
{
    [TitleGroup("Current Ore Value")]
    [ReadOnly] public string currentValue;

    [Title("Settings")]
    [ValueDropdown("GetOreType",HideChildProperties = true)]
    [SerializeField] private OreData oreType;

    [Title("References")]
    [Required]
    [SerializeField] private SSO_OreData oreData;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI text;
    [Space(10)]
    [SerializeField] private OreVisual oreVisual;
    [SerializeField] private OreAnimation oreAnimation;
    [SerializeField] private OreEffects oreEffects;

    private BiggerFloat currentOreValue = new BiggerFloat(1e0);

    private IEnumerable<ValueDropdownItem<OreData>> GetOreType()
    {
        if(oreData == null) yield break;

        foreach(var ore in oreData.oreData)
        {
            yield return new ValueDropdownItem<OreData>(ore.stats.name, ore);
        }
    }

    public BiggerFloat CurrentOreValue
    {
        get => currentOreValue;
        set
        {
            currentValue = value.ToString();
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
        CurrentOreValue = oreType.stats.baseValue;
        UpdateOre();
    }

    private void Start()
    {
        Initialize();
    }

    public void MultiplyValue(float value)
    {
        CurrentOreValue = currentOreValue.Multiply(new BiggerFloat(value,0));
        text.text = CurrentOreValue.ToString();
    }

    private void UpdateOre()
    {
        for(int i = oreType.stats.index; i < oreData.oreData.Count; i++)
        {
            Debug.Log("qdq");
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