using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveAway", menuName = "Ai/Action/MoveAway")]
public class ActMoveAwayFromPlayer : TreeNode
{
    public TreeNode passThrough;

    public float speed;

    
    public override void Run()
    {


        Vector3 dir = ai.transform.position - ai.player.transform.position;
        dir.Normalize();
        ai.agent.velocity = dir * speed;

        
        if (passThrough != null)
            passThrough.Run();
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
