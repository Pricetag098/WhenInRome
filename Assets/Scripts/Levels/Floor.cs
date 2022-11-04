using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    MusicPlayer musicPlayer;
    public Room[] rooms;
    public bool inCombat = false;
    bool wasInCombat = false;
    // Start is called before the first frame update
    void Start()
    {
        musicPlayer = FindObjectOfType<MusicPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        inCombat = false;
        foreach(Room room in rooms)
        {
            if (room.started)
            {
                if (!room.complete)
                {
                    inCombat = true;
                }
            }
        }
        if(inCombat && !wasInCombat)
        {
            OnEnterCombat();
        }
        if(!inCombat && wasInCombat)
        {
            OnExitCombat();
        }
    }

    void OnEnterCombat()
    {
        if(musicPlayer != null)
            musicPlayer.muffler.UnMuffle();
    }
    void OnExitCombat()
    {
        if (musicPlayer != null)
            musicPlayer.muffler.Muffle();
    }
}
