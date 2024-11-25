using UnityEngine;

public class Ore : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SSO_OreData oreData;
    [SerializeField] private OreVisual oreVisual;

    public float value;
    public int index;

    private float Value
    {
        get
        {
            return this.value;
        }
        set
        {
            this.value = value;
            UpdateOre();
        }
    }

    private void Start()
    {
        UpdateOre();
    }

    public void MultiplyValue(float value)
    {
        Value = Value * value;
        UpdateOre();
    }

    private void UpdateOre()
    {
        for(int i = index; i < oreData.oreData.Count; i++)
        {
            if(value > oreData.oreData[i].baseValue)
            {
                index = i;
            }
        }

        oreVisual.UpdateVisual(oreData.oreData[index].visual);
    }
}