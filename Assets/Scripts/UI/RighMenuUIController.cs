using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RighMenuUIController : MonoBehaviour
{
    public Slider musicSlider;
    public Slider volumeSlider;
    public AudioSource musicSource;
    public AudioSource soundSource;
    public GameObject mapButton;

    public void OnChangeMusicVolume()
    {
        musicSource.volume = musicSlider.value;
    }

    public void OnChangeSoundVolume()
    {
        soundSource.volume = volumeSlider.value;
    }

    public GameObject rightMenuBg;
    public GameObject rightMenuPanel;
    private bool isSettingPanelActive = false;
    public void SettingsMenuButton()
    {
        if(isSettingPanelActive)
        {
            rightMenuBg.SetActive(false);
            HidePanel();
            isSettingPanelActive = false;
            if(mapButton != null)
                mapButton.GetComponent<Animation>().Stop();
        }
        else
        {
            isSettingPanelActive = true;
            rightMenuBg.SetActive(true);
            ShowPanel();
            if (mapButton != null)
                mapButton.GetComponent<Animation>().Play();
        }
    }

    private void ShowPanel()
    {
        rightMenuPanel.GetComponent<Animation>().Play("RightPanelShow");
    }

    private void HidePanel()
    {
        rightMenuPanel.GetComponent<Animation>().Play("RightPanelHide");
    }

    public void BackToMapButton()
    {
        if (UserInfo.Instance.canUseLife)
        {
            GameObject.FindObjectOfType<LivesController>().SubtractOneLife();
        }

        LoadingController.nextSceneName = "Map";
        StartCoroutine(GlobalVariables.LoadingSomeScene("Loading", false));
        //SceneManager.LoadScene("Map",LoadSceneMode.Single);
    }


    public void BuyFullVersion()
    {
        Application.OpenURL("http://google.com.ua");
    }
}
