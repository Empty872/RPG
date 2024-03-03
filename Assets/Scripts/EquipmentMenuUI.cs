using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentMenuUI : MonoBehaviour
{
    public void StartTheGame()
    {
        Loader.Load(Loader.Scene.BattleScene);
    }
}