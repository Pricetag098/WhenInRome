using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlashColour", menuName = "Ai/Animation/FlashColour")]
public class AnimFlashColour : TreeNode
{
    public Color color;
    public float duration;
    public TreeNode passThrough;

    public override void Run()
    {
        if (ai.flash != null)
        {
            ai.flash.FlashColour(color,duration);
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