using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public Door door;
    SoundPlayer sound;
    public GameObject weapon;
    
    public Holster holster;
    float MaxPickUPDist = 5;
    bool isholding = false;
    
    private void Start()
    {
        sound = GetComponent<SoundPlayer>();

    }

    [ContextMenu("Test")]
    public void SpawnGun()
    {
        GameObject newWeapon = Instantiate(weapon, holster.transform);
        newWeapon.GetComponent<ObjectPooler>().owner = holster.playerAim.gameObject;
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
