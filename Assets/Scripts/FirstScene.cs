using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour
{
    public GameObject difficultBlock;
    public GameObject loadBlock;
    public GameObject playButton;
    public GameObject logo;
    public GameObject howToPlay;

    public void PlayButton()
    {
        logo.GetComponent<Animator>().Play("LogotypeHide");
        //logo.SetActive(false);

        playButton.GetComponent<Animator>().Play("PlayHide");
        //playButton.SetActive(false);

        howToPlay.GetComponent<Animator>().Play("HowToPlayHide");
        //howToPlay.SetActive(false);

        difficultBlock.SetActive(true);
        difficultBlock.GetComponent<Animation>().Play("DifficultBlockUnhide");
       
    }

    public void CloseDifficultBlockButton()
    {
        difficultBlock.GetComponent<Animation>().Play("DifficultBlockHide");
        //difficultBlock.SetActive(false);

        logo.GetComponent<Animator>().Play("LogotypeUnhide");
        //logo.SetActive(true);

        playButton.GetComponent<Animator>().Play("PlayUnhide");
        //playButton.SetActive(true);

        howToPlay.GetComponent<Animator>().Play("HowToPlayUnhide");
        //howToPlay.SetActive(true);
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
