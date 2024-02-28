using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerBattle playerBattle;
    private PlayerWeaponVisual playerWeaponVisual;
    private Weapon possibleWeapon;

    [SerializeField] private GameInput gameInput;
    // [SerializeField] private Transform bagWeaponL;
    // [SerializeField] private Transform bagWeaponR;
    //
    // // ReSharper disable once InconsistentNaming
    // [SerializeField] private Transform bagWeaponLR;

    void Start()
    {
        gameInput.OnInteractButtonPressed += GameInputOnInteractButtonPressed;
        playerBattle = GetComponent<PlayerBattle>();
        playerWeaponVisual = GetComponent<PlayerWeaponVisual>();
    }

    private void GameInputOnInteractButtonPressed(object sender, EventArgs e)
    {
        if (possibleWeapon == null) return;
        EquipWeapon(possibleWeapon);
        playerBattle.EquipWeapon(possibleWeapon.GetWeaponSO());
        playerWeaponVisual.ShowWeapon(possibleWeapon.GetWeaponSO());
    }

    // Update is called once per frame
    void Update()
    {
        // Ray ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 1));
        // if (Physics.Raycast(ray, out var raycastHit, Mathf.Infinity))
        // {
        //     var target = raycastHit.transform.gameObject;
        //     if (Input.GetKeyDown(KeyCode.F))
        //     {
        //         if (target.CompareTag("Weapon"))
        //         {
        //             PickUpWeapon(target);
        //         }
        //     }
        // }
    }

    private void OnCollisionStay(Collision other)
    {
        possibleWeapon = other.gameObject.TryGetComponent(out Weapon newWeapon) ? newWeapon : null;
    }

    private void EquipWeapon(Weapon weapon)
    {
        var weaponSO = weapon.GetWeaponSO();
        switch (weaponSO.weaponType)
        {
            case WeaponSO.WeaponType.Shield:
                DropWeapon(playerBattle.GetWeaponL(), weapon.transform);
                if (playerBattle.GetWeaponR().weaponType == WeaponSO.WeaponType.BigSword)
                {
                    DropWeapon(playerBattle.GetWeaponR(), weapon.transform);
                }

                // weapon.transform.SetParent(bagWeaponL);
                break;
            case WeaponSO.WeaponType.Sword:
                DropWeapon(playerBattle.GetWeaponR(), weapon.transform);
                // DropWeapon(bagWeaponLR, weapon.transform);
                // weapon.transform.SetParent(bagWeaponR);
                break;
            case WeaponSO.WeaponType.BigSword:
                DropWeapon(playerBattle.GetWeaponL(), weapon.transform);
                DropWeapon(playerBattle.GetWeaponR(), weapon.transform);
                // DropWeapon(bagWeaponL, weapon.transform);
                // DropWeapon(bagWeaponR, weapon.transform);
                // DropWeapon(bagWeaponLR, weapon.transform);
                // weapon.transform.SetParent(bagWeaponLR);
                break;
            case WeaponSO.WeaponType.Fist:
                break;
        }

        Destroy(weapon.gameObject);
    }

    private void DropWeapon(WeaponSO equippedWeaponSO, Transform weaponTransform)
    {
        if (equippedWeaponSO.weaponType == WeaponSO.WeaponType.Fist)
        {
            return;
        }

        Instantiate(equippedWeaponSO.prefab, weaponTransform.position, weaponTransform.rotation);
        // if (equippedWeaponPrefab.childCount != 0)
        // {
        //     equippedWeaponPrefab.GetChild(0).transform.position = weaponTransform.position;
        //     equippedWeaponPrefab.GetChild(0).SetParent(null);
        // }
    }
}