using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    GameObject pot;
    ParticleSystem particle;
    Collider col;

    // Start is called before the first frame update
    void Start()
    {
        pot = transform.GetChild(0).gameObject;
        particle = GetComponent<ParticleSystem>();
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        pot.SetActive(false);
        particle.Play();
        col.enabled = false;
        enabled = false;
    }
}
