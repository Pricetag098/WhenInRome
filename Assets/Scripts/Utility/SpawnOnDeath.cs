using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDeath : MonoBehaviour
{
    public GameObject[] objects;
    public bool spawnRandom;
    public float spawnRad;
    public void Run()
    {
        if(objects.Length == 0)
        {
            return;
        }
        if (spawnRandom)
        {
            int rand = Random.Range(0, objects.Length);
            GameObject spawn = Instantiate(objects[rand]);
            spawn.transform.position = transform.position;
        }
        else
        {
            foreach(GameObject obj in objects)
            {
                GameObject spawn = Instantiate(obj);

                
                Vector3 pos = Random.insideUnitSphere*spawnRad;
                pos.y = 0;
                
                spawn.transform.position = transform.position + pos;
            }
        }
    }
}
