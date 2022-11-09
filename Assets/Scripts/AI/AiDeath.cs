using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AiDeath : MonoBehaviour
{
    Ai ai;
    NavMeshAgent agent;
    Health health;
    MeshRenderer mr;
    Collider col;
    public SoundPlayer deathSound;

    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<Ai>();
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
        mr = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
    }
    public void Die()
    {
        ai.enabled = false;
        health.enabled = false;
        mr.enabled = false;
        col.enabled = false;
        deathSound.Play();
    }

}
