using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] float dashForce;
    [SerializeField] float dashDuration;
    Vector3 dashDir;
    [SerializeField] bool dashOnMouse;

    Rigidbody rb;
    PlayerMove mv;
    PlayerAim aim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mv = GetComponent<PlayerMove>();
        aim = GetComponent<PlayerAim>();
    }

    // Update is called once per frame
    void Update()
    {
        dashDir = dashOnMouse ? mv.inputDir : aim.aimDir;
        //dashDir = Vector3.one;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            
            StartCoroutine("PauseMv");
            rb.velocity = dashDir * dashForce;
        }
    }

    public IEnumerator PauseMv()
    {
        mv.enabled = false;
        yield return new WaitForSeconds(dashDuration);
        mv.enabled = true;
    }
}
