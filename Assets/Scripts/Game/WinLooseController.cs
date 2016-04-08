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

        if (isThisWinBlock)
        {
            if (currentSelectedLevel != 35)
            {
                switch (matrixSize)
                {
                    case 4:
                        UserInfo.Instance.currentLevel4++;
                        break;
                    case 5:
                        UserInfo.Instance.currentLevel5++;
                        break;
                    case 6:
                        UserInfo.Instance.currentLevel6++;
                        break;
                    case 7:
                        UserInfo.Instance.currentLevel7++;
                        break;
                    case 8:
                        UserInfo.Instance.currentLevel8++;
                        break;
                    case 9:
                        UserInfo.Instance.currentLevel9++;
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