using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Colliding", menuName = "Ai/Decision/Colliding")]
public class DecColliding : TreeNode
{
    
    public LayerMask validLayers;
    public TreeNode yes;
    public TreeNode no;
    public float rad;
    
    public override void Run()
    {
        bool colliding = Physics.CheckSphere(ai.transform.position,rad,validLayers);
        
        
        if (colliding)
        {
            if (yes != null)
                yes.Run();
        }
        else
        {
            if (no != null)
                no.Run();
        }
    }
    public override void Innit(Ai owner)
    {
        ai = owner;
        
        if (yes != null)
        {
            yes = Instantiate(yes);
            yes.Innit(owner);
        }
        if (no != null)
        {
            no = Instantiate(no);
            no.Innit(owner);
        }
    }
    public override void Tick()
    {
        if (yes != null)
        {

            yes.Tick();
        }
        if (no != null)
        {

            no.Tick();
        }
    }
}
