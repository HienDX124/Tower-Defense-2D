using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UserData
{
    private const string User_Coins = "user_coin_number";
    private const string User_Level = "user_level_number";

    public static int CoinsNumber
    {
        get => PlayerPrefs.GetInt(User_Coins, 0);
        set => PlayerPrefs.SetInt(User_Coins, value);
    }

    public static int LevelNumber
    {
        get => PlayerPrefs.GetInt(User_Level, 0);
        set => PlayerPrefs.SetInt(User_Level, value);
    }
}
