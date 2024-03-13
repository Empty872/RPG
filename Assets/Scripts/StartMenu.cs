using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject weaponSelectionMenu;
    [SerializeField] private GameObject optionSelectionMenu;

    public void StartTheGame()
    {
        Loader.Load(Loader.Scene.BattleScene);
    }

    public void GoToOptionSelectionMenu()
    {
        CloseAll();
        optionSelectionMenu.SetActive(true);
    }

    public void GoToWeaponSelectionMenu()
    {
        CloseAll();
        weaponSelectionMenu.SetActive(true);
    }

    private void CloseAll()
    {
        optionSelectionMenu.SetActive(false);
        weaponSelectionMenu.SetActive(false);
    }
}