using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public Door door;
    SoundPlayer sound;
    public GameObject weapon;
    
    public Holster holster;
    
    
    private void Start()
    {
        sound = GetComponent<SoundPlayer>();
        if(door != null)
        door.Close();

    }

    [ContextMenu("Test")]
    public void SpawnGun()
    {
        GameObject newWeapon = Instantiate(weapon, holster.transform);
        newWeapon.GetComponent<ObjectPooler>().owner = holster.playerAim.gameObject;
        if (door != null)
            door.Open();
    }
    public void OnTriggerStay(Collider coll)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnGun();
            GetComponent<Collider>().enabled = false;
            //GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            GetComponent<ObjectSpin>().enabled = false;
            sound.Play();
            holster.Equip();
            enabled = false;
        }
        
    }
}
