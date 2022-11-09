using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public Door door;
    SoundPlayer sound;
    public GameObject weapon;
    public GameObject weaponHint;
    
    public Holster holster;
    //float MaxPickUPDist = 5;
    //bool isholding = false;
    bool insideCol = false;
    
    private void Start()
    {
        sound = GetComponent<SoundPlayer>();
        weaponHint.SetActive(false);
    }

    [ContextMenu("Test")]
    public void SpawnGun()
    {
        GameObject newWeapon = Instantiate(weapon, holster.transform);
        newWeapon.GetComponent<ObjectPooler>().owner = holster.playerAim.gameObject;
        door.Open();
    }

    private void Update()
    {
        if (insideCol)
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
                weaponHint.SetActive(false);
            }
        }
    }
    
    public void OnTriggerEnter(Collider other)
    {
        EnbleHint();
        insideCol = true;
    }
    public void OnTriggerExit(Collider other)
    {
        DisableHint();
        insideCol = false;
    }
    public void EnbleHint()
    {
        weaponHint.SetActive(true);
    }
    public void DisableHint()
    {
        weaponHint.SetActive(false);
    }
}
