using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float maxAge = 1;
    float age = 0;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        age = 0;
        PooledObj obj = GetComponent<PooledObj>();
        if(obj != null) { obj.Despawn(); }
        else { Destroy(gameObject); }
    }
}
