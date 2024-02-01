using System;
using System.Collections.Generic;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine;

public class Banner : MonoBehaviour
{

    // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-8741261465579918/6124360926";
#else
      private string _adUnitId = "unexpected_platform";
#endif

    BannerView _bannerView;


    public void Start()
    {
        MobileAds.Initialize(initStatus =>
        {

            Dictionary<string, AdapterStatus> map = initStatus.getAdapterStatusMap();
            foreach (KeyValuePair<string, AdapterStatus> keyValuePair in map)
            {
                string className = keyValuePair.Key;
                AdapterStatus status = keyValuePair.Value;
                switch (status.InitializationState)
                {
                    case AdapterState.NotReady:
                        // The adapter initialization did not complete.
                        Debug.Log("Adapter: " + className + " not ready.");
                        break;
                    case AdapterState.Ready:
                        // The adapter was successfully initialized.
                        Debug.Log("Adapter: " + className + " is initialized.");
                        break;
                }
            }

            Debug.Log("Creating banner view");
            _bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Center);
            // create our request used to load the ad.
            var adRequest = new AdRequest();
            // send the request to load the ad.
            Debug.Log("Loading banner ad.");
            ListenToAdEvents();
            _bannerView.LoadAd(adRequest);
        });
    }

    private void ListenToAdEvents()
    {
        // Raised when an ad is loaded into the banner view.
        _bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response : "
                + _bannerView.GetResponseInfo());

            ShowBannerAd();
        };
        // Raised when an ad fails to load into the banner view.
        _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                + error);
        };
        // Raised when the ad is estimated to have earned money.
        _bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Banner view paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        _bannerView.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        _bannerView.OnAdClicked += () =>
        {
            Debug.Log("Banner view was clicked.");
        };
        // Raised when an ad opened full screen content.
        _bannerView.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        _bannerView.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view full screen content closed.");
        };
    }

    public void ShowBannerAd()
    {
        if (_bannerView != null && !_bannerView.IsDestroyed)
        {
            _bannerView.Show();
        }

    }

    public void HideBannerAd()
    {
        if (_bannerView != null && !_bannerView.IsDestroyed)
        {
            _bannerView.Hide();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}