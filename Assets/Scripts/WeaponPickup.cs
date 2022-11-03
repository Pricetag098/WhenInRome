using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    SoundPlayer sound;
    public GameObject weapon;
    public GameObject player;
    public Transform holster;
    float MaxPickUPDist = 5;
    bool isholding = false;
    [ContextMenu("Test")]
    private void Start()
    {
        sound = GetComponent<SoundPlayer>();
    }
    public void SpawnGun()
    {
        GameObject newWeapon = Instantiate(weapon, holster);
        newWeapon.GetComponent<ObjectPooler>().owner = player;

    }
    public void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            SpawnGun();
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<ObjectSpin>().enabled = false;
            sound.Play();
            enabled = false;
        }

    }
}
