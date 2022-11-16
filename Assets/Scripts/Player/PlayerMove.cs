using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] Animator animator;

    PlayerAim playerAim;
    public bool canMove = true;
    //[SerializeField] float acceleration;
    //[SerializeField] float counter;

    public Vector3 inputDir;
    Rigidbody rb;
    public Vector3 relVect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAim = GetComponent<PlayerAim>();
    }


    PlayerInputs inputActions;
    InputAction move;
    private void Awake()
    {
        inputActions = new PlayerInputs();
        move = inputActions.Player.Move;
    }
    private void OnEnable()
    {
        
        move.Enable();
    }
    private void OnDisable()
    {
        move.Disable();
    }
    private void Update()
    {

        //inputDir = new Vector3(Input.GetAxisRaw("Horizontal"),0 , Input.GetAxisRaw("Vertical"));
        Vector2 contIn = move.ReadValue<Vector2>();
        inputDir.x = contIn.x;
        inputDir.z = contIn.y;
        //inputDir = inputDir.normalized;
        if(canMove)
            rb.velocity = inputDir*maxSpeed;

        animator.SetFloat("Vel", rb.velocity.magnitude);
        Vector2 vect = FindVelRelativeToLook().normalized;
        relVect.x = vect.x;
        relVect.z = vect.y;
        animator.SetFloat("VelX",vect.x);
        animator.SetFloat("VelY", vect.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = rb.velocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }

    private void OnDrawGizmos()
    {
        
    }
}
/*

Vector3 vel = rb.velocity;
        Vector3 idealVel = inputDir * maxSpeed;
        Vector3 maxVel = vel.normalized * maxSpeed;
        Vector3 inputVel = inputDir * acceleration;

        //Control force in x component

        Vector3 counterX = Vector3.right * -rb.velocity.x * counter;
        if (inputDir.x == 0)
        {
            rb.AddForce(counterX);
        }
        if (inputDir.x > 0)
        {
            if (vel.x < 0)
            {
                rb.AddForce(counterX);
            }
            if (vel.x > idealVel.x)
            {
                rb.AddForce(Vector3.right * -(vel.x * vel.x));
                inputDir.x = 0;
            }
        }
        if (inputDir.x < 0)
        {
            if (vel.x > 0)
            {
                rb.AddForce(counterX);
            }
            if (vel.x < idealVel.x)
            {
                rb.AddForce(Vector3.right * (vel.x * vel.x));
                inputDir.x = 0;
            }
        }

        Vector3 counterZ = Vector3.forward * -rb.velocity.z * counter;
        if (inputDir.z == 0)
        {
            rb.AddForce(counterZ);
        }

        if (inputDir.z > 0)
        {
            if (vel.z < 0)
            {
                rb.AddForce(counterZ);
            }
            if (vel.z > idealVel.z)
            {
                rb.AddForce(Vector3.forward * -vel.z * 2);
                inputDir.z = 0;
            }
        }
        if (inputDir.z < 0)
        {
            if (vel.z > 0)
            {
                rb.AddForce(counterZ);
            }
            if (vel.z < idealVel.z)
            {
                rb.AddForce(Vector3.forward * -vel.z * 2);
                inputDir.z = 0;
            }
        }

        rb.AddForce(inputVel);

    }




 Vector3 vel = rb.velocity;
        Vector3 idealVel = inputDir * maxSpeed;
        Vector3 maxVel = vel.normalized * maxSpeed;
        Vector3 inputVel = inputDir * acceleration;

        //Control force in x component

        Vector3 counterX = Vector3.right * -rb.velocity.x * counter;
        if (inputDir.x == 0)
        {
            rb.AddForce(counterX);
        }
        if (inputDir.x > 0)
        {
            if (vel.x < 0)
            {
                rb.AddForce(counterX);
            }
            if (rb.velocity.x + inputVel.x > idealVel.x)
            {
                inputVel.x = Mathf.Clamp(idealVel.x - vel.x, 0, float.PositiveInfinity);

            }
        }
        if (inputDir.x < 0)
        {
            if (vel.x > 0)
            {
                rb.AddForce(counterX);
            }
            if (rb.velocity.x + inputVel.x < idealVel.x)
            {
                inputVel.x = Mathf.Clamp(idealVel.x - vel.x, float.NegativeInfinity, 0);

            }
        }

        Vector3 counterZ = Vector3.forward * -rb.velocity.z * counter;
        if (inputDir.z == 0)
        {
            rb.AddForce(counterZ);
        }

        if (inputDir.z > 0)
        {
            if (vel.z < 0)
            {
                rb.AddForce(counterZ);
            }
            if (rb.velocity.z + inputVel.z > idealVel.z)
            {
                inputVel.z = Mathf.Clamp(idealVel.z - vel.z, 0, float.PositiveInfinity);

            }
        }
        if (inputDir.z < 0)
        {
            if (vel.z > 0)
            {
                rb.AddForce(counterZ);
            }
            if (rb.velocity.z + inputVel.z < idealVel.z)
            {
                inputVel.z = Mathf.Clamp(idealVel.z - vel.z, float.NegativeInfinity, 0);

            }
        }

        rb.AddForce(inputVel);
 */
