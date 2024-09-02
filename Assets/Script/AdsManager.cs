using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;
    string GameID = "4160483";
    bool test = true;
    string placementId2 = "video";
    string placementId3 = "rewardedVideo";
    public ShopManager shopManager;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Advertisement.Initialize(GameID, test);
        //Advertisement.AddListener(this);
    }


    public void showads()
    {
        Advertisement.Show(placementId2);
    }

    public void showRewardads()
    {
        //var result = new ShowOptions { resultCallback = OnUnityAdsDidFinish };
        //Advertisement.Show(placementId3, result);
    }

    public void OnUnityAdsDidFinish(ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            GameManager.Instance.Coin += 200;
            shopManager.updateCall();
            Debug.Log("Finished");
        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.Log("Ads skipped");
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.Log("Ads error");
        }
    }
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == placementId2)
        {
            Advertisement.Show(placementId2);
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string myPlacementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
}
