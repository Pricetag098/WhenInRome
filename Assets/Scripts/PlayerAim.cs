using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] LayerMask floorLayer = 64;
    [SerializeField] float rotate;

    Vector3 hitPoint;

    /// <summary>
    /// Direction Player is aiming in
    /// </summary>
    public Vector3 aimDir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, float.PositiveInfinity,floorLayer))
        {
            hitPoint = hit.point;
            hitPoint.y = 0;
        }
        Vector3 playerPos = transform.position;
        playerPos.y = 0;
        aimDir = (hitPoint - playerPos - Vector3.forward * transform.position.y).normalized;

        if (rotate > 0)
            transform.forward = Vector3.Slerp(transform.forward,aimDir,rotate * Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        

        Vector3 playerPos = transform.position;
        playerPos.y = 0;
        Gizmos.DrawLine(transform.position, transform.position + aimDir * Vector3.Distance(hitPoint, playerPos));
        Gizmos.DrawLine(transform.position + aimDir * Vector3.Distance(hitPoint, playerPos), transform.position + aimDir * Vector3.Distance(hitPoint, playerPos) + Vector3.down * transform.position.y);
    }
}
