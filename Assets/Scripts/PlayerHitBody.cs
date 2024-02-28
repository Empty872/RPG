using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBody : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int hitPoints;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0) Die();
    }

    public void Die()
    {
        // Destroy(gameObject);
    }
}