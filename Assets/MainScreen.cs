using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadBannerButtonClicked()
    {
        SceneManager.LoadScene("Banner");
    }

    public void loadInterstitialButtonClicked()
    {
        SceneManager.LoadScene("Interstitial");
    }

    public void loadRewardedButtonClicked()
    {
        SceneManager.LoadScene("Rewarded");
    }
}
