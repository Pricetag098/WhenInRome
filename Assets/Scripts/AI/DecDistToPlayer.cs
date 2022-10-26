using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DistToPlayer",menuName = "Ai/Decision/DistanceToPlayerGreaterThen")]
public class DecDistToPlayer : TreeNode
{
    public float dist;
    public TreeNode greater;
    public TreeNode less;

    public override void Run()
    {
        if (Vector3.Distance(ai.transform.position, ai.player.transform.position) > dist)
        {
            if(greater != null)
            greater.Run();
        }
        else
        {
            if(less != null)
            less.Run();
        }
    }
    public override void Innit(Ai owner)
    {
        ai = owner;
        if (greater != null)
        {
            greater = Instantiate(greater);
            greater.Innit(owner);
        }
        if (less != null)
        {
            less = Instantiate(less);
            less.Innit(owner);
        }
    }
    public override void Tick()
    {
        if (greater != null)
        {
            greater.Tick();
        }
        if (less != null)
        {
            less.Tick();
        }
    }
}
