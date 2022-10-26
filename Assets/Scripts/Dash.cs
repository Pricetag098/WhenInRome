using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Dash Variables")]
    [SerializeField] float dashForce;
    [SerializeField] float dashDuration,iFrames;
    [SerializeField] bool dashOnMouse;

    [Header("Stamina")]
    [SerializeField] float maxStamina = 100;
    [SerializeField] float staminaGainRate = 5;
    [SerializeField] float staminaBurnedOnDash = 33;
    [SerializeField] float stamina;

    [Header("Sound")]
    [SerializeField] SoundPlayer soundPlayer;
    Health health;
    Vector3 dashDir;
    bool dashing;
    Rigidbody rb;
    PlayerMove mv;
    PlayerAim aim;
    // Start is called before the first frame update
    void Start()
    {
        
        stamina = maxStamina;
        rb = GetComponent<Rigidbody>();
        mv = GetComponent<PlayerMove>();
        aim = GetComponent<PlayerAim>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        dashDir = dashOnMouse ? aim.aimDir : mv.inputDir;
        //dashDir = Vector3.one;
        if (Input.GetKeyDown(KeyCode.LeftShift) && !dashing && dashDir != Vector3.zero && stamina >= staminaBurnedOnDash)
        {
            health.AddIFrames(iFrames);
            StartCoroutine("PauseMv");
            rb.velocity = dashDir * dashForce;
            stamina -= staminaBurnedOnDash;
            soundPlayer.Play();
        }


        stamina = Mathf.Clamp(stamina + staminaGainRate * Time.deltaTime, 0, maxStamina);
    }

    public IEnumerator PauseMv()
    {
        dashing = true;
        mv.enabled = false;
        yield return new WaitForSeconds(dashDuration);
        mv.enabled = true;
        rb.velocity = Vector3.zero;
        dashing = false;
    }
}
