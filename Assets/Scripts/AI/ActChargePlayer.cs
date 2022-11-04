using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChargePlayer", menuName = "Ai/Action/ChargePlayer")]
public class ActChargePlayer : TreeNode
{
    public TreeNode passthrough;

    Vector3 dir = Vector3.zero;
    public float vel;
    public override void Run()
    {
        ai.rb.isKinematic = false;
        ai.agent.enabled = false;
        if(dir == Vector3.zero)
        {
            dir = ai.player.transform.position - ai.transform.position;
            dir.y = 0;
            dir.Normalize();
        }
        ai.rb.velocity = dir * vel;
        if (passthrough != null)
        {
            ai.root = passthrough;
            passthrough.Run();
        }

    }
    public override void Innit(Ai owner)
    {
        ai = owner;
        if (passthrough != null)
        {
            passthrough = Instantiate(passthrough);
            passthrough.Innit(owner);
        }
    }
    public override void Tick()
    {
        if (passthrough != null)
        {
            passthrough.Tick();
        }
    }
}
