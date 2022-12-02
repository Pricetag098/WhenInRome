using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> rooms = new List<GameObject>();
    public GameObject player;
    public WaveDisp waveDisp;
    public int wave =0;
    public float waveWaitTime = 5;

    MuffleMusic muffleMusic;
    Room room;
    // Start is called before the first frame update
    void Start()
    {
        muffleMusic = FindObjectOfType<MuffleMusic>();
    }

    // Update is called once per frame
    void Update()
    {
        if(room == null)
        {
            OnClearWave();
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
        if(room != null)
        Destroy(room.gameObject);
        StartCoroutine("WaitBetweenLevels");
    }
    IEnumerator WaitBetweenLevels()
    {
        if (muffleMusic != null)
            muffleMusic.Muffle();
        yield return new WaitForSeconds(waveWaitTime);
        Spawn();
        wave++;
        if (waveDisp != null)
        {
            waveDisp.UpdateText(wave);
        }
        if(muffleMusic != null)
            muffleMusic.UnMuffle();
    }

}
