using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SSO_Crate", menuName = "ScriptableObject/Crate/SSO_Crate")]
public class SSO_Crate : ScriptableObject
{
    [Title("Settings")] 
    [MinValue(1)][InfoBox("Number of Content picked")]public int quantityObjectLoot = 1;
    [PropertySpace(10)]
    [TableList] public List<ContentCrate> contentCrate;
#if UNITY_EDITOR
    [PropertySpace(10)]
    [HorizontalGroup("Split")] [SerializeField] [Wrap(1,99999999)] 
    private int crateOpenDebug = 1;
    [PropertySpace(5)]
    [HorizontalGroup("Split")] [Button(buttonSize: ButtonSizes.Large, name:"Debug Crate Console")] 
    private void DebugCrate()
    {
        if (contentCrate.Count == 0) return;
        Dictionary<ContentCrate, int> resultDebug = new();
        for (int i = 0; i < crateOpenDebug; i++)
        {
            var elems = GetRandomContentByWeight();
            for (int j = 0; j < elems.Count; j++)
                if (!resultDebug.TryAdd(elems[j], 1)) resultDebug[elems[j]] += 1;
        }
        var txtDebugValue = "";
        resultDebug.ForEach(o=>txtDebugValue += o.Key.data + " : " + o.Value + "\n");
        Debug.Log(txtDebugValue);
    }

    private void OnValidate()
    {
        // print(EditorWindow.GetWindow<EditorWindow>());
    }

#endif
    
    /// <summary>
    /// Algorithm based on weight of item and get one randomly
    /// </summary>
    /// <returns> Content with it weight and the context of it (Function link to Data Game)</returns>
    public List<ContentCrate> GetRandomContentByWeight()
    {
        //Calcul Weight List
        float totalWeight = 0.0f;
        float minWeight = Mathf.Infinity;
        for (int i = 0; i <contentCrate.Count; ++i)
        {
            minWeight = contentCrate[i].weight < minWeight ? contentCrate[i].weight : minWeight;
            totalWeight += contentCrate[i].weight;
        }
        
        List<ContentCrate> result = new List<ContentCrate>();
        
        for (int j = 0; j < quantityObjectLoot; ++j)
        {
            float randomWeight = UnityEngine.Random.Range(minWeight, totalWeight);
            float cursorWeight = 0.0f;

            for (int i = 0; i < contentCrate.Count; ++i)
            {
                cursorWeight += contentCrate[i].weight;
                if (cursorWeight >= randomWeight)
                {
                    result.Add(contentCrate[i]);
                    break;
                }
            }
            if (result.Count < j+1) Debug.Log($"Error Weight in {this.name}, cant find a content available for itération {j}!");
        }
        return result;
    }
}

[Serializable]
public class ContentCrate
{
    [AssetsOnly][Required] public SSO_ContentCrateData data;
    [MinValue(0.001f)] public float weight = 0.001f;
}