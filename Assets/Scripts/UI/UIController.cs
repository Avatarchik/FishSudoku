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
        Debug.Log("Level " + _level + " loading...");
        PlayerPrefs.SetInt("SelectedLevel", _level);
        matrixSize = PlayerPrefs.GetInt("matrixSize");
        SceneManager.LoadScene("PlayLevel" + matrixSize + "x" + matrixSize, LoadSceneMode.Single);
    }

    public void BackToChooseLevelDifficultButton()
    {
        SceneManager.LoadScene("ChooseLevelDifficult", LoadSceneMode.Single);
    }
}
