using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health", menuName = "Ai/Decision/Health")]
public class DecHeathLessThan : TreeNode
{
    public float val;
    public TreeNode greater;
    public TreeNode less;

    public override void Run()
    {
        if (ai.GetComponent<Health>().health > val)
        {
            if (greater != null)
                greater.Run();
        }
        else
        {
            if (less != null)
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
