using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Enemy enemy;
    [SerializeField] private Image healthBar;

    void Start()
    {
        enemy.OnHealthChanged += EnemyOnHealthChanged;
        Hide();
    }

    private void EnemyOnHealthChanged(object sender, Enemy.OnHealthChangedEventArgs e)
    {
        if (e.normalizedHeath > 0) Show();
        else Hide();
        healthBar.fillAmount = e.normalizedHeath;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}