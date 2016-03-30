using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject shopBlock;
    private GlobalVariables.TypeOfShopElement typeOfShopElement;

    public void OpenShopButton(int _typeOfShopElement)
    {
        typeOfShopElement = (GlobalVariables.TypeOfShopElement)_typeOfShopElement;
        shopBlock.SetActive(true);
        shopBlock.GetComponent<ShopController>().ShowSelectedElement((int)typeOfShopElement);
    }
}
