using System.Collections.Generic;
using BT.ScriptablesObject;
using JetBrains.Annotations;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Analytics;

public abstract class AnalyticsEventTracker<T> : MonoBehaviour
 {
     [Title("Settings")]
     [SerializeField][EnumToggleButtons] protected AnalyticEventName eventName;
     [SerializeField] [NotNull] protected string nameDataT;
 
     [Title("Input")] 
     [SerializeField] protected RuntimeScriptableEvent<T> rseDataTracked;
 
     protected void OnEnable() => rseDataTracked.action += OnDataTrackedChanged;
     protected void OnDisable() => rseDataTracked.action -= OnDataTrackedChanged;
 
     protected void OnDataTrackedChanged(T dataT)
     {
         var analyticsResult = Analytics.CustomEvent(eventName.ToString(), new Dictionary<string, object>
         {
             { nameDataT, dataT}
         });
         print(analyticsResult);
     }
 }
 
public abstract class AnalyticsEventTracker<T,T1> : MonoBehaviour
{
    [Title("Settings")]
    [SerializeField][EnumToggleButtons] protected AnalyticEventName eventName;
    [SerializeField] [NotNull] protected string nameDataT;
    [SerializeField] [NotNull] protected string nameDataT1;

    [Title("Input")] 
    [SerializeField] protected RuntimeScriptableEvent<T,T1> rseDataTracked;

    protected void OnEnable() => rseDataTracked.action += OnDataTrackedChanged;
    protected void OnDisable() => rseDataTracked.action -= OnDataTrackedChanged;

    protected void OnDataTrackedChanged(T dataT,T1 dataT1)
    {
        var analyticsResult = Analytics.CustomEvent(eventName.ToString(), new Dictionary<string, object>
        {
            { nameDataT, dataT},
            {nameDataT1, dataT1}
        });
        print(analyticsResult);
    }
}

public abstract class AnalyticsEventTracker<T,T1,T2> : MonoBehaviour
{
    [Title("Settings")]
    [SerializeField][EnumToggleButtons] protected AnalyticEventName eventName;
    [SerializeField] [NotNull] protected string nameDataT;
    [SerializeField] [NotNull] protected string nameDataT1;
    [SerializeField] [NotNull] protected string nameDataT2;

    [Title("Input")] 
    [SerializeField] protected RuntimeScriptableEvent<T,T1,T2> rseDataTracked;

    protected void OnEnable() => rseDataTracked.action += OnDataTrackedChanged;
    protected void OnDisable() => rseDataTracked.action -= OnDataTrackedChanged;

    protected void OnDataTrackedChanged(T dataT,T1 dataT1,T2 dataT2)
    {
        var analyticsResult = Analytics.CustomEvent(eventName.ToString(), new Dictionary<string, object>
        {
            { nameDataT, dataT },
            {nameDataT1, dataT1},
            {nameDataT2, dataT2}
        });
        print(analyticsResult);
    }
}