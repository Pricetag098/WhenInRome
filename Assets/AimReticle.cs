using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class AimReticle : MonoBehaviour
{
    
    public PlayerAim pAim;

    PlayerInputs inputActions;
    InputAction aim;
    public float contAimScale;
    public float angleConstant = 0;
    Image img;
    private void Awake()
    {
        inputActions = new PlayerInputs();
        aim = inputActions.Player.Look;

    }
    public void OnEnable()
    {

        aim.Enable();
    }
    public void OnDisable()
    {
        aim.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        angleConstant = Camera.main.transform.rotation.eulerAngles.x;
        angleConstant = Mathf.Tan(angleConstant * Mathf.Deg2Rad);
        //angleConstant = 1 / angleConstant;
    }

    // Update is called once per frame
    void Update()
    {
        switch (pAim.inputType) 
        {
            case PlayerAim.InputTypes.mouse:
                if (Mouse.current != null)
                    transform.position = Mouse.current.position.ReadValue();
                img.enabled = Time.timeScale != 0;
                break;
            case PlayerAim.InputTypes.gamepad:
                Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2, 0);
                Vector3 aimVal = (Vector3)aim.ReadValue<Vector2>();
                aimVal.y = aimVal.y * angleConstant;
                Vector3 pos = center + aimVal * contAimScale;
                
                transform.position = pos;
                img.enabled = transform.position != center && Time.timeScale != 0;
                
                break;
        }
    }
}
