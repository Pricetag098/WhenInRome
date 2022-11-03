using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SoundPlayer))]
public class Exit : MonoBehaviour
{
    public LevelLoader LevelLoader;
    SoundPlayer sound;

    public void Start()
    {
        sound = GetComponent<SoundPlayer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        LevelLoader.Load();
        sound.Play();
        PlayerData.relavtivePos = other.transform.position - transform.position;
        //PlayerData.relavtivePos.y = 0;
        PlayerMove playerMove = other.GetComponent<PlayerMove>();
        if (playerMove == null)
        {
            playerMove = other.GetComponentInParent<PlayerMove>();
        }
        playerMove.enabled = false;
        playerMove.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
