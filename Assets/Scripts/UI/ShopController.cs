using UnityEngine;
using System.Collections;

public class ShopController : MonoBehaviour
{
    public GameObject[] tubsImages;
    public GameObject[] bundles;
    public bool thisIsMapScene = false;

    public void ShowSelectedElement(int _typeOfShopElement)
    {
        for(int i=0;i<3;i++)
        {
            if(i == _typeOfShopElement)
            {
                tubsImages[i].SetActive(true);
                bundles[i].SetActive(true);
            }
            else
            {
                tubsImages[i].SetActive(false);
                bundles[i].SetActive(false);
            }
        }
    }

    public void CloseShopButton()
    {
        gameObject.SetActive(false);
    }

    //Buy Pearl START
    public void BuyPearls(int _count)
    {
        //Testing add pearls
        UserInfo.Instance.pearlCount += _count;
        UserInfo.Instance.SaveUserInfo();

        if(thisIsMapScene)
        {
            GameObject.FindObjectOfType<MapController>().UpdateResources();
        }
        else
        {
            //GameObject.FindObjectOfType<MapController>().UpdateResources(GlobalVariables.TypeOfShopElement.Pearls);
        }

        Debug.Log("Buy " + _count + " pearls");
    }
    //Buy Pearl END

    //Buy Lives START
    public void BuyLives(int _count)
    {
        if (UserInfo.Instance.lifeCount + _count <= 5)
        {
            GlobalVariables.BuyLifes buyLifes = new GlobalVariables.BuyLifes(_count);
            if (UserInfo.Instance.pearlCount >= buyLifes.price)
            {
                UserInfo.Instance.pearlCount -= buyLifes.price;
                UserInfo.Instance.lifeCount += _count;
                UserInfo.Instance.SaveUserInfo();

                if (thisIsMapScene)
                {
                    GameObject.FindObjectOfType<MapController>().UpdateResources();
                }
                else
                {
                    //GameObject.FindObjectOfType<MapController>().UpdateResources(GlobalVariables.TypeOfShopElement.);
                }

                Debug.Log("Buy " + _count + " lifes");
            }
            else
                Debug.Log("Not Enought Money");
        }
        else
        {
            Debug.Log("You have full life");
        }
    }
    //Buy Lives END

    //Buy Hints START
    public void BuyHints(int _count)
    {
        GlobalVariables.BuyHints buyHints = new GlobalVariables.BuyHints(_count);
        if (UserInfo.Instance.pearlCount >= buyHints.price)
        {
            UserInfo.Instance.pearlCount -= buyHints.price;
            UserInfo.Instance.hintCount += _count;
            UserInfo.Instance.SaveUserInfo();

            if (!thisIsMapScene)
            {
                //GameObject.FindObjectOfType<MapController>().UpdateResources(GlobalVariables.TypeOfShopElement.);
            }

            Debug.Log("Buy " + _count + " hints");
        }
        else
            Debug.Log("Not Enought Money");
       
    }
    //Buy Hints END
}
