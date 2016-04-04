using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LivesController : MonoBehaviour
{
    public List<Image> allLifes;
    public Sprite heartYes;
    public Sprite heartNo;
    public GameObject noLifesImage;
    public GameObject lifeRechargeFill;
    public GameObject lifesRechargeText;

    private int currentLifes;
    void Start()
    {
        currentLifes = UserInfo.Instance.lifeCount;
        if(currentLifes == 5)
        {
            noLifesImage.SetActive(false);
            lifeRechargeFill.SetActive(false);
            lifesRechargeText.SetActive(false);

            foreach(var mm in allLifes)
            {
                mm.sprite = heartYes;
            }
        }
        else
        {
            noLifesImage.SetActive(true);
            lifeRechargeFill.SetActive(true);
            lifesRechargeText.SetActive(true);

            for(int i = 0; i < currentLifes; i++)
            {
                AddOneLife(i);
            }
        }

        Debug.Log("Lives Is Loaded");
    }

    public void RefreshLives()
    {
        currentLifes = UserInfo.Instance.lifeCount;
        if (currentLifes == 5)
        {
            noLifesImage.SetActive(false);
            lifeRechargeFill.SetActive(false);
            lifesRechargeText.SetActive(false);

            foreach (var mm in allLifes)
            {
                mm.sprite = heartYes;
            }
        }
        else
        {
            noLifesImage.SetActive(true);
            lifeRechargeFill.SetActive(true);
            lifesRechargeText.SetActive(true);

            for (int i = 0; i < currentLifes; i++)
            {
                AddOneLife(i);
            }
        }
    }

    public void AddOneLife(int _lifePos)
    {
        allLifes[_lifePos].sprite = heartYes;
    }

    public void DeleteOneLife()
    {

    }
}
