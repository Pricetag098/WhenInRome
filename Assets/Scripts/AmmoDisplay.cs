using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{
    public Text text;
    Gun gun;
    public Holster holster;

    // Update is called once per frame
    void Update()
    {
        gun = holster.transform.GetChild(holster.selectedWeapon).GetComponent<Gun>();
        text.text = gun.ammo + " / " + gun.maxAmmo;
    }
}
