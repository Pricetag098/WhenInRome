using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public int amountToSpawn;
    public GameObject player;

    List<GameObject> spawns = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {

    }

    void SpawnObj()
    {
        GameObject obj = Instantiate(enemy,transform);
        obj.GetComponent<Ai>().player = player;
        spawns.Add(obj);
    }
}
