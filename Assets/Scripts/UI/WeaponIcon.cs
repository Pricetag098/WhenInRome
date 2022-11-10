using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIcon : MonoBehaviour
{
    Image image;
    Gun gun;
    public Holster holster;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (holster.transform.childCount == 0)
        {
            //image.
        }

    }
}
