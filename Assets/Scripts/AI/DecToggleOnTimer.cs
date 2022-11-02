using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Toggle", menuName = "Ai/Decision/Toggle")]
public class DecToggleOnTimer : TreeNode
{

    
    public TreeNode left;
    public TreeNode right;
    public float timer;
    float time;
    bool toggle = true;
    public override void Run()
    {
        if(time > timer)
        {
            toggle = !toggle;
            time = 0;
        }


        if (toggle)
        {
            if (left != null)
                left.Run();
        }
        else
        {
            if (right != null)
                right.Run();
        }
        time += Time.deltaTime;
    }
    public override void Innit(Ai owner)
    {
        ai = owner;

        if (left != null)
        {
            left = Instantiate(left);
            left.Innit(owner);
        }
        if (right != null)
        {
            right = Instantiate(right);
            right.Innit(owner);
        }
    }
    public override void Tick()
    {
        if (left != null)
        {

            left.Tick();
        }
        if (left != null)
        {

            left.Tick();
        }
    }
}
