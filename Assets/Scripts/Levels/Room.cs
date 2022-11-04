using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<GameObject> enemys = new List<GameObject>();
    public bool complete = false;
    bool started = false;
    public Door enterance,exit;
    // Start is called before the first frame update
    void Start()
    {
        
        foreach(GameObject enemy in enemys)
        {
            enemy.SetActive(false);
        }    
        
    }

    public void Enter()
    {
        if (!complete)
        {
            foreach (GameObject enemy in enemys)
            {
                enemy.SetActive(true);
            }
            if(enterance != null)
            enterance.Close();
            if(exit != null)
            exit.Close();
            started = true;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!complete && started)
        {
            bool allDead = true;
            foreach (GameObject enemy in enemys)
            {
                if (enemy.GetComponent<MeshRenderer>().enabled)
                {
                    allDead = false;
                }
            }
            if (allDead)
            {
                complete = true;
                if(exit != null)
                exit.Open();
            }
        }
    }
}
