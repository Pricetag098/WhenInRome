using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceVel : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rb != null)
        transform.forward = rb.velocity;
    }
}
