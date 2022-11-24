using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AiDeath : MonoBehaviour
{
    Ai ai;
    NavMeshAgent agent;
    Health health;
    public GameObject dropShadow;
    //MeshRenderer mr;
    Collider col;
    Indicator ind;
    public SoundPlayer deathSound;
    public List<Dissolver> dissolvers = new List<Dissolver>();
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<Ai>();
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
        //mr = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
        ind = GetComponent<Indicator>();
        rb = GetComponent<Rigidbody>();
    }
    public void Die()
    {
        ai.enabled = false;
        if(rb != null)
        rb.isKinematic = true;
        if(ai.animator != null)
        ai.animator.SetTrigger("Die");
        agent.enabled = false;
        health.enabled = false;
        //mr.enabled = false;
        if(col)
        col.enabled = false;        
        if(ind != null)
        ind.enabled = false;
        //ind.Arrow = null;
        //ind.Icon = null;
        if(deathSound != null)
        deathSound.Play();
        if(dropShadow != null)
        dropShadow.SetActive(false);
        foreach(Dissolver disolver in dissolvers)
        {
            disolver.Dissolve();
        }
       
    }

}
