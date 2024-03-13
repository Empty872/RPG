using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Wallet
{
    // Start is called before the first frame update
    public static int Money { get; private set; } = SaveManager.LoadData().money;

    public static event EventHandler OnMoneyChanged;

    public static bool TrySpendMoney(int spentMoney)
    {
        if (spentMoney > Money) return false;
        Money -= spentMoney;
        OnMoneyChanged?.Invoke(null, EventArgs.Empty);
        SaveManager.SaveData();
        return true;
    }

    public static void EarnMoney(int gotMoney)
    {
        Money += gotMoney;
        OnMoneyChanged?.Invoke(null, EventArgs.Empty);
        SaveManager.SaveData();
        Debug.Log(Money);
    }

    public static void EarnMoneyForEnemyDeath()
    {
        EarnMoney(5);
    }
}