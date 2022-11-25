using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TriggerOnce", menuName = "Ai/Animation/TriggerOnce")]
public class AnimTriggerOnce : TreeNode
{
    public string triggerName = "";
    public TreeNode passThrough;
    bool triggered = false;

    public override void Run()
    {
        if (ai.animator != null)
        {
            if (!triggered)
            {
                triggered = true;
                ai.animator.SetTrigger(triggerName);
            }
            
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