using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CirclePlayer", menuName = "Ai/Action/CirclePlayer")]
public class ActCirclePlayer : TreeNode
{
    public TreeNode passThrough;
    public float rad;
    public float circleSpeed;

    float randVal;
    public override void Run()
    {
        

        float posX = Mathf.Sin(Time.time * circleSpeed + randVal) * rad;
        float posZ = Mathf.Cos(Time.time * circleSpeed + randVal) * rad;
        posX += ai.player.transform.position.x;
        posZ += ai.player.transform.position.z;

        Vector3 target = new Vector3(posX, ai.transform.position.y, posZ);

        ai.agent.destination = target;
        //ai.agent.speed = circleSpeed;
        ai.agent.stoppingDistance = 0;
        if (passThrough != null)
            passThrough.Run();
    }
    public override void Innit(Ai owner)
    {
        ai = owner;
        randVal = Random.value * 10;
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
