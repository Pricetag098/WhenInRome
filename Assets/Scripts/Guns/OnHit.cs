using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnHit : MonoBehaviour
{
    //public UnityEvent onHit;
    CombatMeter meter; 
    public float timeSinceLastHit = 0;
    // Start is called before the first frame update
    void Start()
    {
        meter = GetComponent<CombatMeter>();
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
    }
}
