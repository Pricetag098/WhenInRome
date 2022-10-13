using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float counter;

    public Vector3 inputDir;
    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        inputDir = new Vector3(Input.GetAxisRaw("Horizontal"),0 , Input.GetAxisRaw("Vertical"));
        inputDir = inputDir.normalized;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 vel = rb.velocity;
        Vector3 idealVel = inputDir * maxSpeed;
        Vector3 maxVel = vel.normalized * maxSpeed;
        Vector3 inputVel = inputDir * acceleration;
        
        //Control force in x component

        if(inputDir.x == 0)
        {
            Vector3 counterX = Vector3.right * -rb.velocity.x * counter;
            rb.AddForce(counterX);
        }
        if(inputDir.x > 0)
        {
            if(vel.x < 0)
            {
                Vector3 counterX = Vector3.right * -rb.velocity.x * counter;
                rb.AddForce(counterX);
            }
            if(rb.velocity.x + inputVel.x > idealVel.x)
            {
                inputVel.x = inputVel.x - Mathf.Clamp(idealVel.x - vel.x, 0, float.PositiveInfinity);
                
            }
        }
        if(inputDir.x < 0)
        {
            if (vel.x > 0)
            {
                Vector3 counterX = Vector3.right * -rb.velocity.x * counter;
                rb.AddForce(counterX);
            }
            if (rb.velocity.x + inputVel.x < idealVel.x)
            {
                inputVel.x = Mathf.Clamp(idealVel.x - vel.x, float.NegativeInfinity, 0);

            }
        }

        if (inputDir.z == 0)
        {
            Vector3 counterZ = Vector3.forward * -rb.velocity.z * counter;
            rb.AddForce(counterZ);
        }

        if (inputDir.z > 0)
        {
            if (vel.z < 0)
            {
                Vector3 counterZ = Vector3.forward * -rb.velocity.z * counter;
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
                Vector3 counterZ = Vector3.forward * -rb.velocity.z * counter;
                rb.AddForce(counterZ);
            }
            if (rb.velocity.z + inputVel.z < idealVel.z)
            {
                inputVel.z = Mathf.Clamp(idealVel.z - vel.z, float.NegativeInfinity, 0);

            }
        }

        rb.AddForce(inputVel);
    }
}
