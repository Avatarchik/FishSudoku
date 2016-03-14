using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public HintTimer currentTimer;

    public GameObject rightMenuBackground;
    public GameObject rightMenuPanel;

    private bool isRightMenuActive = true;
    private Animation rightMenuPanelAnim;

    public void RightMenu_Button()
    {
        rightMenuPanelAnim = rightMenuPanel.GetComponent<Animation>();
        StartCoroutine(RightMenuAnimation());
    }

    IEnumerator RightMenuAnimation()
    {
        if (isRightMenuActive)
        {
            currentTimer.Pause();

            rightMenuBackground.SetActive(true);
            rightMenuPanelAnim["RightMenuPanel_LevelUI"].speed = 1;
            rightMenuPanelAnim["RightMenuPanel_LevelUI"].time = rightMenuPanelAnim["RightMenuPanel_LevelUI"].length;
            rightMenuPanelAnim.Play("RightMenuPanel_LevelUI");
            yield return new WaitForSeconds(rightMenuPanelAnim.clip.length);
            isRightMenuActive = false;
        }
        else
        {
            rightMenuPanelAnim["RightMenuPanel_LevelUI"].speed = -1;
            rightMenuPanelAnim["RightMenuPanel_LevelUI"].time = rightMenuPanelAnim["RightMenuPanel_LevelUI"].length;
            rightMenuPanelAnim.Play("RightMenuPanel_LevelUI");
            rightMenuBackground.SetActive(false);
            isRightMenuActive = true;

            currentTimer.UnPause();
        }
    }
}
