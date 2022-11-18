using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticlesOnHit : MonoBehaviour
{

    public ObjectPooler hitWall;
    public ObjectPooler hitSkelleton;
    public ObjectPooler hitSatyr;
    public ObjectPooler hitMinotaur;
    public void Hit(HitData data)
    {
        if(data.hitObject.GetComponent<TargetType>() == null)
        {
            Spawn(hitWall, data);
        }
        else
        {
            switch (data.hitObject.GetComponent<TargetType>().type)
            {
                case TargetType.Type.skeleton:
                    Spawn(hitSkelleton, data);
                    break;
                case TargetType.Type.satyr:
                    Spawn(hitSatyr, data);
                    break ;
                case TargetType.Type.minotaur:
                    Spawn(hitMinotaur, data);
                    break;
                default:
                    Spawn(hitWall, data);
                    break;
            }
        }
    }
    void Spawn(ObjectPooler pooler,HitData data)
    {
        GameObject go = pooler.SpawnObj();
        go.transform.position = data.position;
        go.transform.forward = data.dir;
        go.GetComponent<FxPlayer>().Play();
        StartCoroutine("DespawnLater",go);
    }

    IEnumerator DespawnLater(GameObject obj)
    {
        yield return new WaitForSeconds(3);
        obj.GetComponent<PooledObj>().Despawn();
    }
}
