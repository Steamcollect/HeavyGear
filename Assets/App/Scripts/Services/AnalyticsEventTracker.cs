using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Analytics;

public abstract class AnalyticsEventTracker : MonoBehaviour
{
    [Title("Settings")]
    [SerializeField] private string eventName;

    public void OnDataTrackedChanged(string nameData,object data)
    {
        var analyticsResult = Analytics.CustomEvent(eventName, new Dictionary<string, object>
        {
            { nameData, data }
        });
        print(analyticsResult);
    }

    public void OnDataTrackedChanged(List<Tuple<string, object>> dataList)
    {
        var analyticsResult = Analytics.CustomEvent(eventName, dataList.ToDictionary(o=> o.Item1, o=> o.Item2 as object));
        print(analyticsResult);
    }
    
}