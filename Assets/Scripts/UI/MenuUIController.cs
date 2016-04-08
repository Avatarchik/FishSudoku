using UnityEngine;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour
{
    public Text pearlCountText;
    public LivesController livesController;
    public Text hintCountText;

    void Start()
    {
        pearlCountText.text = UserInfo.Instance.pearlCount.ToString();
        //livesController.RefreshLives();
        hintCountText.text = UserInfo.Instance.hintCount.ToString();
    }

    public GameObject shopBlock;
    private GlobalVariables.TypeOfShopElement typeOfShopElement;

    public void OpenShopButton(int _typeOfShopElement)
    {
        typeOfShopElement = (GlobalVariables.TypeOfShopElement)_typeOfShopElement;
        shopBlock.SetActive(true);
        shopBlock.GetComponent<ShopController>().ShowSelectedElement((int)typeOfShopElement);
    }

    public void UpdateResources()
    {
        pearlCountText.text = UserInfo.Instance.pearlCount.ToString();
        livesController.RefreshLifes();
        hintCountText.text = UserInfo.Instance.hintCount.ToString();
    }
}
