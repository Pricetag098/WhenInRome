using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerDeath : MonoBehaviour
{
    PlayerMove move;
    LevelLoader levelLoader;
    Rigidbody rigidbody;
    private void Start()
    {
        move = GetComponent<PlayerMove>();
        levelLoader = GetComponent<LevelLoader>();
        rigidbody = GetComponent<Rigidbody>();
        levelLoader.level = SceneManager.GetActiveScene().buildIndex-1;
    }

    public void Die()
    {
        move.enabled = false;
        levelLoader.Load();
        rigidbody.velocity = Vector3.zero;
    }

}
