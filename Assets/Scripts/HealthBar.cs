using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Health health;
    public Image bar;
 
    private void Update()
    {
     
        bar.fillAmount = health.health / health.maxHealth;
    }
}
