using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CanSeePlayer", menuName = "Ai/Decision/CanSeePlayer")]
public class DecCanSeePlayer : TreeNode
{
    public float dist,fov;
    public LayerMask validLayers;
    public TreeNode yes;
    public TreeNode no;

    public int rays = 20;
    public override void Run()
    {
        bool hitPlayer = false;
        float angle = fov / rays;
        int halfRays = rays / 2;
        for(int i = -halfRays; i < halfRays; i++)
        {
            RaycastHit hit;
            if(Physics.Raycast(ai.transform.position,Quaternion.Euler(0,i*angle,0) * ai.transform.forward, out hit,dist, validLayers))
            {
                if (hit.collider.gameObject.GetComponent<PlayerAim>())
                {
                    hitPlayer = true;
                    break;
                }
            }
        }
        Debug.Log(hitPlayer);

        if (hitPlayer)
        {
            if (yes != null)
                yes.Run();
        }
        else
        {
            if (no != null)
                no.Run();
        }
    }
    public override void Innit(Ai owner)
    {
        ai = owner;
        if (yes != null)
        {
            yes = Instantiate(yes);
            yes.Innit(owner);
        }
        if (no != null)
        {
            no = Instantiate(no);
            no.Innit(owner);
        }
    }
    public override void Tick()
    {
        if (yes != null)
        {
            
            yes.Tick();
        }
        if (yes != null)
        {
            
            yes.Tick();
        }
    }
}
