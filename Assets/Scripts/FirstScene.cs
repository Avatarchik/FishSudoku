using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour
{
    public GameObject difficultBlock;
    public GameObject loadBlock;
    public GameObject playButton;
    public GameObject logo;

    public void PlayButton()
    {
        logo.SetActive(false);
        playButton.SetActive(false);

        difficultBlock.SetActive(true);
    }

    public void CloseDifficultBlockButton()
    {
        difficultBlock.SetActive(false);

        logo.SetActive(true);
        playButton.SetActive(true);
    }

    public void TypeOfLevelButton(int _matrixSize)
    {
        PlayerPrefs.SetInt("matrixSize", _matrixSize);
        SceneManager.LoadScene("Map", LoadSceneMode.Single);
    }

    public void BuyFullVersionButton()
    {
        Debug.Log("Show URL");
        //Application.OpenURL("");
    }
}
