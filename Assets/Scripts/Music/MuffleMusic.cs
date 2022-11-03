using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MuffleMusic : MonoBehaviour
{
    private AudioMixer output;
    private bool muffled;
    public float highpass;
    public float lowpass;
    public float frequencyGain;
    public float transitionTime;
    // Start is called before the first frame update
    void Start()
    {
        output = GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer;
        
        StartCoroutine(Muffle());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine(Muffle());
        }
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
                yield return null;
            }
            muffled = false;
        }
       
    }
}
