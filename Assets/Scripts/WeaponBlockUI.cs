using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WeaponBlockUI : MonoBehaviour
{
    private enum SelectionState
    {
        Selected,
        Unselected
    }

    // Start is called before the first frame update
    private readonly Vector3 rotation = Vector3.up * 15;
    [SerializeField] private WeaponSO weaponSO;
    [SerializeField] private Transform weaponUI;
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private GameObject selectButton;
    [SerializeField] private GameObject buyButton;
    private SelectionState state;
    [SerializeField] private bool bought;
    private PlayerWeaponManager playerWeaponManager;


    private void Start()
    {
        if (new GameData().boughtWeapons.Contains(weaponSO)) bought = true;
        if (bought)
        {
            selectButton.SetActive(true);
            buyButton.SetActive(false);
        }
        else
        {
            selectButton.SetActive(false);
            buyButton.SetActive(true);
        }

        playerWeaponManager = PlayerWeaponManager.Instance;
        // var weaponUIPrefab = weaponSO.WeaponUIPrefab;
        // weaponVisual.gameObject.layer = LayerMask.NameToLayer("UI");
        Instantiate(weaponSO.WeaponUIPrefab, weaponUI);
        weaponName.text = weaponSO.Name;
        buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Buy " + weaponSO.Cost;
        Unselect();
        playerWeaponManager.OnEquipWeapon += InstanceOnEquipWeapon;
    }

    private void Update()
    {
        weaponUI.Rotate(rotation * Time.deltaTime);
    }

    private void InstanceOnEquipWeapon(object sender, PlayerWeaponManager.OnEquipWeaponEventArgs e)
    {
        if (e.weaponSO == weaponSO) return;
        if (e.weaponSO.WeaponArmType == WeaponArmType.BothHanded ||
            weaponSO.WeaponArmType == e.weaponSO.WeaponArmType || weaponSO.WeaponArmType == WeaponArmType.BothHanded)
        {
            UnSelectVisual();
        }
    }

    public void Select()
    {
        SelectVisual();
        playerWeaponManager.EquipWeapon(weaponSO);
    }

    public void Unselect()
    {
        UnSelectVisual();
        playerWeaponManager.UnEquipWeapon(weaponSO.WeaponArmType);
    }

    public void SelectVisual()
    {
        state = SelectionState.Selected;
        selectButton.GetComponent<Image>().color = Color.green;
    }

    public void UnSelectVisual()
    {
        state = SelectionState.Unselected;
        selectButton.GetComponent<Image>().color = Color.white;
    }

    public void ChangeSelectionState()
    {
        switch (state)
        {
            case SelectionState.Selected:
                Unselect();
                break;
            case SelectionState.Unselected:
                Select();
                break;
        }
    }

    public void Buy()
    {
        if (!Wallet.TrySpendMoney(weaponSO.Cost)) return;
        selectButton.SetActive(true);
        buyButton.SetActive(false);
        bought = true;
        GameData.staticBoughtWeapons.Add(weaponSO);
        SaveManager.SaveData();
    }
}