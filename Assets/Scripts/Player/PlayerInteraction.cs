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

    void Start()
    {
        gameInput.OnInteractButtonPressed += GameInputOnInteractButtonPressed;
        playerBattle = GetComponent<PlayerBattle>();
        playerWeaponVisual = GetComponent<PlayerWeaponVisual>();
    }

    private void GameInputOnInteractButtonPressed(object sender, EventArgs e)
    {
        if (possibleWeapon == null) return;
        PickUpWeapon(possibleWeapon);
        PlayerWeaponManager.Instance.EquipWeapon(possibleWeapon.GetWeaponSO());
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

    private void PickUpWeapon(Weapon weapon)
    {
        var weaponSO = weapon.GetWeaponSO();
        switch (weaponSO.WeaponArmType)
        {
            case WeaponArmType.LeftHanded:
                DropWeapon(playerBattle.GetWeaponL(), weapon.transform);
                if (playerBattle.GetWeaponR().WeaponType == WeaponType.BigSword)
                {
                    DropWeapon(playerBattle.GetWeaponR(), weapon.transform);
                }

                // weapon.transform.SetParent(bagWeaponL);
                break;
            case WeaponArmType.RightHanded:
                DropWeapon(playerBattle.GetWeaponR(), weapon.transform);
                break;
            case WeaponArmType.BothHanded:
                DropWeapon(playerBattle.GetWeaponL(), weapon.transform);
                DropWeapon(playerBattle.GetWeaponR(), weapon.transform);
                break;
        }

        Destroy(weapon.gameObject);
    }

    private void DropWeapon(WeaponSO equippedWeaponSO, Transform weaponTransform)
    {
        if (equippedWeaponSO.WeaponType == WeaponType.Fist)
        {
            return;
        }

        Instantiate(equippedWeaponSO.WeaponPrefab, weaponTransform.position, weaponTransform.rotation);
    }
}