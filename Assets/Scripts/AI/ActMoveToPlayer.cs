using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveToPlayer", menuName = "Ai/Action/MoveToPlayer")]
public class ActMoveToPlayer : TreeNode
{
    public TreeNode passThrough;
    public float stoppingDist;
    public float speed;
    public override void Run()
    {
        ai.agent.destination = ai.player.transform.position;
        ai.agent.speed = speed;
        ai.agent.stoppingDistance = stoppingDist;
        if(passThrough != null)
            passThrough.Run();
    }
    public override void Innit(Ai owner)
    {
        ai = owner;
        if(passThrough != null)
        {
            passThrough = Instantiate(passThrough);
            passThrough.Innit(owner);
        }
    }
}
