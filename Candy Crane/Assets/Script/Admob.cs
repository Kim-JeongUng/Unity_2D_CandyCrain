using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
public class Admob : MonoBehaviour
{
    public Canvas MyCanvas;
    private InterstitialAd interstitial;
    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        Debug.Log("BBBBB");
        ReplayGame();
        RequestInterstitial();

    }
    private void RequestInterstitial()
    {
        #if UNITY_ANDROID
            //string adUnitId = "ca-app-pub-3940256099942544/1033173712";
            string adUnitId = "ca -app-pub-4992780716235419/8376657236";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/4411468910";
        #else
            string adUnitId = "unexpected_platform";
            Debug.Log("AAAASDSAVAD");
        #endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId); 
        
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }   

    public void ReplayGame()
    {
        RequestInterstitial();
        StartCoroutine(showInterstitial());
        IEnumerator showInterstitial()
        {
            while (!this.interstitial.IsLoaded())
            {
                yield return new WaitForSeconds(0.2f);
            }
            this.interstitial.Show();
            //MyCanvas.sortingOrder = -1;
            Debug.Log("Open");
        }

    }
    public void HandleOnAdClosed(object sender, System.EventArgs args)
    {
        Debug.Log("Closesd");
        this.interstitial.Destroy();
        SceneManager.LoadScene("LobyScene");
    }
}
*/
public class Admob : MonoBehaviour
{
    public bool isTestMode;
    public Button FrontAdsBtn;


    void Start()
    {
        var requestConfiguration = new RequestConfiguration
           .Builder()
           .SetTestDeviceIds(new List<string>() { }) // test Device ID
           .build();

        MobileAds.SetRequestConfiguration(requestConfiguration);

      
        LoadFrontAd();
    }

    void Update()
    {
        //FrontAdsBtn.interactable = frontAd.IsLoaded();
    }

    AdRequest GetAdRequest()
    {
        return new AdRequest.Builder().Build();
    }


    const string frontTestID = "ca-app-pub-3940256099942544/1033173712";
    const string frontID = "ca-app-pub-4992780716235419/8376657236";
    InterstitialAd frontAd;


    void LoadFrontAd()
    {
        frontAd = new InterstitialAd(isTestMode ? frontTestID : frontID);
        frontAd.LoadAd(GetAdRequest());
        frontAd.OnAdClosed += (sender, e) =>
        {
            SceneManager.LoadScene("LobyScene");
        };
    }

    public void ShowFrontAd()
    {
        /*frontAd.Show();
        LoadFrontAd();

        if (!frontAd.IsLoaded())
        {
            SceneManager.LoadScene("LobyScene");
        }*/
        StartCoroutine(showInterstitial());

        IEnumerator showInterstitial()
        {
            int cnt = 0;
            while (!frontAd.IsLoaded() && cnt<5)
            {
                cnt++;
                yield return new WaitForSeconds(0.2f);
            }
            if (cnt >= 5)
            {
                SceneManager.LoadScene("LobyScene");
            }
            frontAd.Show();
            LoadFrontAd();
        }
    }

}
