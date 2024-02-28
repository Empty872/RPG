using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerHealthBarUI : MonoBehaviour
{
    // Start is called before the first frame update
    private Player player;
    [SerializeField] private Image healthBar;

    void Start()
    {
        player = Player.Instance;
        player.OnHealthChanged += PlayerOnHealthChanged;
    }

    private void PlayerOnHealthChanged(object sender, Player.OnHealthChangedEventArgs e)
    {
        healthBar.fillAmount = e.normalizedHeath;
    }
}