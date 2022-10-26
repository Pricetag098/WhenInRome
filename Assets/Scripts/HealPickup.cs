using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickup : MonoBehaviour
{
    
    public float healAmount;
    public void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<Health>().Heal(healAmount);
            Debug.Log("healed");
            Destroy(gameObject);
        }

    }
}
