using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    public Room room;
    private void OnTriggerEnter(Collider other)
    {
        room.Enter();
        enabled = false;
        GetComponent<Collider>().enabled = false;
    }
}
