using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [Title("Settings")] 
    [SerializeField] [TableList] private List<Content> contentCrate;
    [PropertySpace]
    [Button("Debug Crate Console")] private void DebugCrate() => Debug.Log(GetRandomContentByWeight().data);
    
    public Content GetRandomContentByWeight()
    {
        float totalWeight = 0.0f;
        float minWeight = Mathf.Infinity;
        for (int i = 0; i < contentCrate.Count; ++i)
        {
            minWeight = contentCrate[i].weight < minWeight ? contentCrate[i].weight : minWeight;
            totalWeight += contentCrate[i].weight;
        }
        float randomWeight = UnityEngine.Random.Range(minWeight, totalWeight);
        float cursorWeight = 0.0f;

        for (int i = 0; i < contentCrate.Count; ++i)
        {
            cursorWeight += contentCrate[i].weight;
            if (cursorWeight >= randomWeight)
            {
                return contentCrate[i];
            }
        }
        Debug.Log("Error Weight Crate, Cant find a content available!");
        return default;
    }
}

[Serializable]
public class Content
{
    public string data;
    [MinValue(0.001f)]public float weight = 0.001f;
}