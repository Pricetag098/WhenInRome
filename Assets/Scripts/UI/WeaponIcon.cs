using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIcon : MonoBehaviour
{
    //GameObject[] icon;
    //public int selectedIcon = 0;
    Gun gun;
    public Holster holster;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (holster.transform.childCount == 0)
        {
            image.enabled = false;
            return;
        }
        image.enabled = true;
        gun = holster.transform.GetChild(holster.selectedWeapon).GetComponent<Gun>();
        image.sprite = gun.icon;
    }
}
