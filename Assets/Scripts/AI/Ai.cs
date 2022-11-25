using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Ai : MonoBehaviour
{
    public GameObject player;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Rigidbody rb;
    public TreeNode root;
    public GameObject body;
    public Flash flash;

    [HideInInspector]
    public Health health;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        
        Reset();
        
    }
    public void Reset()
    {
        if (root != null)
        {
            root = Instantiate(root);
            root.Innit(this);
            health.health = health.maxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        root.Run();
        root.Tick();
    }

}
