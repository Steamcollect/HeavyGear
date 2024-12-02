using BigFloatNumerics;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class OreData
{
    [PreviewField(64, Alignment = ObjectFieldAlignment.Center)]
    [TableColumnWidth(64, Resizable = false)]
    public Sprite visual;

    public OreStatsData stats;

    public OreParticleData particle;

    public override string ToString()
    {
        return stats.name;
    }
}

[Serializable]
public class OreStatsData
{
    public string name;

    public string defaultValue;

    public int index;

    public BigNumber baseValue => new BigNumber(defaultValue);
}

[Serializable]
public class OreParticleData
{
    public ParticleSystem particleSystem;
    public Color particleColor;
}