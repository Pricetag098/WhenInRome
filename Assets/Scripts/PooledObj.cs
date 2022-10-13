using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObj : MonoBehaviour
{
    public ObjectPooler owner;
    public void Despawn()
    {
        owner.DespawnObj(gameObject);
    }
}
