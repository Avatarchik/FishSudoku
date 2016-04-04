using UnityEngine;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour
{
    public Text pearlCount;
    public LivesController lifes;
    public Text hintCount;

    void Start()
    {
        pearlCount.text = UserInfo.Instance.pearlCount.ToString();
        hintCount.text = UserInfo.Instance.hintCount.ToString();
    }

    public GameObject shopBlock;
    private GlobalVariables.TypeOfShopElement typeOfShopElement;

    public void OpenShopButton(int _typeOfShopElement)
    {
        typeOfShopElement = (GlobalVariables.TypeOfShopElement)_typeOfShopElement;
        shopBlock.SetActive(true);
        shopBlock.GetComponent<ShopController>().ShowSelectedElement((int)typeOfShopElement);
    }
}
