using System;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdsManager {
    
#if UNITY_ANDROID
    private const string AD_UNIT_ID = "ca-app-pub-8380134935999475/8241869369";
#elif UNITY_IOS
    private const string AD_UNIT_ID = "ca-app-pub-8380134935999475/8241869369";
#else
    private const string AD_UNIT_ID = "unexpected_platform";
#endif

    private static AdsManager instance;

    private TopGameManager gameManager;
    private AppOpenAd ad;
    private bool isShowingAd = false;

    // COMPLETE: Add loadTime field
    private DateTime loadTime;

    public static AdsManager Instance {
        get {
            if (instance == null) {
                instance = new AdsManager();
            }
            return instance;
        }
    }

    private bool IsAdAvailable {
        get {
            // COMPLETE: Consider ad expiration
            return ad != null && (System.DateTime.UtcNow - loadTime).TotalHours < 4;
        }
    }

    public void LoadAd() {
        var request = new AdRequest.Builder().Build();

        // Load an app open ad for portrait orientation
        AppOpenAd.LoadAd(AD_UNIT_ID, ScreenOrientation.Portrait, request, ((appOpenAd, error) => {
            if (error != null) {
                // Handle the error.
                Debug.LogFormat("Failed to load the ad. (reason: {0})", error.LoadAdError.GetMessage());
                return;
            }

            // App open ad is loaded
            ad = appOpenAd;
            Debug.Log("App open ad loaded");
            
            // COMPLETE: Keep track of time when the ad is loaded.
            loadTime = DateTime.UtcNow;
        }));
    }

    public void ShowAdIfAvailable(TopGameManager manager) {
        if (!IsAdAvailable || isShowingAd) {
            return;
        }

        gameManager = manager;
        ad.OnAdDidDismissFullScreenContent += HandleAdDidDismissFullScreenContent;
        ad.OnAdFailedToPresentFullScreenContent += HandleAdFailedToPresentFullScreenContent;
        ad.OnAdDidPresentFullScreenContent += HandleAdDidPresentFullScreenContent;
        ad.OnAdDidRecordImpression += HandleAdDidRecordImpression;
        ad.OnPaidEvent += HandlePaidEvent;

        ad.Show();
    }

    private void HandleAdDidDismissFullScreenContent(object sender, EventArgs args) {
        Debug.Log("Closed app open ad");
        // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
        gameManager.RetryGame();
        ad = null;
        isShowingAd = false;
        LoadAd();
    }

    private void HandleAdFailedToPresentFullScreenContent(object sender, AdErrorEventArgs args) {
        Debug.LogFormat("Failed to present the ad (reason: {0})", args.AdError.GetMessage());
        // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
        ad = null;
        LoadAd();
    }

    private void HandleAdDidPresentFullScreenContent(object sender, EventArgs args) {
        Debug.Log("Displayed app open ad");
        isShowingAd = true;
    }

    private void HandleAdDidRecordImpression(object sender, EventArgs args) {
        Debug.Log("Recorded ad impression");
    }

    private void HandlePaidEvent(object sender, AdValueEventArgs args) {
        Debug.LogFormat("Received paid event. (currency: {0}, value: {1}",
            args.AdValue.CurrencyCode, args.AdValue.Value);
    }
}
