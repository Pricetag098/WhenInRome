using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endless : MonoBehaviour
{
    public Spawner spawner;
    public GameObject deathScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDeath()
    {

    }

    void SaveScore()
    {

    }
    void Load()
    {

    }
    private void OnDestroy()
    {
        SaveScore();
    }
}
