using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(fileName = "MoveRandom", menuName = "Ai/Action/MoveRandom")]
public class ActMoveToRandomSpot : TreeNode
{
    public TreeNode passThrough;

    public float speed;
    public float roamDist;
    Vector3 target;

    public override void Run()
    {
        target.y = ai.transform.position.y;
        if (Vector3.Distance(ai.transform.position,target) <= 0.1f || Vector3.Distance(ai.transform.position, target) > roamDist)
        {
            GetNewTarget(0,10);
        }
        ai.agent.destination = target;
        ai.agent.speed = speed;
        


        if (passThrough != null)
            passThrough.Run();
    }
    public override void Innit(Ai owner)
    {
        ai = owner;
        GetNewTarget(0,10);
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

    void GetNewTarget(int depth,int maxDepth)
    {
        Vector2 randPos = Random.insideUnitCircle * roamDist;
        target.x = randPos.x;
        target.z = randPos.y;
        target += ai.transform.position;
        target.y = ai.transform.position.y;
        NavMeshPath path = new NavMeshPath();
        if(depth > maxDepth)
        {
            target = ai.transform.position;
            return;
        }
        if (!ai.agent.CalculatePath(target, path))
        {
            depth++;
            GetNewTarget(depth,maxDepth);
        }

    }
}
