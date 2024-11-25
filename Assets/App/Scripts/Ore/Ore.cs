using UnityEngine;
public class Ore : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float value;

    [Header("References")]
    [SerializeField] private SSO_OreData oreData;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    public float Value
    {
        get { return value; }
        set { this.value = value; CheckVisual(); }
    }

    private void CheckVisual()
    {
        for(int i = 0; i < oreData.oreData.Count; i++)
        {

        }
    }
}