using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        Wallet.OnMoneyChanged += WalletOnMoneyChanged;
    }

    private void WalletOnMoneyChanged(object sender, EventArgs e)
    {
        ChangeMoneyCountView();
    }

    // Update is called once per frame
    private void ChangeMoneyCountView()
    {
        textMeshProUGUI.text = "Money " + Wallet.Money;
    }
}