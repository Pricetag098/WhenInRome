using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelLoader : MonoBehaviour
{
    [SerializeField] CanvasGroup fadeObj;
    [SerializeField] public float transitionTime;
    float transitionTimer = 0;
    public int lastLevel;
    bool transitionOut = false,transitionIn = false;
    bool reloading = false;
    public int level;

    public bool fadeIn;

    private void Start()
    {
        if(fadeIn)
        transitionIn = true;
    }
    private void Update()
    {
        if (transitionIn)
        {
            
            if (fadeObj != null)
            {
                fadeObj.alpha =  1-(transitionTimer / transitionTime);
            }
            if (transitionTimer >= transitionTime)
            {
                transitionIn = false;
                transitionTimer = 0;
            }
            transitionTimer += Time.unscaledDeltaTime;
        }
        if (transitionOut)
        {
            
            if(fadeObj != null)
            {
                fadeObj.alpha = transitionTimer / transitionTime;
            }
            if(transitionTimer >= transitionTime)
            {
                Debug.Log("loaded");
                LoadLvl();
            }
            transitionTimer += Time.unscaledDeltaTime;
        }
    }

    [ContextMenu("Run")]
    public void Load()
    {
        if(level+1 == SceneManager.GetActiveScene().buildIndex)
        {
            MuffleMusic muffleMusic = FindObjectOfType<MuffleMusic>();
            //muffleMusic.Muffle();
            level = level+1;
            Reload();
            if(muffleMusic.music.volume == muffleMusic.unMuffledVol)
            {
                muffleMusic.Muffle();
            }
        }
        transitionOut = true;
    }
    [ContextMenu("ForceLoad")]
    void LoadLvl()
    {
        Time.timeScale = 1;
        //todo check if the level is unlocked
        
        if (reloading)
        {
            SceneManager.LoadScene(level);
            MuffleMusic muffleMusic = FindObjectOfType<MuffleMusic>();
            if (muffleMusic != null) 
            {
                muffleMusic.UnPause();
            }

            return;
        }
        if(level == lastLevel)
        {
            MuffleMusic muffleMusic = FindObjectOfType<MuffleMusic>();
            if (muffleMusic != null)
            {
                //muffleMusic.Muffle();
                muffleMusic.VolDown();
            }
            SceneManager.LoadScene(0);
        }
        if (SceneManager.sceneCountInBuildSettings > level + 1)
        {
            SceneManager.LoadScene(level + 1);
            MuffleMusic muffleMusic = FindObjectOfType<MuffleMusic>();
            if (muffleMusic != null)
            {
                //muffleMusic.Muffle();
                //muffleMusic.VolDown();
            }
        }
        else
        {
            MuffleMusic muffleMusic = FindObjectOfType<MuffleMusic>();
            if (muffleMusic != null)
            {
                //muffleMusic.Muffle();
                muffleMusic.VolDown();
            }
            SceneManager.LoadScene(0);
        }
            
    }

    public void Reload()
    {
        transitionOut = true;
        //level = level - 1;
        reloading = true;
    }

}



