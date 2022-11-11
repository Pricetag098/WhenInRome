using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Holster : MonoBehaviour
{
    public PlayerAim playerAim;
    public int selectedWeapon;
    [HideInInspector]
    public float VertOffset;
    int last = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool swapped = false;
    PlayerInputs inputActions;
    InputAction swap;
    private void Awake()
    {
        inputActions = new PlayerInputs();
        swap = inputActions.Player.ChangeWeapon;
    }
    private void OnEnable()
    {
        
        swap.Enable();
    }
    private void OnDisable()
    {
        swap.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        VertOffset = playerAim.offset;

        float scrollVal = swap.ReadValue<float>();
        //change the selected weapon
        if(scrollVal > 0 && !swapped)
        {
            selectedWeapon++;
            if(selectedWeapon > transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            swapped = true;
        }
        if (scrollVal < 0 && !swapped)
        {
            selectedWeapon--;
            if(selectedWeapon < 0)
            {
                selectedWeapon = transform.childCount-1;
            }
            swapped = true;
        }
        if(scrollVal == 0)
        {
            swapped = false;
        }
        if(selectedWeapon != last)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(i == selectedWeapon);
                if (i == selectedWeapon)
                {
                    Gun gun = transform.GetChild(i).GetComponent<Gun>();
                    gun.Equip();
                }

            }
        }
        last = selectedWeapon;
        
    }
    public void Equip()
    {
        last = -1;
        selectedWeapon = transform.childCount-1;
    }
}
