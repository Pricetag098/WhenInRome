using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxPlayer : MonoBehaviour
{
    ParticleSystem particle;
    SoundPlayer soundPlayer;
    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        soundPlayer = GetComponent<SoundPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(particle != null)
        particle.Play();
        if(soundPlayer != null)
        soundPlayer.Play();
    }
}
