using UnityEngine;
using System.Collections;

public class UserInfo : MonoBehaviour
{
    private UserInfo() { }
    private static UserInfo _instance;
    public static UserInfo Instance
    {
        get
        {
            if (_instance == null)
                _instance = new UserInfo();

            return _instance;
        }
    }

    //User Info Singelton
    public int pearlCount { get; set; }
    private int lifeCountPrivate;
    public int lifeCount
    {   get
        {
            return lifeCountPrivate;
        }
        set
        {
            if (lifeCountPrivate >= 5 && lifeCountPrivate <= 0)
                Debug.Log("Lives is Full");
            else
                lifeCountPrivate = value;
        }
    }
    public int hintCount { get; set; }
    
    private UserParser userParser;
    public void LoadUserInfo()
    {
        userParser = UserParser.Load(Application.streamingAssetsPath + "/Userinfo.xml");
        pearlCount = userParser.pearlCount;
        lifeCount = userParser.lifeCount;
        hintCount = userParser.hintCount;
    }

    public void SaveUserInfo()
    {
        userParser.pearlCount = pearlCount;
        userParser.lifeCount = lifeCount;
        userParser.hintCount = hintCount;
        userParser.Save(Application.streamingAssetsPath + "/Userinfo.xml");
    }
}
