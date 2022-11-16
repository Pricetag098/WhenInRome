using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class Dash : MonoBehaviour
{
    

    [SerializeField] Animator animator;
    [Header("Dash Variables")]
    [SerializeField] float dashForce;
    [SerializeField] float dashDuration,iFrames;
    [SerializeField] bool dashOnMouse;



    [Header("Stamina")]
    public float maxStamina = 100;
    [SerializeField] float staminaGainRate = 5;
    [SerializeField] float staminaBurnedOnDash = 33;
     public float stamina;

    [Header("Sound")]
    [SerializeField] SoundPlayer soundPlayer;
    Health health;
    Vector3 dashDir;
    bool dashing;
    Rigidbody rb;
    PlayerMove mv;
    PlayerAim aim;


    public Smear smear;
    PlayerInputs inputActions;
    InputAction dash;
    private void Awake()
    {
        inputActions = new PlayerInputs();
        dash = inputActions.Player.Dash;

        dash.performed += DoDash;
    }
    private void OnEnable()
    {
        
        dash.Enable();
    }
    private void OnDisable()
    {
        dash.Disable();
    }
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
        
        //dashDir = Vector3.one;
        
        stamina = Mathf.Clamp(stamina + staminaGainRate * Time.deltaTime, 0, maxStamina);

    }

    void DoDash(InputAction.CallbackContext context)
    {
        dashDir = dashOnMouse ? aim.aimDir : mv.inputDir;
        if (!dashing && dashDir != Vector3.zero && stamina >= staminaBurnedOnDash)
        {
            health.AddIFrames(iFrames);
            StartCoroutine("PauseMv");
            rb.velocity = dashDir * dashForce;
            stamina -= staminaBurnedOnDash;
            soundPlayer.Play();
            animator.SetTrigger("Dash");
            smear.SmearModel(mv.relVect);
        }
    }

    public IEnumerator PauseMv()
    {
        dashing = true;
        mv.canMove = false;
        yield return new WaitForSeconds(dashDuration);
        mv.canMove = true;
        //rb.velocity = Vector3.zero;
        dashing = false;
    }
}
