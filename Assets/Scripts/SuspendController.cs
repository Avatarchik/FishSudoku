using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SuspendController : MonoBehaviour
{
    private string sceneName;
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    #region Suspend
    void OnApplicationFocus(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            switch (sceneName)
            {
                case "PlayLevel4x4":
                case "PlayLevel5x5":
                case "PlayLevel6x6":
                case "PlayLevel7x7":
                case "PlayLevel8x8":
                case "PlayLevel9x9":
                    UserInfo.Instance.exitAfterGameStart = 1;
                    UserInfo.Instance.SaveUserInfo();
                    break;
            }

            if (DateTime.Now < UserInfo.Instance.timeToStartLifesTimer)
            {
                if (PlayerPrefs.HasKey("LifeFullLocalNotification"))
                {
                    NativePluginsController.CancelSomeLocalNotification(PlayerPrefs.GetInt("LifeFullLocalNotification"));
                    PlayerPrefs.DeleteKey("LifeFullLocalNotification");

                    NativePluginsController.LifeFullLocalNotification(UserInfo.Instance.timeToStartLifesTimer);
                }
                else
                {
                    NativePluginsController.LifeFullLocalNotification(UserInfo.Instance.timeToStartLifesTimer);
                }
            }

            if (DateTime.Now < UserInfo.Instance.timeToFillHint)
            {
                if (PlayerPrefs.HasKey("HintRefillLocalNotification"))
                {
                    NativePluginsController.CancelSomeLocalNotification(PlayerPrefs.GetInt("HintRefillLocalNotification"));
                    PlayerPrefs.DeleteKey("HintRefillLocalNotification");

                    NativePluginsController.HintRefillLocalNotification(UserInfo.Instance.timeToFillHint);
                }
                else
                {
                    NativePluginsController.HintRefillLocalNotification(UserInfo.Instance.timeToFillHint);
                }
            }
        }   
        else
        {
            switch (sceneName)
            {
                case "PlayLevel4x4":
                case "PlayLevel5x5":
                case "PlayLevel6x6":
                case "PlayLevel7x7":
                case "PlayLevel8x8":
                case "PlayLevel9x9":
                    UserInfo.Instance.exitAfterGameStart = 0;
                    UserInfo.Instance.SaveUserInfo();
                    break;
            }
        }
    }
    #endregion
}
