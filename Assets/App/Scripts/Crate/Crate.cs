using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

public class Crate : MonoBehaviour
{
    [Title("Reference")] 
    [SerializeField] private SSO_CrateData ssoCrateData;
    
#if UNITY_EDITOR
    [HorizontalGroup("Split")] [SerializeField] [Wrap(1,99999999)] 
    private int crateOpenDebug = 1;
    [HorizontalGroup("Split")] [Button(buttonSize: ButtonSizes.Large, name:"Debug Crate Console")] 
    private void DebugCrate()
    {
        Dictionary<Content, int> contents = new();
        for (int i = 0; i < crateOpenDebug; i++)
        {
            var elem = GetRandomContentByWeight();
            if (contents.ContainsKey(elem)) contents[elem] += 1;
            else contents.Add(elem, 1);
        }

        var txtDebugValue = "";
        contents.ForEach(o=>txtDebugValue += o.Key.data + " : " + o.Value + "\n");
        Debug.Log(txtDebugValue);
    }
#endif
    
    public Content GetRandomContentByWeight()
    {
        float totalWeight = 0.0f;
        float minWeight = Mathf.Infinity;
        for (int i = 0; i < ssoCrateData.contentCrate.Count; ++i)
        {
            minWeight = ssoCrateData.contentCrate[i].weight < minWeight ? ssoCrateData.contentCrate[i].weight : minWeight;
            totalWeight += ssoCrateData.contentCrate[i].weight;
        }
        float randomWeight = UnityEngine.Random.Range(minWeight, totalWeight);
        float cursorWeight = 0.0f;

        for (int i = 0; i < ssoCrateData.contentCrate.Count; ++i)
        {
            cursorWeight += ssoCrateData.contentCrate[i].weight;
            if (cursorWeight >= randomWeight)
            {
                return ssoCrateData.contentCrate[i];
            }
        }
        Debug.Log("Error Weight Crate, Cant find a content available!");
        return default;
    }
    
}