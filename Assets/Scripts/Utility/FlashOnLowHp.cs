using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashOnLowHp : MonoBehaviour
{
    public Flash flash;
    public float threshold = 50;
    Health health;
    public float freq;
    public Color color;
    public AudioSource heartBeat;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        flash.pulseColour = color;
        if(health.health < threshold)
        {
            flash.frequncy = freq;
            heartBeat.gameObject.SetActive(true);
        }
        else
        {
            flash.frequncy = 0;
            heartBeat.gameObject.SetActive(false);
        }
    }
}
