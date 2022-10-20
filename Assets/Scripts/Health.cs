using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class Health : MonoBehaviour
{
    
    public float health;
    public float maxHealth;
    public float iFrames = 0;

    public UnityEvent onHit;
    public UnityEvent onDeath;

    public Collider col;

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
        if(iFrames > 0) { return; }
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
    public void AddIFrames(float amount)
    {
        iFrames += amount;
    }
}
