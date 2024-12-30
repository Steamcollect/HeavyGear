using System;
using UnityEngine;

[Serializable]
public class StageData
{
    [Header("Settings")]
    public SSO_FactoryStageData factoryStageData;
    public int currentStage;
    public bool lastStageReached;
}