using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBody : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float hitPoints;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0) Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}