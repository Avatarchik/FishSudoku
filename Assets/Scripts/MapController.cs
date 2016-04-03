using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    public List<Button> levelButtons;
    private int currentLevel;
    private int matrixSize;
    public Text pealrCountText;
    public LivesController livesController;

    void Start()
    {
        UserInfo.Instance.LoadUserInfo();

        pealrCountText.text = UserInfo.Instance.pearlCount.ToString();
        livesController.enabled = true;

        matrixSize = PlayerPrefs.GetInt("matrixSize");
        switch(matrixSize)
        {
            case 4:
                currentLevel = UserInfo.Instance.currentLevel4;
                break;
            case 5:
                currentLevel = UserInfo.Instance.currentLevel5;
                break;
            case 6:
                currentLevel = UserInfo.Instance.currentLevel6;
                break;
            case 7:
                currentLevel = UserInfo.Instance.currentLevel7;
                break;
            case 8:
                currentLevel = UserInfo.Instance.currentLevel8;
                break;
            case 9:
                currentLevel = UserInfo.Instance.currentLevel9;
                break;
        }

        for(int i = 0; i < currentLevel; i++)
        {
            SetInteractableToButton(levelButtons[i]);
        }
    }

    void SetInteractableToButton(Button _someBtn)
    {
            _someBtn.interactable = true;
            _someBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            _someBtn.GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
    }
}
