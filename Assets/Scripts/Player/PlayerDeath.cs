using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
public class PlayerDeath : MonoBehaviour
{
    PlayerMove move;
    public float timeBeforeFade;
    public Animator animator;
    LevelLoader levelLoader;
    public SoundPlayer deathSound;
    public Volume deathPP;
    public float fadeTime;
    Rigidbody rb;
    bool dead = false;
    bool wait = true;
    public float waitTimer;
    public float timeBeforeAnim;
    public float fadeTimer = 0;
    public AudioSource heartbeat;
    private void Start()
    {
        move = GetComponent<PlayerMove>();
        levelLoader = GetComponent<LevelLoader>();
        rb = GetComponent<Rigidbody>();
        
    }

    IEnumerator Animate()
    {
        if (dead)
        {
            if (wait)
            {
                while (waitTimer < timeBeforeAnim)
                {
                    deathPP.weight = 1;
                    waitTimer += Time.unscaledDeltaTime;
                    yield return null;
                }
                animator.SetTrigger("Die");
                animator.updateMode = AnimatorUpdateMode.UnscaledTime;
                wait = false;
                levelLoader.transitionTime = fadeTime;
                levelLoader.Reload();
                yield return null;
            }
        }
    }

    public void Die()
    {
        if (!dead)
        {
            deathSound.Play();
            move.enabled = false;
            rb.velocity = Vector3.zero;
            Time.timeScale = 0;
            animator.SetTrigger("Die");
            dead = true;
            MuffleMusic mm = FindObjectOfType<MuffleMusic>();
            if (mm != null)
            mm.GetComponent<AudioSource>().Pause();
            heartbeat.volume = 0;
            heartbeat.loop = false;
            StartCoroutine(Animate());
            
        }
        
    }

}
