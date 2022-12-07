using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float maxAge = 1;
    [SerializeField] float decayRate = 0;
    float age = 0;
    Rigidbody rb;
    float maxDmg;
    float damage;
    TrailRenderer trailRenderer;


    // Start is called before the first frame update
    void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    public void Init(Vector3 vel, float dmg,float fallOff)
    {
        damage = dmg;
        maxDmg = dmg;
        decayRate = fallOff;
        rb.velocity = vel;
        if(trailRenderer != null)
        trailRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(decayRate > 0)
        {
            damage = maxDmg * (1-Mathf.Pow(age/maxAge,decayRate));
        }
        if(age > maxAge) { Despawn(); }
        age += Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject owner = GetComponent<PooledObj>().owner.owner;
        if (collision.gameObject != owner)
        {
            HitData hitData = new HitData();
            hitData.damage = 0;
            ContactPoint contactPoint = collision.GetContact(0);
            hitData.hitObject = collision.gameObject;
            hitData.position = contactPoint.point;
            hitData.dir = contactPoint.normal;
            Health health = collision.gameObject.GetComponent<Health>();

            //check if we hit a valid target
            if (health != null)
            {
                health.TakeDmg(damage);
                hitData.damage = damage;
                
            }
            //reflect any on-hit effects 
            if (owner != null)
            {
                OnHit onHit = owner.GetComponent<OnHit>();
                if (onHit != null)
                {
                    onHit.Hit(hitData);
                }
            }
        }

        Despawn();
        
    }
    void Despawn()
    {
        if (trailRenderer != null)
            trailRenderer.enabled = false;
        age = 0;
        rb.velocity = Vector3.zero;
        PooledObj obj = GetComponent<PooledObj>();
        if(obj != null) { obj.Despawn(); }
        else { Destroy(gameObject); }
    }
}
