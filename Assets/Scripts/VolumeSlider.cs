using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string path;
    public void SetFloat(float f)
    {
        audioMixer.SetFloat(path,Mathf.Log10(f) * 20);
    }
}
