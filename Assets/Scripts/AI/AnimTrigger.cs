using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trigger", menuName = "Ai/Animation/Trigger")]
public class AnimTrigger : TreeNode
{
    public string triggerName = "";
    public TreeNode passThrough;
    
    public override void Run()
    {
        if(ai.animator != null)
        {
            ai.animator.SetTrigger(triggerName);
        }
        if (passThrough != null)
        {
            
            passThrough.Run();
        }

    }
    public override void Innit(Ai owner)
    {
        ai = owner;
        
        if (passThrough != null)
        {
            passThrough = Instantiate(passThrough);
            passThrough.Innit(owner);
        }
    }
    public override void Tick()
    {
        if (passThrough != null)
        {
            passThrough.Tick();
        }
    }
}