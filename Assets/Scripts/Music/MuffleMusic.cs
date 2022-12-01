using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MuffleMusic : MonoBehaviour
{
    private AudioMixer output;
    [SerializeField]
    public AudioSource music;
    public float muffledVol;
    public float unMuffledVol;
    //private bool muffled = true;
    public float highpass;
    public float lowpass;
    public float frequencyGain;
    public float transitionTime;
    public float volumeTime;
    //private bool loud;
    //public AudioClip[] songs;
    // Start is called before the first frame update
    void Start()
    {
        output = GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer;
        music = GetComponent<AudioSource>();
        //StartCoroutine(VolumeChange());
        
    }

    private void Update()
    {
        //Debug.Log(music.volume);
        if(Time.deltaTime > 0.3)
        {
            Debug.Log(music.volume);
        }
        
    }

    public void Muffle()
    {
        StopCoroutine("UnMuffleIE");
        StartCoroutine("MuffleIE");
    }
    public void UnMuffle()
    {
        StopCoroutine("MuffleIE");
        StartCoroutine("UnMuffleIE");
    }
    public void UnPause()
    {
        if (!music.isPlaying)
        {
            Debug.Log("respawn");
            music.UnPause();
            StartMuffled();
            VolUp();
        }
    }

    void StartMuffled()
    {
        output.SetFloat("Lowpass Simple", lowpass);
        output.SetFloat("Highpass Simple", highpass);
        output.SetFloat("FrequencyGain", frequencyGain);
        music.volume = unMuffledVol;
    }
    IEnumerator MuffleIE()
    {
       
        float timer = 0;
        float currentVol = music.volume;
        while (timer < 1)
        {
            timer += Time.unscaledDeltaTime / transitionTime;
            
            output.SetFloat("Lowpass Simple", Mathf.Lerp(lowpass, 22000, 1 - timer));
            output.SetFloat("Highpass Simple", Mathf.Lerp(highpass, 0, 1 - timer));
            output.SetFloat("FrequencyGain", Mathf.Lerp(frequencyGain, 1, 1 - timer));
            music.volume = Mathf.Lerp(muffledVol, unMuffledVol, 1 - timer);
            yield return null;
        }
       
        //muffled = true;
    }

 
    IEnumerator UnMuffleIE()
    {
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime / transitionTime;
            output.SetFloat("Lowpass Simple", Mathf.Lerp(lowpass, 22000, timer));
            output.SetFloat("Highpass Simple", Mathf.Lerp(highpass, 0, timer));
            output.SetFloat("FrequencyGain", Mathf.Lerp(frequencyGain, 1, timer));
            music.volume = Mathf.Lerp(muffledVol, unMuffledVol, timer);
            yield return null;
        }
        //muffled = false;
    }

    public void VolUp()
    {
        StopCoroutine("VolumeDown");
        StartCoroutine("VolumeUp");
    }
    public void VolDown()
    {
        StopCoroutine("VolumeUp");
        //Debug.Log(gameObject);
        StartCoroutine("VolumeDown");
    }
    
    IEnumerator VolumeUp()
    {
        float timer = 0;
        
        while (timer < 1)
        {
            timer += Time.unscaledDeltaTime / volumeTime;
            music.volume = Mathf.Lerp(0, 1, timer);
            yield return null;
        }
        //loud = true;
    }
    IEnumerator VolumeDown()
    {
        float timer = 0;

        while (timer < 1)
        {
            timer += Time.unscaledDeltaTime / volumeTime;
            music.volume = Mathf.Lerp(0, 1, 1 - timer);
            yield return null;
        }
        //loud = false;
    }
}
