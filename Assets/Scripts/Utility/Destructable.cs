using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    GameObject pot;
    ParticleSystem particle;
    Collider col;
    SoundPlayer audioSource;
    [SerializeField] GameObject prefab;
    [Range(0,1)]
    [SerializeField] float spawnOdds;
    [SerializeField] Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        pot = transform.GetChild(0).gameObject;
        particle = GetComponent<ParticleSystem>();
        col = GetComponent<Collider>();
        audioSource = GetComponent<SoundPlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        pot.SetActive(false);
        if(particle != null)
        particle.Play();
        if(audioSource != null)
        audioSource.Play();
        col.enabled = false;
        enabled = false;
        if(Random.value <= spawnOdds && prefab != null)
        {
            GameObject go = Instantiate(prefab,transform.position + offset, Quaternion.identity);
        }
        if(GetComponent<AiDeath>() != null)
        {
            GetComponent<AiDeath>().Die();
        }
    }
}
