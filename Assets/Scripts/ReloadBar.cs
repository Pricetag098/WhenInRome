using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadBar : MonoBehaviour
{

    Gun gun;
    public Holster holster;
    public Image bar;

    // Update is called once per frame
    void Update()
    {
        gun = holster.transform.GetChild(holster.selectedWeapon).GetComponent<Gun>();
        bar.fillAmount = gun.reloadProgress;
        bar.enabled = bar.fillAmount != 1f;
    }
}
