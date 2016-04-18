using System;
using UnityEngine;

public class SuspendController : MonoBehaviour
{    
    #region Suspend
    void OnApplicationFocus(bool pauseStatus)
    {
        if (!pauseStatus)
        {
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
    }
    #endregion

   
}
