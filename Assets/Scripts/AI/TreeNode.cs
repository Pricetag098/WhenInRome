using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Base class for decision tree based ai
/// </summary>
public class TreeNode : ScriptableObject
{
    protected Ai ai;
    public virtual void Run()
    {

    }
    public virtual void Innit(Ai owner)
    {

    }
    public virtual void Tick()
    {

    }
}
