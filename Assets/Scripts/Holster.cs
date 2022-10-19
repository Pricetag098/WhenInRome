using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holster : MonoBehaviour
{
    public PlayerAim playerAim;
    int selectedWeapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //change the selected weapon
        if(Input.mouseScrollDelta.y > 0)
        {
            selectedWeapon++;
            if(selectedWeapon > transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            selectedWeapon--;
            if(selectedWeapon < 0)
            {
                selectedWeapon = transform.childCount-1;
            }
        }
        for(int i = 0; i< transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == selectedWeapon);
            
        }
    }
}
