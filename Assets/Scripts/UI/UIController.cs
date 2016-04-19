using UnityEngine;
using UnityEngine.SceneManagement;

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

    private int matrixSize;
    public void SomeLevelButton(int _level)
    {
        if (UserInfo.Instance.canUseLife)
        {
            PlayerPrefs.SetInt("SelectedLevel", _level);
            matrixSize = PlayerPrefs.GetInt("matrixSize");

            LoadingController.nextSceneName = "PlayLevel" + matrixSize + "x" + matrixSize;
            StartCoroutine(GlobalVariables.LoadingSomeScene("Loading", false));
            //SceneManager.LoadScene("PlayLevel" + matrixSize + "x" + matrixSize, LoadSceneMode.Single);
        }
        else
        {
            Debug.Log("No Lifes!!!");
        }
    }

    public void BackToChooseLevelDifficultButton()
    {
        LoadingController.nextSceneName = "ChooseLevelDifficult";
        StartCoroutine(GlobalVariables.LoadingSomeScene("Loading", false));
        //SceneManager.LoadScene("ChooseLevelDifficult", LoadSceneMode.Single);
    }
}
