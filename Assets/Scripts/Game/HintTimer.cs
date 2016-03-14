using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class HintTimer : MonoBehaviour
{
    public float timeToEndSeconds = 60f;
    public Text timerText;
    public Image fillImage;
    public bool IsForwardFill = true;

    private TimeSpan timerCount;
    private float timeToEndSecondsMax;

    void Start()
    {
        timerCount = new TimeSpan();
        timerCount = TimeSpan.FromSeconds(timeToEndSeconds);
        timeToEndSecondsMax = timeToEndSeconds;

        if (IsForwardFill)
            fillImage.fillAmount = 0;
        else
            fillImage.fillAmount = 1;

        InvokeRepeating("TimerCountDown", 0.5f, 1f);
    }

    private void TimerCountDown()
    {
        if (timeToEndSeconds != 0)
        {
            timeToEndSeconds--;
            timerCount = TimeSpan.FromSeconds(timeToEndSeconds);
            timerText.text = String.Format("{0:D2}:{1:D2}",timerCount.Minutes,timerCount.Seconds);

            if(IsForwardFill)
                fillImage.fillAmount = 1f - timeToEndSeconds / timeToEndSecondsMax; // [0 to 1]
            else
                fillImage.fillAmount = timeToEndSeconds / timeToEndSecondsMax; // [1 to 0]
        }
        else
        {
            CancelInvoke("TimerCountDown");
            Debug.Log("Timer is end!!! You Lose!!!");
            return;
        }
    }

    public void Pause()
    {
        CancelInvoke("TimerCountDown");
    }

    public void UnPause()
    {
        InvokeRepeating("TimerCountDown", 0.5f, 1f);
    }
}
