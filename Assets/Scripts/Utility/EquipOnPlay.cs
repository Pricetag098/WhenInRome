using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipOnPlay : MonoBehaviour
{
    public List<GameObject> guns = new List<GameObject>();
    public Holster holster;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject gun in guns)
        {
            GameObject newWeapon = Instantiate(gun, holster.transform);
            newWeapon.GetComponent<ObjectPooler>().owner = holster.playerAim.gameObject;
        }
        holster.Equip();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
