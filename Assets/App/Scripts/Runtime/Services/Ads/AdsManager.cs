using System;
using UnityEngine;
// using com.unity3d.mediation;
public class AdsManager : MonoBehaviour
{
// #if UNITY_ANDROID
//     private string appKey = "205dd9305";
// #else
//     private string appKey = "unexpected_platform";
// #endif
//     
//     private void Awake()
//     {
//         Debug.Log("unity-script: unity version" + IronSource.unityVersion());
//         
//         Debug.Log("unity-script: IronSource.Agent.validateIntegration");
//         IronSource.Agent.validateIntegration();
//         
//         LevelPlayAdFormat[] legacyAdFormats = { LevelPlayAdFormat.REWARDED };
//         
//         Debug.Log("unity-script: LevelPlay SDK initialization");
//         LevelPlay.Init(appKey, "demoUserUnity",legacyAdFormats);
//         
//         LevelPlay.OnInitSuccess += SdkInitializationCompletedEvent;
//         LevelPlay.OnInitFailed += SdkInitializationFailedEvent;
//         
//         
//     }
//
//     void OnApplicationPause(bool isPaused)
//     {
//         Debug.Log("unity-script: OnApplicationPause = " + isPaused);
//         IronSource.Agent.onApplicationPause(isPaused);
//     }
//     
//     private void SdkInitializationFailedEvent(LevelPlayInitError obj)
//     {
//         Debug.Log("unity-script: I got SdkInitializationCompletedEvent with config: "+ obj);
//     }
//
//     private void SdkInitializationCompletedEvent(LevelPlayConfiguration obj)
//     {
//         Debug.Log("unity-script: I got SdkInitializationFailedEvent with error: "+ obj);
//         EnableAds();
//     }
//
//     private void EnableAds()
//     {
//         // IronSource.Agent.setManualLoadRewardedVideo(true);
//         IronSource.Agent.loadRewardedVideo();
//         
//         // //Add AdInfo Rewarded Video Events
//         // IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
//         // IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
//         // IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
//         // IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
//         // IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
//         // IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
//         // IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;
//
//     }
//
//     public void ShowRewardedVideo()
//     {
//         if (IronSource.Agent.isRewardedVideoAvailable())
//         {
//             IronSource.Agent.showRewardedVideo();
//         }
//         else
//         {
//             Debug.Log("unity-script: Rewarded video not available");
//         }
//     }
//
}