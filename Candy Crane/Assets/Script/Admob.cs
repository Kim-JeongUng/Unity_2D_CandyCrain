using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class Admob : MonoBehaviour
{
    public Canvas MyCanvas;
    private InterstitialAd interstitial;

    private void RequestInterstitial()
    {
#if UNITY_ANDROID
    string adUnitId = "ca-app-pub-4992780716235419/8376657236";
#elif UNITY_IPHONE
    string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
    string adUnitId = "unexpected_platform";
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
