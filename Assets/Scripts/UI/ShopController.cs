using UnityEngine;
using System.Collections;

public class ShopController : MonoBehaviour
{
    public GameObject[] tubsImages;
    public GameObject[] bundles;

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

    //Buy Lives START
    public void BuyLives(int _count)
    {

    }
    //Buy Lives END

    //Buy Hints START
    public void BuyHints(int _count)
    {

    }
    //Buy Hints END
}
