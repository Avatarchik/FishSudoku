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
    public int maxLifeCount { get; private set; }
    private int lifeCountPrivate;
    public int lifeCount
    {   get
        {
            return lifeCountPrivate;
        }
        set
        {
            if (value <= maxLifeCount && value > 0)
            {
                lifeCountPrivate = value;
            }
            else
                lifeCountPrivate = maxLifeCount;
        }
    }
    public int hintCount { get; set; }
    
    private UserParser userParser;
    public void LoadUserInfo()
    {
        userParser = UserParser.Load(Application.streamingAssetsPath + "/Userinfo.xml");
        pearlCount = userParser.pearlCount;
        lifeCount = userParser.lifeCount;
        maxLifeCount = userParser.maxLifeCount;
        hintCount = userParser.hintCount;
    }

    public void SaveUserInfo()
    {
        userParser.pearlCount = pearlCount;
        userParser.lifeCount = lifeCount;
        userParser.maxLifeCount = maxLifeCount;
        userParser.hintCount = hintCount;
        userParser.Save(Application.streamingAssetsPath + "/Userinfo.xml");
    }
}
