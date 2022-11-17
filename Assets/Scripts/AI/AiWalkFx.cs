using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AiWalkFx : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    Ai ai;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float timeBetween;
    [SerializeField] LayerMask ground = 1;
    [SerializeField] SoundPlayer defSound, waterSound;
    SoundPlayer player;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<Ai>();
    }

    // Update is called once per frame
    void Update()
    {
        player = defSound;
        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, float.PositiveInfinity, ground))
        {
            FloorType floor = hit.transform.GetComponent<FloorType>();
            if (floor != null)
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


        if (rb.velocity + agent.velocity != Vector3.zero && timer > timeBetween)
        {
            player.Play();
            timer = 0;
        }

        timer += Time.deltaTime;

        ai.animator.SetFloat("Vel", FindVelRelativeToLook().y);
        //Debug.Log(FindVelRelativeToLook());
    }

    public Vector2 FindVelRelativeToLook()
    {
        Vector3 vel = rb.velocity + agent.velocity;
        //Debug.Log(vel);
        float lookAngle = ai.body.transform.eulerAngles.y;
        //Debug.Log(lookAngle);
        float moveAngle = Mathf.Atan2(vel.x, vel.z) * Mathf.Rad2Deg;
        //Debug.Log(moveAngle);
        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;
        
        float magnitue = rb.velocity.magnitude + agent.velocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);
        //Debug.Log(u);
        return new Vector2(xMag, yMag);
    }
}
