using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelLoader : MonoBehaviour
{
    [SerializeField] CanvasGroup fadeObj;
    [SerializeField] float transitionTime;
    float transitionTimer = 0;
    bool transitionOut = false,transitionIn = false;
    bool reloading = false;
    public int level;

    private void Start()
    {
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
            transitionTimer += Time.deltaTime;
        }
        if (transitionOut)
        {
            
            if(fadeObj != null)
            {
                fadeObj.alpha = transitionTimer / transitionTime;
            }
            if(transitionTimer >= transitionTime)
            {
                
                LoadLvl();
            }
            transitionTimer += Time.deltaTime;
        }
    }

    [ContextMenu("Run")]
    public void Load()
    {
        transitionOut = true;
    }
    [ContextMenu("ForceLoad")]
    void LoadLvl()
    {
        //todo check if the level is unlocked

        if (reloading)
        {
            SceneManager.LoadScene(level);
            return;
        }

        if (SceneManager.sceneCountInBuildSettings > level + 1)
        {
            SceneManager.LoadScene(level + 1);
            FindObjectOfType<MuffleMusic>().VolDown();
        }
        else
        {
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



