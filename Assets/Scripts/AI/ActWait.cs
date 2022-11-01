using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wait", menuName = "Ai/Action/Wait")]
public class ActWait : TreeNode
{
    TreeNode root;
    public float time;
    float timer;
    TreeNode newRoot;
    public override void Run()
    {
        if(timer > time)
        {
            root = ai.root;
            timer = 0;
        }


        
        
        

    }
    public override void Innit(Ai owner)
    {
        ai = owner;
        if (newRoot != null)
        {
            newRoot = Instantiate(newRoot);
            newRoot.Innit(owner);
        }
    }
    public override void Tick()
    {
        if (newRoot != null)
        {
            newRoot.Tick();
        }
    }
}
