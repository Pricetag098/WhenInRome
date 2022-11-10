using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthVignette : MonoBehaviour
{
    public Health health;
    CanvasGroup canvas;

    private void Start()
    {
        canvas = GetComponent<CanvasGroup>();
    }
    private void Update()
    {

        canvas.alpha = 1 - health.health / health.maxHealth;
    }
}
