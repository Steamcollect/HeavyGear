using System.Collections.Generic;
using BT.ScriptablesObject;
using JetBrains.Annotations;
using UnityEngine;
using Sirenix.OdinInspector;
using Unity.Services.Analytics;

public abstract class AnalyticsEventTracker<T> : MonoBehaviour
 {
     [Title("Settings")]
     [SerializeField][EnumToggleButtons] protected AnalyticEventName eventName;
     [SerializeField] [NotNull] protected string nameDataT;
 
     [Title("Input")] 
     [SerializeField] protected RuntimeScriptableEvent<T> rseDataTracked;

     [Title("Output")] 
     [SerializeField] protected RSE_SendAnalyticsEvent rseSendAnalyticsEvent;
 
     protected virtual void OnEnable() => rseDataTracked.action += OnDataTrackedChanged;
     protected virtual void OnDisable() => rseDataTracked.action -= OnDataTrackedChanged;
 
     public virtual void OnDataTrackedChanged(T dataT)
     {
         CustomEvent customEvent = new CustomEvent(eventName.ToString())
         {
             { nameDataT, dataT}
         };
         Debug.Log($"Analytics Tracker: SendAnalyticsEvent {customEvent}");
         rseSendAnalyticsEvent.Call(customEvent);
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
    [Title("Output")] 
    [SerializeField] protected RuntimeScriptableEvent<CustomEvent> rseSendAnalyticsEvent;

    protected void OnEnable() => rseDataTracked.action += OnDataTrackedChanged;
    protected void OnDisable() => rseDataTracked.action -= OnDataTrackedChanged;

    public virtual void OnDataTrackedChanged(T dataT,T1 dataT1)
    {
        CustomEvent customEvent = new CustomEvent(eventName.ToString())
        {
            { nameDataT, dataT},
            { nameDataT1, dataT1 }
        };
        rseSendAnalyticsEvent.Call(customEvent);
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
    [Title("Output")] 
    [SerializeField] protected RuntimeScriptableEvent<CustomEvent> rseSendAnalyticsEvent;

    protected void OnEnable() => rseDataTracked.action += OnDataTrackedChanged;
    protected void OnDisable() => rseDataTracked.action -= OnDataTrackedChanged;

    public virtual void OnDataTrackedChanged(T dataT,T1 dataT1,T2 dataT2)
    {
        CustomEvent customEvent = new CustomEvent(eventName.ToString())
        {
            { nameDataT, dataT},
            {nameDataT1, dataT1},
            {nameDataT2, dataT2}
        };
        rseSendAnalyticsEvent.Call(customEvent);
    }
}