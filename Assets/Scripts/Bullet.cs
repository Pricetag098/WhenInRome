using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float maxAge = 1;
    float age = 0;
    Rigidbody rb;

    TrailRenderer trailRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        rb = GetComponent<Rigidbody>();
    }
    
    public void Init(Vector3 vel)
    {
       
        rb.velocity = vel;
        trailRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(age > maxAge) { Despawn(); }
        age += Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Despawn();
        
    }
    void Despawn()
    {
        trailRenderer.enabled = false;
        age = 0;
        rb.velocity = Vector3.zero;
        PooledObj obj = GetComponent<PooledObj>();
        if(obj != null) { obj.Despawn(); }
        else { Destroy(gameObject); }
    }
}
