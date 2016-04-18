using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinLooseController : MonoBehaviour
{
    private int currentSelectedLevel;
    private int matrixSize;

    public bool isThisWinBlock = false;
    public LivesController livesContr;
    void Start()
    {
        matrixSize = PlayerPrefs.GetInt("matrixSize");
        currentSelectedLevel = PlayerPrefs.GetInt("SelectedLevel");
        int value = 0;

        if (isThisWinBlock)
        {
            if (currentSelectedLevel != 35)
            {
                switch (matrixSize)
                {
                    case 4:
                        value = UserInfo.Instance.currentLevel4;
                        CheckOpenNewLevel(ref value);
                        UserInfo.Instance.currentLevel4 = value;
                        break;
                    case 5:
                        value = UserInfo.Instance.currentLevel5;
                        CheckOpenNewLevel(ref value);
                        UserInfo.Instance.currentLevel5 = value;
                        break;
                    case 6:
                        value = UserInfo.Instance.currentLevel6;
                        CheckOpenNewLevel(ref value);
                        UserInfo.Instance.currentLevel6 = value;
                        break;
                    case 7:
                        value = UserInfo.Instance.currentLevel7;
                        CheckOpenNewLevel(ref value);
                        UserInfo.Instance.currentLevel7 = value;
                        break;
                    case 8:
                        value = UserInfo.Instance.currentLevel8;
                        CheckOpenNewLevel(ref value);
                        UserInfo.Instance.currentLevel8 = value;
                        break;
                    case 9:
                        value = UserInfo.Instance.currentLevel9;
                        CheckOpenNewLevel(ref value);
                        UserInfo.Instance.currentLevel9 = value;
                        break;
                }

                UserInfo.Instance.SaveUserInfo();
            }
        }
        else
        {
            livesContr.SubtractOneLife();
        }
    }

    private void CheckOpenNewLevel(ref int _xmlCurrentLevel)
    {
        if(currentSelectedLevel == _xmlCurrentLevel)
        {
            _xmlCurrentLevel++;
        }
    }

    //Win Block START
    public void NextLevelButton()
    {
        if (currentSelectedLevel != 35)
        {
            currentSelectedLevel++;
            PlayerPrefs.SetInt("SelectedLevel", currentSelectedLevel);
            SceneManager.LoadScene("PlayLevel"+matrixSize+"x"+matrixSize);
        }
        else
        {
            SceneManager.LoadScene("ChooseLevelDifficult");
        }
    }

    public void CloseButtonWin()
    {
        SceneManager.LoadScene("Map");
    }
    //Win Block END

    //Loose Block START
    public void RestartLevelButton()
    {
        if(UserInfo.Instance.canUseLife)
        {
            SceneManager.LoadScene("PlayLevel" + matrixSize + "x" + matrixSize);
        }
    }
    
    public void CloseButtonLoose()
    {
        SceneManager.LoadScene("Map");
    }
    //Loose Block END
}