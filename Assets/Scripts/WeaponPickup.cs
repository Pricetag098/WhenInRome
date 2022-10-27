using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weapon;
    public GameObject player;
    public Transform holster;
    float MaxPickUPDist = 5;
    bool isholding = false;
    [ContextMenu("Test")]
    public void SpawnGun()
    {
        GameObject newWeapon = Instantiate(weapon, holster);
        newWeapon.GetComponent<ObjectPooler>().owner = player;

    }
    public void PickUpGun()
    {

    }
    public void OnTriggerStay(Collider coll)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnGun();
            Destroy(gameObject);
        }

    }
}
