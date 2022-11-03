using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<GameObject> enemys = new List<GameObject>();
    public bool complete = false;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject enemy in enemys)
        {
            enemy.SetActive(false);
        }    
    }

    public void Spawn()
    {
        //if (complete == true) { return; }
        foreach (GameObject enemy in enemys)
        {
            enemy.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!complete)
        {
            bool allDead = true;
            foreach (GameObject enemy in enemys)
            {
                if (enemy.activeSelf)
                {
                    allDead = false;
                }
            }
            if (allDead)
            {
                complete = true;
            }
        }
    }
}
