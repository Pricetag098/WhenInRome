using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{
    TextMeshProUGUI text;
    Gun gun;
    public Holster holster;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(holster.transform.childCount == 0)
        {
            text.text = "";
            return;
        }
        gun = holster.transform.GetChild(holster.selectedWeapon).GetComponent<Gun>();
        
        text.text = gun.ammo + " / " + gun.maxAmmo;
    }
}
