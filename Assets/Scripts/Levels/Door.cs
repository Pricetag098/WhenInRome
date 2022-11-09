using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    WeaponPickup pickup;
    Collider col;
    [SerializeField] SoundPlayer open,close;
    [SerializeField] Animator animator;
    [SerializeField] bool startOpen = true; 
    private void Start()
    {
        col = GetComponent<Collider>();
        if (startOpen)
        {
            col.enabled = false;
            animator.SetBool("Open",true);
        }
        else
        {
            col.enabled = true;
            animator.SetBool("Open", false);
        }
        
    }
    [ContextMenu("Open")]
    public void Open()
    {
        col.enabled = false;
        animator.SetBool("Open", true);
        open.Play();
    }
    [ContextMenu("Close")]
    public void Close()
    {
        col.enabled=true;
        animator.SetBool("Open", false);
        close.Play();
    }
}
