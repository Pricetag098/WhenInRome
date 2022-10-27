using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Die", menuName = "Ai/Action/Die")]
public class ActDie : TreeNode
{
    public TreeNode passThrough;
    Health health;
    public override void Run()
    {
        health.TakeDmg(health.maxHealth * 2);
        if (passThrough != null)
        {
            ai.root = passThrough;
            passThrough.Run();
        }

    }
    public override void Innit(Ai owner)
    {
        ai = owner;
        health = ai.GetComponent<Health>();
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