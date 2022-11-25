using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashVignette : MonoBehaviour
{
    public Health health;
    public float duration =1;
    float timer = 0;
    CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        health.onHit.AddListener(Fill);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            canvasGroup.alpha = timer / duration;
            timer -= Time.deltaTime;
        }
    }
    void Fill()
    {
        timer = duration;
    }
}
