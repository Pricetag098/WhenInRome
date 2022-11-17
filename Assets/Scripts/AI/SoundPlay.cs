using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Play", menuName = "Ai/Sound/Play")]
public class SoundPlay : TreeNode
{
    public bool waitForEnd = false;
    public TreeNode passThrough;
    public float volume = 1, pitch =1, pitchRange;
    public List<AudioClip> clipList = new List<AudioClip>();
    AudioSource source;
    public override void Run()
    {
        if(!source.isPlaying || !waitForEnd)
        Play();
        
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