using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRoot", menuName = "Ai/Action/SetNewRoot")]
public class ActNewRoot : TreeNode
{
    public TreeNode newRoot;
    
    public override void Run()
    {
        
        if (newRoot != null)
        {
            ai.root = newRoot;
            newRoot.Run();
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
