using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMeter : MonoBehaviour
{
    public float meter;
    public float maxMeter;
    [SerializeField] float decayRate = 1, decayDelay = 1;
    [SerializeField] float chargePerDamage = 10;
    [SerializeField] SoundPlayer chargerdSound;
    public bool inCombat;
    
    //[SerializeField] float enemyDetectRad = 100;
    [SerializeField] LayerMask enemyLayer = 8;
    OnHit oh;
    float timeSinceLastHit;
    public Floor floor;
    bool wasCharged = false;
    // Start is called before the first frame update
    void Start()
    {
        oh = GetComponent<OnHit>();
        //music = FindObjectOfType<MusicPlayer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (floor != null)
        {
            inCombat = floor.inCombat; //Physics.CheckSphere(transform.position, enemyDetectRad, enemyLayer);
        }
        else
        {
            inCombat = true;
        }
        

        if (inCombat)
        {
            timeSinceLastHit += Time.deltaTime;
            if (timeSinceLastHit > decayDelay && meter <maxMeter)
            {

                meter = Mathf.Clamp(meter - timeSinceLastHit * timeSinceLastHit  * decayRate * Time.deltaTime, 0, maxMeter);
            }
        }
        /*
        if(music != null)
        {
            if (inCombat && !wasInCombat)
            {
                wasInCombat = true;
                music.muffler.UnMuffle();
            }
            if (!inCombat && wasInCombat)
            {
                wasInCombat = false;
                music.muffler.Muffle();
            }
        }
        */
        if(meter == maxMeter && !wasCharged)
        {
            wasCharged = true;
            chargerdSound.Play();
        }
        
    }
    public void Discharge()
    {
        meter = 0;
        wasCharged=false;
    }
    public void Charge(HitData data)
    {
        meter += chargePerDamage * data.damage;
        if(meter > maxMeter)
        {
            meter = maxMeter;
        }
        timeSinceLastHit = 0;
    }
}
