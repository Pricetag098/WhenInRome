using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] GameObject objToPool;
    [SerializeField] int poolSize;
    GameObject container;
    List<GameObject> active = new List<GameObject>();
    List<GameObject> inactive = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        container = new GameObject(transform.name + "Pool");
        FillPool();
    }


    void FillPool()
    {
        int objsToSpawn = poolSize - (active.Count + inactive.Count);
        for (int i = 0; i < objsToSpawn; i++)
        {
            GameObject obj = Instantiate(objToPool, container.transform);
            inactive.Add(obj);
            obj.AddComponent<PooledObj>();
            obj.GetComponent<PooledObj>().owner = this;
            obj.SetActive(false);

        }
    }
    public GameObject SpawnObj()
    {
        GameObject obj;
        if(inactive.Count > 0)
        {
            obj = inactive[0];
            inactive.RemoveAt(0);
            active.Add(obj);

        }
        else
        {
            poolSize *= 2;
            FillPool();
            obj = inactive[0];
            active.Add(obj);
        }
        obj.SetActive(true);
        return obj;
    }
    public void DespawnObj(GameObject obj)
    {
        if (active.Contains(obj))
        {
            active.Remove(obj);
            inactive.Add(obj);
            obj.SetActive(false);
        }
    }
}
