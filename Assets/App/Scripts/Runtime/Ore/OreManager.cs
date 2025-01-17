using System.Collections.Generic;
using UnityEngine;

public class OreManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Tooltip("Nombre d'objets � instantier au start")] int oreToInstantiateOnStart;

    [Header("References")]
    [SerializeField] Ore orePrefab;

    Queue<Ore> ores = new ();

    [Space(10)]
    // RSO
    [SerializeField] RSO_OreManager rsoOreManager;

    // RSF
    // RSP

    //[Header("Output")]

    private void OnEnable()
    {
        rsoOreManager.Value = this;
    }
    private void OnDisable()
    {
        rsoOreManager.Value = null;
    }

    private void Start()
    {
        for (int i = 0; i < oreToInstantiateOnStart; i++)
        {
            CreateNewOre();
        }
    }

    public Ore InstantiateOre()
    {
        if (ores.Count <= 0) CreateNewOre();
        return ores.Dequeue();
    }

    public void DestroyOre(Ore ore)
    {
        ore.gameObject.SetActive(false);
        ores.Enqueue(ore);
    }

    void CreateNewOre()
    {
        Ore ore = Instantiate(orePrefab, transform);
        ore.gameObject.SetActive(false);
        ores.Enqueue(ore);
    }
}