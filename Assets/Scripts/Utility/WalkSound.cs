using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float timeBetween;
    [SerializeField] SoundPlayer player;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity != Vector3.zero && timer > timeBetween)
        {
            player.Play();
            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
