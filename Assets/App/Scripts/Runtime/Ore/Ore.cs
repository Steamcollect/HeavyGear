using BigFloatNumerics;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ore : MonoBehaviour
{
    [BoxGroup("Current Ore Value")]
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

    private BigNumber currentOreValue = new BigNumber(1e0);

    private IEnumerable<ValueDropdownItem<OreData>> GetOreType()
    {
        foreach(var ore in oreData.oreData)
        {
            yield return new ValueDropdownItem<OreData>(ore.stats.name, ore);
        }
    }

    public BigNumber CurrentOreValue
    {
        get => currentOreValue;
        set
        {
            text.text = value.ToString(); // Temporaire
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
        CurrentOreValue *= value;
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
    }
}