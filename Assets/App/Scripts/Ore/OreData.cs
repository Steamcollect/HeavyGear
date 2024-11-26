using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class OreData
{
    public string name;

    public float baseValue;

    public int index;

    [PreviewField]
    public Sprite visual;

    public override string ToString()
    {
        return name;
    }
}