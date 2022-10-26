using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Ai : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent agent;
    public Rigidbody rb;
    public TreeNode root;
    // Start is called before the first frame update
    void Start()
    {
        if(root != null)
        {
            root = Instantiate(root);
            root.Innit(this);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        root.Run();
        root.Tick();
    }

}
