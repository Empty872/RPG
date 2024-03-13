using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject gameOverPanel;

    private void Start()
    {
        LevelManager.Instance.OnAllEnemiesDied += LevelManagerOnAllEnemiesDied;
        Player.Instance.OnDeath += PlayerOnDeath;
    }

    private void PlayerOnDeath(object sender, EventArgs e)
    {
        ShowGameOverPanel();
    }

    private void LevelManagerOnAllEnemiesDied(object sender, EventArgs e)
    {
        ShowVictoryPanel();
    }

    private void ShowVictoryPanel()
    {
        victoryPanel.SetActive(true);
    }

    private void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void GoToStartMenu()
    {
        Loader.Load(Loader.Scene.StartScene);
    }
}