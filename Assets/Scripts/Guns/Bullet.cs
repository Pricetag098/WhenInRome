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
    Material mat;
    public MeshRenderer renderer;

    public bool damageChanges;
    public float sizeOffset;

    // Start is called before the first frame update
    void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        rb = GetComponent<Rigidbody>();
       
    }
    IEnumerator Change()
    {
        float trailTime = GetComponent<TrailRenderer>().time;
        float trailSize = GetComponent<TrailRenderer>().widthMultiplier;
        while (gameObject.activeSelf && transform.localScale.x > 0)
        {
            Debug.Log(damage);
            float size = damage;//GetComponent<PooledObj>().owner.gameObject.GetComponent<Gun>().damage;
            transform.localScale = sizeOffset * new Vector3(size, size, size);
            GetComponent<TrailRenderer>().time = trailTime * (damage / maxDmg);
            //Debug.Log(GetComponent<TrailRenderer>().time);
            GetComponent<TrailRenderer>().widthMultiplier = trailSize * (damage / maxDmg);
            yield return null;
        }
        transform.localScale = Vector3.zero;
        

    }

    public void Init(Vector3 vel, float dmg,float fallOff)
    {
        

        damage = dmg;
        maxDmg = dmg;
        decayRate = fallOff;
        rb.velocity = vel;
        if(trailRenderer != null)
        trailRenderer.enabled = true;
        if(decayRate > 0)
        {
            mat = renderer.material;
        }

        if (damageChanges)
        {
            StartCoroutine(Change());
        }
        else if(sizeOffset > 0)
        {
            float size = damage;//GetComponent<PooledObj>().owner.gameObject.GetComponent<Gun>().damage;
            transform.localScale = sizeOffset * new Vector3(size, size, size);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(decayRate > 0)
        {
            damage = maxDmg * (1-Mathf.Pow(age/maxAge,decayRate));
            mat.SetFloat("Val", damage/ maxDmg);
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

            if (sizeOffset > 0)
            {
                GetComponent<TrailRenderer>().time = 0.1f;
                GetComponent<TrailRenderer>().widthMultiplier = 0.2f;
            }
            
        }

        Despawn();
        
    }
    void Despawn()
    {
        if (trailRenderer != null)
            trailRenderer.enabled = false;
        age = 0;
        if (sizeOffset > 0)
        {
            GetComponent<TrailRenderer>().time = 0.1f;
            GetComponent<TrailRenderer>().widthMultiplier = 0.2f;
        }

        rb.velocity = Vector3.zero;
        PooledObj obj = GetComponent<PooledObj>();
        if(obj != null) { obj.Despawn(); }
        else { Destroy(gameObject); }
    }
}
