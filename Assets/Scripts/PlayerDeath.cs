using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerDeath : MonoBehaviour
{
    PlayerMove move;
    LevelLoader levelLoader;
    Rigidbody rb;
    private void Start()
    {
        move = GetComponent<PlayerMove>();
        levelLoader = GetComponent<LevelLoader>();
        rb = GetComponent<Rigidbody>();
        
    }

    public void Die()
    {
        enabled = false;
        move.enabled = false;
        levelLoader.Reload();
        rb.velocity = Vector3.zero;
    }

}
