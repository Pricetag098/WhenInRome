using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoAll", menuName = "Ai/Action/DoAll")]
public class ActDoAll : TreeNode
{
    public List<TreeNode> nodes = new List<TreeNode>();

   

    public override void Run()
    {
        foreach(TreeNode node in nodes)
        {
            node.Run();
        }
    }
    public override void Innit(Ai owner)
    {
        ai = owner;
        
        for(int i = 0; i < nodes.Count; i++)
        {
            nodes[i] = Instantiate(nodes[i]);
            nodes[i].Innit(ai);
        }
    }
    public override void Tick()
    {
        foreach (TreeNode node in nodes)
        {
            node.Tick();
        }
    }
}