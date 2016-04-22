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
    public RectTransform scrollMap;
    
    private float currentLevelPosX = 0;
    private float Center = 960.0f;
    private float buttonW = 0;
    private float shiftScrollMap = 0;
    private float maxShiftScrollMaxp = -3838.322f;

    public RectTransform fishWrapper;
    public float offsetXFishWrapper;
    public float offsetYFishWrapper;

    void Start()
    {
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

        if(currentLevel >= 10 && currentLevel <= 32)
        {
            currentLevelPosX = levelButtons[currentLevel - 1].GetComponent<RectTransform>().anchoredPosition.x;
            buttonW = levelButtons[currentLevel - 1].GetComponent<RectTransform>().sizeDelta.x;
            shiftScrollMap = currentLevelPosX - Center + (buttonW / 2);

            scrollMap.anchoredPosition = new Vector2(-1 * shiftScrollMap, 0);
        }
        else if(currentLevel > 32)
        {
            scrollMap.anchoredPosition = new Vector2(-3838.322f, 0);
        }

        //Position of Fish Wrapper
        RectTransform tempButtonPos = levelButtons[currentLevel-1].GetComponent<RectTransform>();
        Debug.Log(tempButtonPos.anchoredPosition);
        fishWrapper.anchoredPosition = new Vector2(Mathf.Floor(tempButtonPos.anchoredPosition.x + offsetXFishWrapper),
                tempButtonPos.anchoredPosition.y + offsetYFishWrapper);
    }

    void SetInteractableToButton(Button _someBtn)
    {
            _someBtn.interactable = true;
            _someBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            _someBtn.GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
    }

    public void UpdateResources()
    {
        pealrCountText.text = UserInfo.Instance.pearlCount.ToString();
        livesController.RefreshLifes();
    }
}
