using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform player;
    [Range(0,1)]
    public float followPercent = .2f;
    public float followRate = 5;
    
    [SerializeField] Vector3 offset;
    PlayerAim playerAim;
    Camera cam;
    Vector3 idealVect;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        cam = GetComponentInChildren<Camera>();
        if(player != null)
        playerAim = player.GetComponent<PlayerAim>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            Vector3 aimPoint = playerAim.hitPoint;
            aimPoint.y = transform.position.y;
            Vector3 vectToAim = playerAim.aimDir * (Vector3.Distance(transform.position, aimPoint))* followPercent;
            idealVect = player.position + offset + vectToAim;
            
            

        }
        
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.Slerp(transform.position, idealVect, followRate * Time.deltaTime);
    }
}
