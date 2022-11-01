using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public LevelLoader LevelLoader;
    private void OnTriggerEnter(Collider other)
    {
        LevelLoader.Load();
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
