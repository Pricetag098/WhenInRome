using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float timeBetween;
    [SerializeField] LayerMask ground = 1;
    [SerializeField] SoundPlayer defSound,waterSound;
    SoundPlayer player;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player = defSound;
        RaycastHit hit;

        if(Physics.Raycast(transform.position + Vector3.up, Vector3.down,out hit, float.PositiveInfinity, ground))
        {
            FloorType floor = hit.transform.GetComponent<FloorType>();
            if(floor != null)
            {
                switch (floor.type)
                {
                    default:
                        player = defSound;
                        break;
                    case FloorType.Types.water:
                        player = waterSound;
                        break;
                }
            }
        }


        if(rb.velocity != Vector3.zero && timer > timeBetween)
        {
            player.Play();
            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
