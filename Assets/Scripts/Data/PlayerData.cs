using System;
using System.Reflection;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public static PlayerData instance = new PlayerData();

    private int MPChargeLight;
    private int MPReserveLight;
    private int MPLight => MPChargeLight + MPReserveLight;

    private int MPChargeDark;
    private int MPReserveDark;
    private int MPDark => MPReserveDark + MPChargeDark;


    private bool hasDarkFireball;

    public static void SetBool(PlayerData playerData, string name, bool value)
    {
        var field = playerData.GetType().GetField(name);
        if (field.GetType() == typeof(bool))
        {
            field.SetValue(playerData, value);
        };
    }
    public static void SetInt(PlayerData playerData, string name, int value)
    {
        var field = playerData.GetType().GetField(name);
        if (field.GetType() == typeof(int))
        {
            field.SetValue(playerData, value);
        }
    }

    public static bool GetBool(PlayerData playerData, string name)
    {
        var field = playerData.GetType().GetField(name);
        if (field.GetType() == typeof(bool))
        {
            return (bool)field.GetValue(field);
        }

        return false;
    }
    public static int GetInt(PlayerData playerData, string name)
    {
        var field = playerData.GetType().GetField(name);
        if (field.GetType() == typeof(int))
        {
            return (int)field.GetValue(field);
        }
        return -1;
    }
}