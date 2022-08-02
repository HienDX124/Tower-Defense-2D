using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UserData
{
    private const string User_Coins = "user_coin_number";

    public static int CoinsNumber
    {
        get => PlayerPrefs.GetInt(User_Coins, 0);
        set => PlayerPrefs.SetInt(User_Coins, value);
    }
}
