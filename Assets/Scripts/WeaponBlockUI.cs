using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBlockUI : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 rotation = Vector3.up * 15;
    [SerializeField] private WeaponSO weaponSO;
    [SerializeField] private Transform weaponUI;
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private Transform button;
    private WeaponBlockState state;
    private PlayerWeaponManager playerWeaponManager;


    private void Start()
    {
        playerWeaponManager = PlayerWeaponManager.Instance;
        // var weaponUIPrefab = weaponSO.WeaponUIPrefab;
        // weaponVisual.gameObject.layer = LayerMask.NameToLayer("UI");
        Instantiate(weaponSO.WeaponUIPrefab, weaponUI);
        weaponName.text = weaponSO.Name;
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
        state = WeaponBlockState.Selected;
        button.GetComponent<Image>().color = Color.green;
    }

    public void UnSelectVisual()
    {
        state = WeaponBlockState.Unselected;
        button.GetComponent<Image>().color = Color.white;
    }

    public void ChangeSelectionState()
    {
        switch (state)
        {
            case WeaponBlockState.Selected:
                Unselect();
                break;
            case WeaponBlockState.Unselected:
                Select();
                break;
        }
    }
}