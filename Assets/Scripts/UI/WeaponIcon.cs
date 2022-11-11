using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIcon : MonoBehaviour
{
    GameObject[] icon;
    public int selectedIcon = 0;
    Gun gun;
    public Holster holster;

    private void Start()
    {
        icon = GetComponent<GameObject[]>();
    }

    // Update is called once per frame
    void Update()
    {
        if (holster.transform.childCount == 0)
        {
          icon[selectedIcon].SetActive (false);


        }
        //gun = holster.transform.GetChild(holster.selectedWeapon).GetComponent<Gun>();
    }
}
