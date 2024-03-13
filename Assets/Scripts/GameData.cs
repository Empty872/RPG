using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public int money;
    public List<WeaponSO> boughtWeapons;
    public static List<WeaponSO> staticBoughtWeapons = new GameData().boughtWeapons;


    public GameData()
    {
        money = Wallet.Money;
        if (staticBoughtWeapons is null) boughtWeapons = new List<WeaponSO>();
        else
        {
            boughtWeapons = staticBoughtWeapons;
        }
    }
    //
    // public void UpdateData()
    // {
    //     
    // }
}