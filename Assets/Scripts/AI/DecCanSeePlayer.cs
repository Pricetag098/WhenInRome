using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DistToPlayer", menuName = "Ai/Decision/DistanceToPlayerGreaterThen")]
public class DecCanSeePlayer : TreeNode
{
    public float dist,fov;
    public LayerMask validLayers;
    public TreeNode yes;
    public TreeNode no;

    const int rays = 20;
    public override void Run()
    {
        bool hitPlayer = false;
        float angle = fov / rays;
        int halfRays = rays / 2;
        for(int i = -halfRays; i < halfRays; i++)
        {
            RaycastHit hit;
            if(Physics.Raycast(ai.transform.position,Vector3.forward, out hit,dist, validLayers))
            {
                if (hit.collider.gameObject.GetComponent<PlayerAim>())
                {
                    hitPlayer = true;
                    break;
                }
            }
        }


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
}
