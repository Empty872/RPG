using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Wallet
{
    // Start is called before the first frame update
    public static int Money { get; private set; }
    
    public static event EventHandler OnMoneyChanged;

    public static void SpendMoney(int spentMoney)
    {
        Money -= spentMoney;
        OnMoneyChanged?.Invoke(null, EventArgs.Empty);
    }

    public static void EarnMoney(int gotMoney)
    {
        Money -= gotMoney;
        OnMoneyChanged?.Invoke(null, EventArgs.Empty);
    }

    public static void EarnMoneyForEnemyDeath()
    {
        EarnMoney(5);
    }
}