using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlashColourOnce", menuName = "Ai/Animation/FlashColourOnce")]
public class AnimFlashColourOnce : TreeNode
{
    public Color color;
    public float duration;
    public TreeNode passThrough;
    bool flashed = false;
    public override void Run()
    {
        if (ai.flash != null && !flashed)
        {
            ai.flash.FlashColour(color, duration);
            flashed = true;
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