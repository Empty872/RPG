using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int enemiesCount;
    public event EventHandler OnAllEnemiesDied;
    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void DecreaseEnemiesCount()
    {
        enemiesCount -= 1;
        if (enemiesCount == 0)
        {
            OnAllEnemiesDied?.Invoke(this, EventArgs.Empty);
        }
    }
}