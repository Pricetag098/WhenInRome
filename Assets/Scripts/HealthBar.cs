using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Health health;
    Image bar;

    private void Start()
    {
        bar = GetComponent<Image>();
    }
    private void Update()
    {
     
        bar.fillAmount = health.health / health.maxHealth;
    }
}
