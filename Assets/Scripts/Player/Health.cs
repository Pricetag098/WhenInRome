using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float iFrames = 0;

    public UnityEvent onHit;
    public UnityEvent onDeath;

    public Collider col;

    bool dead;
    private void Start()
    {
        col = GetComponent<Collider>();
        health = maxHealth;
    }
    public void Update()
    {
        iFrames = Mathf.Clamp(iFrames - Time.deltaTime, 0, float.PositiveInfinity);
        col.enabled = !(iFrames > 0);
        
    }

    public void TakeDmg(float dmg)
    {
        if (dead)
        {
            return;
        }
        if(iFrames > 0) { return; }
        health -= dmg;
        onHit.Invoke();
        if(health <= 0)
        {
            onDeath.Invoke();
            if (GetComponent<AiDeath>())
            {
                GetComponent<AiDeath>().Die();
            }
            dead = true;
        }
        if (health <= 0)
        {
            health = 0;
        }
    }
    public void Heal(float amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    public void AddIFrames(float amount)
    {
        iFrames += amount;
    }
}
