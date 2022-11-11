using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    CanvasGroup menu;
    public float fadeTime;
   
    public bool menuOpen;
    // Start is called before the first frame update
    void Start()
    {
        menu = GetComponent<CanvasGroup>();    
        CloseMenu();
    }

    PlayerInputs inputActions;
    InputAction menuInput;
    private void Awake()
    {
        inputActions = new PlayerInputs();
        menuInput = inputActions.Player.OpenMenu;

        menuInput.performed += ToggleMenu;
    }
    private void OnEnable()
    {

        menuInput.Enable();
    }
    private void OnDisable()
    {
        menuInput.Disable();
    }

    void ToggleMenu(InputAction.CallbackContext context)
    {
        if (menuOpen)
        {
            CloseMenu();
        }
        else
        {
            OpenMenu();
        }
    }

    [ContextMenu("Open")]
    public void OpenMenu()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            menuOpen = true;
            //transform.GetChild(0).gameObject.SetActive(true);
            StopCoroutine("FadeOut");
            StartCoroutine("FadeIn");
        }
    }
    [ContextMenu("Close")]
    public void CloseMenu()
    {
        Time.timeScale = 1;
        menuOpen = false;
        //transform.GetChild(0).gameObject.SetActive(false);
        StopCoroutine("FadeIn");
        StartCoroutine("FadeOut");
    }


    IEnumerator FadeIn()
    {
        float fade = menu.alpha;
        while(fade < 1)
        {
            fade = Mathf.MoveTowards(fade, 1, 1/(fadeTime*Time.unscaledDeltaTime));
            menu.alpha = fade;
            yield return new WaitForEndOfFrame();
        }
        
    }
    IEnumerator FadeOut()
    {
        float fade = menu.alpha;
        while (fade > 0)
        {
            fade = Mathf.MoveTowards(fade, 0, 1/(fadeTime * Time.unscaledDeltaTime));
            menu.alpha = fade;
            yield return new WaitForEndOfFrame();
        }
        
    }

}
