using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuUIController : MonoBehaviour
{
    private int matrixSize;
    public void TypeOfLevelButton(int _matrixSize)
    {
        matrixSize = _matrixSize;
        //Show Map
    }

    public void SomeLevelButton(int _level)
    {
        PlayerPrefs.SetInt("SelectedLevel", _level);
        SceneManager.LoadScene("PlayLevel"+matrixSize+"x"+ matrixSize);
    }

    public void BuyFullVersionButton()
    {
        Application.OpenURL("");
    }
}
