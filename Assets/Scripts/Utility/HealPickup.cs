using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickup : MonoBehaviour
{
    SoundPlayer sound;
    public float healAmount;
    
     [SerializeField] GameObject model;
    private void Start()
    {
        sound = GetComponent<SoundPlayer>();
    }
    public void OnTriggerEnter(Collider coll)
    {
        //if (coll.gameObject.tag == "Player")
        //{
            
            Health health = coll.gameObject.GetComponentInParent<Health>();
            if(health.health == health.maxHealth)
            {
                return;
            }
            health.Heal(healAmount);
            Debug.Log("healed");
            GetComponent<Collider>().enabled = false;
            //GetComponent<MeshRenderer>().enabled = false;
            model.SetActive(false);
            //GetComponent<ObjectSpin>().enabled = false;
            sound.Play();
            enabled = false;
            Destroy(gameObject,2);
        //}

    }
}
