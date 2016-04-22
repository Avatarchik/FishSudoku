using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

        //Bubbles Animation
        StartCoroutine(AnimBubbles());
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

            LoadingController.nextSceneName = "PlayLevel" + matrixSize + "x" + matrixSize;
            StartCoroutine(GlobalVariables.LoadingSomeScene("Loading", false));
            //SceneManager.LoadScene("PlayLevel"+matrixSize+"x"+matrixSize);
        }
        else
        {
            LoadingController.nextSceneName = "ChooseLevelDifficult";
            StartCoroutine(GlobalVariables.LoadingSomeScene("Loading", false));
            //SceneManager.LoadScene("ChooseLevelDifficult");
        }
    }

    public void CloseButtonWin()
    {
        LoadingController.nextSceneName = "Map";
        StartCoroutine(GlobalVariables.LoadingSomeScene("Loading", false));
        //SceneManager.LoadScene("Map");
    }
    //Win Block END

    //Loose Block START
    public void RestartLevelButton()
    {
        if(UserInfo.Instance.canUseLife)
        {
            LoadingController.nextSceneName = "PlayLevel" + matrixSize + "x" + matrixSize;
            StartCoroutine(GlobalVariables.LoadingSomeScene("Loading", false));
            //SceneManager.LoadScene("PlayLevel" + matrixSize + "x" + matrixSize);
        }
    }
    
    public void CloseButtonLoose()
    {
        LoadingController.nextSceneName = "Map";
        StartCoroutine(GlobalVariables.LoadingSomeScene("Loading", false));
        //SceneManager.LoadScene("Map");
    }
    //Loose Block END

    //Bubbles Pop START
    public List<Animation> bublesWithAnimation = new List<Animation>();
    public int elementCount = 15;
    IEnumerator AnimBubbles()
    {
        for (int i = bublesWithAnimation.Count - 1; i >= 0; i--)
        {
            int rand = Random.Range(0, bublesWithAnimation.Count);
            int j = rand;
            var temp = bublesWithAnimation[i];
            bublesWithAnimation[i] = bublesWithAnimation[j];
            bublesWithAnimation[j] = temp;
        }

        if (elementCount > bublesWithAnimation.Count)
            elementCount = bublesWithAnimation.Count;

        for (int i = 0; i < elementCount; i++)
        {
            bublesWithAnimation[i].Play("BubblePop");
            yield return new WaitForSeconds(bublesWithAnimation[i]["BubblePop"].length);
            //bublesWithAnimation.Remove(bublesWithAnimation[i]);
            //Destroy(bublesWithAnimation[i].gameObject);
        }
    }

    //Bubbles Pop END
}