using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class AdsManager : MonoBehaviour
{

    private BannerView bannerView;
    SoundController sound;
    public bool hasShownAdOneTime;
    int AmountofLoss = 0;

    public void Start()
    {
        sound = FindObjectOfType<SoundController>();
//#if UNITY_ANDROID
//        string appId = "ca-app-pub-4073208922433971~9374728733";
//#elif UNITY_IPHONE
//                    string appId = "ca-app-pub-3940256099942544~1458002511";               
//#else
//                    string appId = "unexpected_platform";
//#endif

        //        //        //        // Initialize the Google Mobile Ads SDK.
        //        //        //        MobileAds.Initialize(appId);
        //        MobileAds.Initialize(appId);
        //        RequestInterstitialAds();


    }
    public void Update()
    {
        //if (GameController.gameState == GameController.GameState.GameOver)
        //{

        //    if (!hasShownAdOneTime)
        //    {
        //        hasShownAdOneTime = true;
        //        Invoke("ADWORK", 0.5f);
        //    }
        //}
    }
    private void HandleAdResult(ShowResult result)               //handle the ad fucntioning
    {
        switch (result)
        {
            case ShowResult.Finished:
                //Debug.Log("finish");
                break;
            case ShowResult.Skipped:
                //Debug.Log("player did not see a whole clio");
                break;
            case ShowResult.Failed:
                //Debug.Log("playerfail internet");
                break;
        }
    }
    public void ADWORK()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("", new ShowOptions() { resultCallback = HandleAdResult });          // string to put the id of the commercial 
        }
    }

    public void RequestBanner()
    {
        #if UNITY_ANDROID
        string adUnitId = "ca-app-pub-4073208922433971/7106113455";
#elif UNITY_IPHONE
                    string adUnitId = "ca-app-pub-4073208922433971/4506788701";
        #else
                 string adUnitId = "unexpected_platform";
#endif
        
        //***For Testing in the Device***
        // AdRequest request = new AdRequest.Builder()
        //.AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
        //.AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")  // My test device.
        //.Build();

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();            /*This code is for production **/

        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

    public void showInterstitialAd()
    {
        //Show Ad
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
            
        }

    }
    InterstitialAd interstitial;
    public void RequestInterstitialAds()
    {

        
        string adID = "ca-app-pub-4073208922433971/2597976515";

        #if UNITY_ANDROID
        string adUnitId = adID;
#elif UNITY_IOS
              string adUnitId = "ca-app-pub-4073208922433971/1932596253";
#else
                string adUnitId = adID;
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);

        //***Test***
       // AdRequest request = new AdRequest.Builder()
       //.AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
       //.AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")  // My test device.
       //.Build();

        //***Production***
        AdRequest request = new AdRequest.Builder().Build();

        //Register Ad Close Event
        interstitial.OnAdClosed += Interstitial_OnAdClosed;

        // Load the interstitial with the request.
        interstitial.LoadAd(request);

     

    }
    private void Interstitial_OnAdClosed(object sender, System.EventArgs e) // event
    {
        //Resume Play Sound
        
    }
}







