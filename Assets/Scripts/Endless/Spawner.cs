using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> rooms = new List<GameObject>();
    public GameObject player;
    public int wave =0;
    Room room;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(room == null)
        {
            Spawn();
        }
        else
        {
            if (!room.started)
            {
                room.Enter();
            }
            if (room.complete)
            {
                OnClearWave();
            }
        }
    }
    public void Spawn()
    {
        int i = Random.Range(0, rooms.Count);
        GameObject roomGo = Instantiate(rooms[i],transform);
        room = roomGo.GetComponent<Room>();
        foreach(GameObject enemy in room.enemys)
        {
            enemy.GetComponent<Ai>().player = player;
        }
        //room.Enter();
    }
    void OnClearWave()
    {
        Destroy(room);
        Spawn();
        wave++;
    }
}
