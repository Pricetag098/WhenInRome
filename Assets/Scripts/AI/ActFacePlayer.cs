using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FacePlayer", menuName = "Ai/Action/FacePlayer")]
public class ActFacePlayer : TreeNode
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
