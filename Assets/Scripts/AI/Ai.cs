using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Ai : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent agent;

    public TreeNode firstNode;
    // Start is called before the first frame update
    void Start()
    {
        if(firstNode != null)
        {
            firstNode = Instantiate(firstNode);
            firstNode.Innit(this);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        firstNode.Run();
        firstNode.Tick();
    }

}
