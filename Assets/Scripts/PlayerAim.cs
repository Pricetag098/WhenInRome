using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] LayerMask floorLayer = 64;
    [SerializeField] float rotate;
    float debugAngle = 1;

    Vector3 hitPoint;
    [SerializeField] LayerMask targetableLayers;
    [SerializeField] int assistRays = 20;
    /// <summary>
    /// Direction Player is aiming in
    /// </summary>
    public Vector3 aimDir;

    [SerializeField]
    float angleConstant = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        angleConstant = Camera.main.transform.rotation.eulerAngles.x;
        angleConstant = Mathf.Tan(angleConstant * Mathf.Deg2Rad);
        angleConstant = 1/angleConstant;

    }

    // Update is called once per frame
    void Update()
    {
        float y = 0;
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, float.PositiveInfinity,floorLayer))
        {
            hitPoint = hit.point;
            y = hitPoint.y;
            hitPoint.y = 0;
        }
        Vector3 playerPos = transform.position;
        playerPos.y = 0;
        aimDir = (hitPoint - playerPos - Vector3.forward * (transform.position.y - y) * angleConstant).normalized;

        if (rotate > 0)
            transform.forward = Vector3.Slerp(transform.forward,aimDir,rotate * Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        

        Vector3 playerPos = transform.position;
        playerPos.y = 0;
        float aimDist = Vector3.Distance(hitPoint, playerPos);
        DrawVector(aimDir * aimDist);
        Gizmos.DrawLine(transform.position + aimDir * aimDist, transform.position + aimDir * aimDist + Vector3.down * transform.position.y);

        Gizmos.color = Color.red;
        DrawVector(GetAssistedDir(debugAngle) * aimDist);
        Gizmos.color = Color.blue;
        DrawVector((Quaternion.Euler(0, -debugAngle / 2, 0) * aimDir) * aimDist);
        DrawVector((Quaternion.Euler(0,  debugAngle / 2, 0) * aimDir) * aimDist);

    }
    public Vector3 GetAssistedDir(float angle)
    {
        debugAngle = angle;
        List<Vector3> hits = new List<Vector3>();
        float localAngle = angle / assistRays;
        for(int i = -assistRays /2; i< assistRays / 2; i++)
        {
            Vector3 dir = Quaternion.Euler(0, i * localAngle,0) * aimDir;
            RaycastHit hit;
            if(Physics.Raycast(transform.position,dir,out hit,float.PositiveInfinity,targetableLayers))
            {
                if(hit.collider.gameObject.GetComponent<Health>() != null)
                {
                    hits.Add(hit.collider.transform.position);
                }
            }
        }
        if(hits.Count == 0)
        {
            return aimDir;
        }
        Vector3 targetdir = Vector3.zero;
        float bestVal = float.PositiveInfinity;
        foreach(Vector3 hit in hits)
        {
            Vector3 h = hit;
            Vector3 p = transform.position;
            h.y = 0;
            p.y = 0;


            Vector3 dir = (h - p);
            dir.Normalize();
            float dist = Vector3.Distance(h, p);
            //float ang = Vector3.Distance(aimDir, dir);


            float val = Vector3.Angle(aimDir,dir);

            if(val < bestVal)
            {
                targetdir = dir;
                bestVal = val;
            }
        }
        return targetdir;

    }
    void DrawVector(Vector3 vect)
    {
        Gizmos.DrawLine(transform.position, transform.position + vect);
    }
}
