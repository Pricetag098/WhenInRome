using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MuffleMusic : MonoBehaviour
{
    private AudioMixer output;
    private AudioSource music;
    private bool muffled = true;
    public float highpass;
    public float lowpass;
    public float frequencyGain;
    public float transitionTime;
    public float volumeTime;
    private bool loud;
    //public AudioClip[] songs;
    // Start is called before the first frame update
    void Start()
    {
        output = GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer;
        music = GetComponent<AudioSource>();
        StartCoroutine(VolumeChange());
        
    }

    public IEnumerator Muffle()
    {
        float timer = 0;
        if (!muffled)
        {
            while (timer < 1)
            {
                timer += Time.deltaTime / transitionTime;
                output.SetFloat("Lowpass Simple", Mathf.Lerp(lowpass, 22000, 1 - timer));
                output.SetFloat("Highpass Simple", Mathf.Lerp(highpass, 0, 1 - timer));
                output.SetFloat("FrequencyGain", Mathf.Lerp(frequencyGain, 1, 1 - timer));
                yield return null;
            }
            muffled = true;
        }
        else
        {
            while (timer < 1)
            {
                timer += Time.deltaTime / transitionTime;
                output.SetFloat("Lowpass Simple", Mathf.Lerp(lowpass, 22000, timer));
                output.SetFloat("Highpass Simple", Mathf.Lerp(highpass, 0, timer));
                output.SetFloat("FrequencyGain", Mathf.Lerp(frequencyGain, 1, timer));
                yield return null;
            }
            muffled = false;
        }
       
    }

    
    public IEnumerator VolumeChange()
    {
        float timer = 0;
        if (!loud)
        {
            while (timer < 1)
            {
                timer += Time.deltaTime / volumeTime;
                music.volume = Mathf.Lerp(0, 1, timer);
                yield return null;
            }
            loud = true;
        }
        else
        {
            while (timer < 1)
            {
                timer += Time.deltaTime / volumeTime;
                music.volume = Mathf.Lerp(0, 1, 1 - timer);
                yield return null;
            }
            loud = false;
        }

    }
}
