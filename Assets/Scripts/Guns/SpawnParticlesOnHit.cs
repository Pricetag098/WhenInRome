using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticlesOnHit : MonoBehaviour
{

    public ObjectPooler hitWall;
    public void Hit(HitData data)
    {
        if(data.damage == 0)
        {
            GameObject go = hitWall.SpawnObj();
            go.transform.position = data.position;
            go.transform.forward = data.dir;
            go.GetComponent<FxPlayer>().Play();
        }
    }
}
