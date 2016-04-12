using UnityEngine;

public class ShopController : MonoBehaviour
{
    public GameObject[] tubsImages;
    public GameObject[] bundles;
    public bool thisIsMapScene = false;
    public LivesController livesContr;
    public GameObject notEnoughtMoneyPopUp;

    void Start()
    {
        if (livesContr == null)
            livesContr = GameObject.FindObjectOfType<LivesController>();
    }

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
            GameObject.FindObjectOfType<MenuUIController>().UpdateResources();
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

                if (_count != 0) //Unlimited Lifes
                    livesContr.ReWriteTime(_count);

                if (thisIsMapScene)
                {
                    GameObject.FindObjectOfType<MapController>().UpdateResources();
                }
                else
                {
                    GameObject.FindObjectOfType<MenuUIController>().UpdateResources();
                }

                Debug.Log("Buy " + _count + " lifes");
            }
            else
                notEnoughtMoneyPopUp.SetActive(true);
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

            if (thisIsMapScene)
            {
                GameObject.FindObjectOfType<MapController>().UpdateResources();
            }
            else
            {
                GameObject.FindObjectOfType<MenuUIController>().UpdateResources();
                GameObject.FindObjectOfType<HintController>().StopHintTimer();
            }

            Debug.Log("Buy " + _count + " hints");
        }
        else
            notEnoughtMoneyPopUp.SetActive(true);

    }
    //Buy Hints END

    //Not Enought Money Pop Up START
    public void NemBuyButton()
    {
        ShowSelectedElement(0);
        notEnoughtMoneyPopUp.SetActive(false);
    }

    public void NemCloseButton()
    {
        notEnoughtMoneyPopUp.SetActive(false);
    }
    //Not Enought Money Pop Up END
}
