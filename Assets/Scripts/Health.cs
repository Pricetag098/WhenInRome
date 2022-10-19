using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
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
=======

public class Health : MonoBehaviour
{

    public float health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
>>>>>>> Guns
    }
}
