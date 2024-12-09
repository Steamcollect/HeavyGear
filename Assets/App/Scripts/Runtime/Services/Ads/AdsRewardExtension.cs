using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Advertisements;
public class AdsRewardExtension : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    #if UNITY_ANDROID || UNITY_EDITOR
    private readonly string adUnitId = "Rewarded_Android";
    #elif UNITY_IOS
    private readonly string adUnitId = "Rewarded_iOS";
    #else
    private readonly string adUnitId = "unexepedted_platform";
    #endif

    private bool adsExtensionReady;
    
    [Title("Output")]
    
    public Action OnAdsFinished;
    public Action OnAdsRunning;
    public Action OnGiveReward;
    
    public void ShowAds()
    {
        if (!adsExtensionReady) return;
        Advertisement.Show(adUnitId,this);
    }

    public void LoadAds()
    {
        Advertisement.Load(adUnitId,this);
    }

    #region AdUnitLoad
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);
        adsExtensionReady = true;
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
    }
    #endregion

    #region AdUnitShow

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (this.adUnitId.Equals(adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            OnGiveReward?.Invoke();
        }
        OnAdsFinished?.Invoke();
        LoadAds();
    }
    
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }
    #endregion

    public void OnUnityAdsShowStart(string adUnitId)
    {
        OnAdsRunning?.Invoke();
    }
    public void OnUnityAdsShowClick(string adUnitId) { }
}