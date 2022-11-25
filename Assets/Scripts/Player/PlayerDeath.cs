using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
public class PlayerDeath : MonoBehaviour
{
    PlayerMove move;
    public float delay;
    public Animator animator;
    LevelLoader levelLoader;
    public SoundPlayer deathSound;
    public Volume deathPP;
    public float greySpeed;
    Rigidbody rb;
    bool dead = false;
    public float timer = 0;
    private void Start()
    {
        move = GetComponent<PlayerMove>();
        levelLoader = GetComponent<LevelLoader>();
        rb = GetComponent<Rigidbody>();
        
    }
    private void Update()
    {
        if (dead)
        {
            if(timer > delay)
            {
                levelLoader.Reload();
            }
            timer += Time.deltaTime;
            deathPP.weight += Time.deltaTime * greySpeed;
        }
    }

    public void Die()
    {
        if (!dead)
        {
            //enabled = false;
            deathSound.Play();
            move.enabled = false;
            rb.velocity = Vector3.zero;
            animator.SetTrigger("Die");
            dead = true;
            FindObjectOfType<MuffleMusic>().Muffle();
            
        }
        
    }

}
