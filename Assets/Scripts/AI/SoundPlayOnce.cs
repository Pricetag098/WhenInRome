using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
[CreateAssetMenu(fileName = "PlayOnce", menuName = "Ai/Sound/PlayOnce")]
public class SoundPlayOnce : TreeNode
{
    bool played = false;
    public TreeNode passThrough;
    public float volume =1, pitch =1, pitchRange;
    public List<AudioClip> clipList = new List<AudioClip>();
    public AudioMixerGroup output;
    AudioSource source;
    public override void Run()
    {
        if (!played)
        {
            played = true;
            Play();
        }
            

        if (passThrough != null)
        {

            passThrough.Run();
        }

    }
    public override void Innit(Ai owner)
    {
        ai = owner;

        source = ai.gameObject.AddComponent<AudioSource>();
        source.volume = volume;
        source.outputAudioMixerGroup = output;
        if (passThrough != null)
        {
            passThrough = Instantiate(passThrough);
            passThrough.Innit(owner);
        }
    }
    public override void Tick()
    {
        if (passThrough != null)
        {
            passThrough.Tick();
        }
    }

    public void Play()
    {
        if (clipList.Count == 0) { return; }
        AudioClip clip = clipList[Random.Range(0, clipList.Count)];
        float rand = (Random.value - .5f) * 2;
        rand *= pitchRange;
        source.pitch = pitch + rand;
        source.clip = clip;
        source.Play();

    }
}