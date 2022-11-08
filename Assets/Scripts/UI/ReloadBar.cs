using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadBar : MonoBehaviour
{

    Gun gun;
    public Holster holster;
    Image bar;

    private void Start()
    {
        bar = GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        if(holster.transform.childCount == 0)
        {
            bar.enabled = false;
            return;
        }
        gun = holster.transform.GetChild(holster.selectedWeapon).GetComponent<Gun>();
        bar.fillAmount = gun.reloadProgress;
        bar.enabled = bar.fillAmount != 1f;
    }
}
