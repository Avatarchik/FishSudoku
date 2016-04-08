using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class LivesController : MonoBehaviour
{
    public List<Image> allLifes;
    public Sprite lifeYesSprite;
    public Sprite lifeNoSprite;

    private int count = 0;

    public GameObject noLifeImage;
    public Image lifeRechargeImage;
    public Text rechargeTime;

    private int seconds;
    void Start()
    {
        if(DateTime.Now < UserInfo.Instance.timeToStartLifesTimer)
        {
            seconds = (int)(UserInfo.Instance.timeToStartLifesTimer - DateTime.Now).TotalSeconds / 900;
            UserInfo.Instance.lifeCount = 5 - (seconds + 1);
        }

        RefreshLifes();
        Debug.Log("Lives Is Loaded");
    }

    public void RefreshLifes()
    {
        count = 0;
        foreach (var mm in allLifes)
        {
            if (count < UserInfo.Instance.lifeCount)
            {
                mm.sprite = lifeYesSprite;
            }
            else
            {
                mm.sprite = lifeNoSprite;
            }

            count++;
        }

        if(UserInfo.Instance.lifeCount < 5)
        {
            noLifeImage.SetActive(true);
            lifeRechargeImage.gameObject.SetActive(true);
            rechargeTime.gameObject.SetActive(true);

            CheckTimer();
        }
        else
        {
            noLifeImage.SetActive(false);
            lifeRechargeImage.gameObject.SetActive(false);
            rechargeTime.gameObject.SetActive(false);
        }
    }

    public void SubtractOneLife()
    {
        if(UserInfo.Instance.canUseLife)
        {
            if (UserInfo.Instance.lifeCount == 5)
                UserInfo.Instance.timeToStartLifesTimer = DateTime.Now.AddMinutes(15);
            else
                UserInfo.Instance.timeToStartLifesTimer = UserInfo.Instance.timeToStartLifesTimer.AddMinutes(15);

            UserInfo.Instance.lifeCount--;
            UserInfo.Instance.SaveUserInfo();

            RefreshLifes();
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SubtractOneLife();
        }
    }

    private int timeToRefillLifes;
    private int timeToRefillNextLife;
    private TimeSpan timer;
    public void CheckTimer()
    {
        CancelInvoke("TimerLifes");

        if (UserInfo.Instance.lifeCount <= 5)
        {
            if (DateTime.Now >= UserInfo.Instance.timeToStartLifesTimer)
            {
                UserInfo.Instance.lifeCount = 5;
            }
            else
            {
                timeToRefillLifes = (int)(UserInfo.Instance.timeToStartLifesTimer - DateTime.Now).TotalSeconds;

                if (timeToRefillLifes % 900 == 0)
                {
                    timeToRefillNextLife = 900;
                }
                else
                {
                    timeToRefillNextLife = timeToRefillLifes % 900;
                }

                Debug.Log(timeToRefillNextLife);
                timer = new TimeSpan(0, 0, timeToRefillNextLife);
                InvokeRepeating("TimerLifes", 0, 1.1f);
            }

            UserInfo.Instance.SaveUserInfo();
        }
    }

    public void TimerLifes()
    {
        timer = timer.Subtract(new TimeSpan(0, 0, 1));
        rechargeTime.text = String.Format("{0:D2}:{1:D2}", timer.Minutes, timer.Seconds);
       // timerTick = 1f - ((float)timer.TotalSeconds) / 7200f;
        //hintFillImage.fillAmount = timerTick;

        if (timer <= TimeSpan.Zero)
        {
            CancelInvoke("TimerLifes");
            UserInfo.Instance.lifeCount++;
            UserInfo.Instance.SaveUserInfo();

            RefreshLifes();
        }
    }

    public void ReWriteTime(int _count)
    {
        UserInfo.Instance.timeToStartLifesTimer = UserInfo.Instance.timeToStartLifesTimer - new TimeSpan(0, 15 * _count, 0);
        UserInfo.Instance.SaveUserInfo();
    }
}
