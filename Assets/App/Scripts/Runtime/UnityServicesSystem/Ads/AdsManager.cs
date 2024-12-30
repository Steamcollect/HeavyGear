using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener
{
    [Title("Settings")] 
    [SerializeField] private bool debugMode;
    
    [Title("Input")]
    [SerializeField] private RSE_ShowAds rseShowAds;
    
    [Title("Output")]
    [SerializeField] private RSE_AdsRunning rseAdsRunning;
    [SerializeField] private RSE_GiveReward rseGiveReward;
    [SerializeField] private RSE_AdsFinished rseAdsFinished;
    
    #if UNITY_ANDROID || UNITY_EDITOR
    private readonly string gameId = "5744111";
    #elif UNITY_IOS
    private readonly string gameId = "5744110";
    #else
    private readonly string gameId = "unexepedted_platform";
    #endif

    private bool adsEnable;
    private AdsRewardExtension adsRewardExtension;


    private void OnEnable()
    {
        rseShowAds.action += TryShowAds;
        adsRewardExtension.OnAdsFinished += rseAdsFinished.Call;
        adsRewardExtension.OnAdsRunning += rseAdsRunning.Call;
        adsRewardExtension.OnGiveReward += rseGiveReward.Call;
    }
    
    private void OnDisable()
    {
        rseShowAds.action -= TryShowAds;
        adsRewardExtension.OnAdsFinished -= rseAdsFinished.Call;
        adsRewardExtension.OnAdsRunning -= rseAdsRunning.Call;
        adsRewardExtension.OnGiveReward -= rseGiveReward.Call;
    }
    
    private void Awake() => adsRewardExtension = gameObject.AddComponent<AdsRewardExtension>();
    
    private void Start()
    {
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, debugMode,this);
        }
    }
    
    public void OnInitializationComplete()
    {
        adsRewardExtension.LoadAds();
        adsEnable = true;
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    private void TryShowAds()
    {
        if (adsEnable) adsRewardExtension.ShowAds();
    }
    
}