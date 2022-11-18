using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxPlayer : MonoBehaviour
{
    [SerializeField]ParticleSystem particle;
    [SerializeField]SoundPlayer soundPlayer;
    // Start is called before the first frame update
    void Start()
    {
        //particle = GetComponent<ParticleSystem>();
        //soundPlayer = GetComponent<SoundPlayer>();
    }

    // Update is called once per frame
    public void Play()
    {
        //if(particle != null)
            particle.Play();
        //if(soundPlayer != null)
            soundPlayer.Play();
    }
}
