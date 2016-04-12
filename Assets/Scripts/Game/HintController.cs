using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class HintController : MonoBehaviour
{
    public GameObject hintButton;
    public Text hintCountText;
    public Text hintTimer;
    public Image hintFillImage;
    public GameController gameContr;
    public Sprite itemSpr;
    public Sprite itemSprError;
    public List<GameObject> fishButtonsGO;

    private TimeSpan timer;
    void Start()
    {
        if (!UserInfo.Instance.canUseHint)
        {
            if (UserInfo.Instance.timeToFillHint > DateTime.Now)
            {
                StartHintTimer();
            }
            else
            {
                UserInfo.Instance.hintCount++;
                hintCountText.text = UserInfo.Instance.hintCount.ToString();
                UserInfo.Instance.SaveUserInfo();
                hintFillImage.gameObject.SetActive(false);
            }
        }
    }

    private void StartHintTimer()
    {
        timer = UserInfo.Instance.timeToFillHint - DateTime.Now;
        hintButton.SetActive(false);
        hintTimer.gameObject.SetActive(true);
        hintFillImage.gameObject.SetActive(true);
        InvokeRepeating("InvokeTimer",0,60f);
    }

    float timerTick;
    private void InvokeTimer()
    {
        timer = timer.Subtract(new TimeSpan(0,1,0));
        hintTimer.text = String.Format("{0:D2}:{1:D2}", timer.Hours, timer.Minutes);
        timerTick = 1f - ((float)timer.TotalSeconds) / 7200f;
        hintFillImage.fillAmount = timerTick;

        if(timer <= TimeSpan.Zero)
        {
            UserInfo.Instance.hintCount++;
            UserInfo.Instance.SaveUserInfo();

            hintCountText.text = UserInfo.Instance.hintCount.ToString();
            StopHintTimer();
        }
    }
    
    private List<Image> currentImageList;
    private int rand;
    private bool isHintUsed = true;
    public void UseHintButton()
    {
        if (!isHintUsed)
        {
            if (UserInfo.Instance.canUseHint)
            {
                currentImageList = new List<Image>();
                UserInfo.Instance.hintCount--;
                hintCountText.text = UserInfo.Instance.hintCount.ToString();

                if (UserInfo.Instance.hintCount == 0)
                {
                    UserInfo.Instance.timeToFillHint = DateTime.Now.AddHours(2);

                    StartHintTimer();
                }
                UserInfo.Instance.SaveUserInfo();

                // hint work
                foreach (var mm in gameContr.imageList)
                {
                    if (!mm.GetComponentInChildren<ListElement>().isAnchor)
                    {
                        currentImageList.Add(mm);
                    }
                }

                CheckHint();
            }
        }
    }

    private void CheckHint()
    {
        rand = UnityEngine.Random.Range(0, currentImageList.Count);
        int pos = int.Parse(currentImageList[rand].name);

        int i, j;
        i = pos / gameContr.matrixSize;
        j = pos % gameContr.matrixSize;
        
        if(gameContr.matrix[i,j] == gameContr.matrixComplete[i,j])
        {
            CheckHint();
        }
        else
        {
            currentImageList[rand].sprite = itemSprError;

            for(int k = 0; k < fishButtonsGO.Count; k++)
            {
                if(k+1 == gameContr.matrixComplete[i,j])
                {
                    fishButtonsGO[k].GetComponent<Animator>().Play("Pressed");
                }
                else
                {
                    fishButtonsGO[k].GetComponent<Animator>().Play("Normal");
                }
            }

            isHintUsed = true;
        }
    }

    public void DisableHintEffects()
    {
        if (!isHintUsed)
        {
            foreach (var mm in currentImageList)
            {
                if (mm.sprite == itemSprError)
                    mm.sprite = itemSpr;
            }
        }
        
        isHintUsed = false;
    }

    public void StopHintTimer()
    {
        CancelInvoke("InvokeTimer");
        hintButton.SetActive(true);
        hintTimer.gameObject.SetActive(false);
        hintFillImage.gameObject.SetActive(false);
    }
}
