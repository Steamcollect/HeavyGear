using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class StageData
{
    public Tuple<string,bool>[] stagesState = Array.Empty<Tuple<string, bool>>();
    public Tuple<string,bool> stageNextFactoryState = default;
    
    public int currentStage;
    public string nextStageName;
}