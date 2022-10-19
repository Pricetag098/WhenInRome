using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class Health : MonoBehaviour
{
    
    public float health;
    public float maxHealth;

    public UnityEvent onHit;
    public UnityEvent onDeath;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDmg(float dmg)
    {
        health -= dmg;
        onHit.Invoke();
        if(health <= 0)
        {
            onDeath.Invoke();
        }
    }
    public void Heal(float amount)
    {
        health += amount;

    }
}
