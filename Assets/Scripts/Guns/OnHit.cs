using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnHit : MonoBehaviour
{
    //public UnityEvent onHit;
    CombatMeter meter; 
    SpawnParticlesOnHit spawnParticlesOnHit;
    public float timeSinceLastHit = 0;
    // Start is called before the first frame update
    void Start()
    {
        meter = GetComponent<CombatMeter>();
        spawnParticlesOnHit = GetComponent<SpawnParticlesOnHit>();
    }
    
    // Update is called once per frame
    void Update()
    {
        timeSinceLastHit += Time.deltaTime;
    }
    public void Hit(HitData data)
    {
        timeSinceLastHit = 0;
        if (meter != null)
            meter.Charge(data);
        if(spawnParticlesOnHit != null)
            spawnParticlesOnHit.Hit(data);
    }
}
