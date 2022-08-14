using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    private BannerView bannerView;
    public InterstitialAd interstitialAd;

    void Start()
    {
        MobileAds.Initialize(InitializationStatus => { });
        this.RequestBanner();
        this.RequestInterstitial();
        this.interstitialAd.Show();
    }

    public void RequestBanner()
    {
        string bannerId = "ca-app-pub-6510879145533406/8413575747";

        this.bannerView = new BannerView(bannerId, adSize: AdSize.Banner, AdPosition.Bottom);

        AdRequest adRequest = new AdRequest.Builder().Build();

        this.bannerView.LoadAd(adRequest);
    }
    public void RequestInterstitial()
    {
        string interstitialId = "ca-app-pub-6510879145533406/8413575747";

        this.interstitialAd = new InterstitialAd(interstitialId);

        AdRequest adRequest = new AdRequest.Builder().Build();

        this.interstitialAd.LoadAd(adRequest);
    }
}
