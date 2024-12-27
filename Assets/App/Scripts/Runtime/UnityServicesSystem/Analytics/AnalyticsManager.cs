using System;
using Sirenix.OdinInspector;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
public class AnalyticsManager : MonoBehaviour
{
    
    [Title("Input")] 
    [SerializeField] private RSE_SendAnalyticsEvent rseSendAnalyticsEvent;

    private bool isInitialized;
    
    private void Start()
    {
        if (UnityServices.State == ServicesInitializationState.Uninitialized)
        {
            Debug.Log("Initializing Analytics Manager");
            UnityServices.Initialized += OnServiceInitialized;
            UnityServices.InitializeFailed += OnServiceInitializationFailed;
            AwaitServiceInit();
            return;
        }
        isInitialized = true;
    }

    private async void AwaitServiceInit()
    {
        try
        {
            await UnityServices.InitializeAsync();
        }
        catch (Exception e)
        {
            Debug.LogWarning(e + "\n" + "Service Initialization Failed");
        }
    }

    private void OnServiceInitialized()
    {
        Debug.Log("AnalyticsManager: Service initialized");
        AnalyticsService.Instance.StartDataCollection();
        isInitialized = true;
        PostServiceInitialization();
    }

    private void OnServiceInitializationFailed(Exception exception)
    {
        Debug.Log("AnalyticsManager: Service initialization failed: " + exception.Message);
        PostServiceInitialization();
        
    }

    private void PostServiceInitialization()
    {
        UnityServices.InitializeFailed -= OnServiceInitializationFailed;
        UnityServices.Initialized -= OnServiceInitialized;
    }

    private void OnEnable()
    {
        rseSendAnalyticsEvent.action += CollectAnalyticsEvent;
    }

    private void OnDisable()
    {
        rseSendAnalyticsEvent.action -= CollectAnalyticsEvent;
    }

    private void CollectAnalyticsEvent(CustomEvent customEvent)
    {
        if (!isInitialized) return;
        Debug.Log($"AnalyticsManager: CollectAnalyticsEvent {customEvent}");
        AnalyticsService.Instance.RecordEvent(customEvent);
        AnalyticsService.Instance.Flush();
    }
    
}