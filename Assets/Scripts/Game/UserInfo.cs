﻿using System;
using UnityEngine;

public class UserInfo
{
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
    private UserInfo() { }

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

    public DateTime timeToStartLifesTimer;

    public int hintCount { get; set; }
    public int currentLevel4 { get; set; }
    public int currentLevel5 { get; set; }
    public int currentLevel6 { get; set; }
    public int currentLevel7 { get; set; }
    public int currentLevel8 { get; set; }
    public int currentLevel9 { get; set; }

    private UserParser userParser;
    public void LoadUserInfo()
    {
        userParser = UserParser.Load(Application.streamingAssetsPath + "/Userinfo.xml");
        pearlCount = userParser.pearlCount;
        lifeCount = userParser.lifeCount;
        hintCount = userParser.hintCount;

        currentLevel4 = userParser.currentLevel4;
        currentLevel5 = userParser.currentLevel5;
        currentLevel6 = userParser.currentLevel6;
        currentLevel7 = userParser.currentLevel7;
        currentLevel8 = userParser.currentLevel8;
        currentLevel9 = userParser.currentLevel9;

        Debug.Log("userInfo Loaded Sucessful");
    }

    public void SaveUserInfo()
    {
        userParser.pearlCount = pearlCount;
        userParser.lifeCount = lifeCount;
        userParser.hintCount = hintCount;

        userParser.currentLevel4 = currentLevel4;
        userParser.currentLevel5 = currentLevel5;
        userParser.currentLevel6 = currentLevel6;
        userParser.currentLevel7 = currentLevel7;
        userParser.currentLevel8 = currentLevel8;
        userParser.currentLevel9 = currentLevel9;

        userParser.Save(Application.streamingAssetsPath + "/Userinfo.xml");

        Debug.Log("userInfo Save Sucessful");
    }
}
