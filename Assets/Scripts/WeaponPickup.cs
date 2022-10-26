using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weapon;
    public GameObject player;
    public Transform holster;
    [ContextMenu("Test")]
    public void SpawnGun()
    {
        GameObject newWeapon = Instantiate(weapon, holster);
        newWeapon.GetComponent<ObjectPooler>().owner = player;

    }

}
