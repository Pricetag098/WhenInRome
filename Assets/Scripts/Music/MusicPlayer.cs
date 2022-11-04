using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicPlayer : MonoBehaviour
{
    CombatMeter inCombat;
    [HideInInspector]
    public MuffleMusic muffler;
    AudioSource musicSource;
    public AudioClip[] songs;
    int lastLvl = -1;
    int currentScene = 0;
    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        muffler = GetComponent<MuffleMusic>();
        if (FindObjectsOfType<MusicPlayer>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene != lastLvl)
        {
            if(songs.Length > currentScene)
            {
                musicSource.clip = songs[currentScene];
                if(musicSource.clip != null)
                musicSource.Play();
                lastLvl = currentScene;
                muffler.VolUp();
                
            }
        }
        
    }
   
}
