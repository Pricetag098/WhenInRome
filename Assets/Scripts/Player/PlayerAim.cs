using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
public class PlayerAim : MonoBehaviour
{
    public enum InputTypes
    {
        mouse,
        gamepad
    }
    public InputTypes inputType;

    [SerializeField] LayerMask floorLayer = 64;
    [SerializeField] float rotate;
    public float offset = 0.75f;
    float debugAngle = 1;



    public Vector3 hitPoint;
    [SerializeField] LayerMask targetableLayers;
    [SerializeField] int assistRays = 20;

    
    /// <summary>
    /// Direction Player is aiming in
    /// </summary>
    public Vector3 aimDir;

    
    float angleConstant = 1;

    PlayerMove playerMove;
    PlayerInputs inputActions;
    InputAction aim;
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
        angleConstant = Camera.main.transform.rotation.eulerAngles.x;
        angleConstant = Mathf.Tan(angleConstant * Mathf.Deg2Rad);
        angleConstant = 1/angleConstant;
        playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 contAimDir = aim.ReadValue<Vector2>();
        inputType = InputTypes.mouse;
        if(Gamepad.current != null)
        {
            if(Gamepad.current.lastUpdateTime > Mouse.current.lastUpdateTime)
            {
                inputType = InputTypes.gamepad;
            }
        }
        if(inputType == InputTypes.mouse) 
        {
            
            float y = 0;
            RaycastHit hit;
            //Cursor.visible = true;
            Vector2 mPos = Mouse.current.position.ReadValue();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(mPos), out hit, float.PositiveInfinity, floorLayer))
            {
                hitPoint = hit.point;
                y = hitPoint.y;
                hitPoint.y = 0;
            }
            Vector3 playerPos = transform.position;
            playerPos.y = 0;
            aimDir = (hitPoint - playerPos - Vector3.forward * ((transform.position.y + offset) - y) * angleConstant).normalized;
        }
        else
        {
            //Mouse.current.WarpCursorPosition(new Vector2(Screen.width/2,Screen.height/2));
            //Cursor.visible = false;
            //Debug.Log("AAAA");
            //Debug.Log("AAAA");
            if(contAimDir != Vector2.zero)
            {
                aimDir = Vector3.zero;
                aimDir.x = contAimDir.x;
                aimDir.z = contAimDir.y;
                hitPoint = transform.position + aimDir * 10;
            }
            else
            {
                aimDir = playerMove.inputDir;
                hitPoint = transform.position + aimDir;
            }
            
            
            
            aimDir.Normalize();
        }
        

        if (rotate > 0)
            transform.forward = Vector3.Slerp(transform.forward,aimDir,rotate * Time.deltaTime);
    }

    


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(hitPoint, 1);

        Vector3 playerPos = transform.position;
        playerPos.y = 0;
        float aimDist = Vector3.Distance(hitPoint, playerPos);
        DrawVector(aimDir * aimDist);
        Gizmos.DrawLine(transform.position + Vector3.up * offset + aimDir * aimDist, transform.position + Vector3.up * offset + aimDir * aimDist + Vector3.down * transform.position.y);

        Gizmos.color = Color.red;
        DrawVector(GetAssistedDir(debugAngle,float.MaxValue) * aimDist);
        Gizmos.color = Color.blue;
        DrawVector((Quaternion.Euler(0, -debugAngle / 2, 0) * aimDir) * aimDist);
        DrawVector((Quaternion.Euler(0,  debugAngle / 2, 0) * aimDir) * aimDist);

    }
    public Vector3 GetAssistedDir(float angle,float bulletVel)
    {
        debugAngle = angle;
        List<RaycastHit> hits = new List<RaycastHit>();
        float localAngle = angle / assistRays;
        for(int i = -assistRays /2; i< assistRays / 2; i++)
        {
            Vector3 dir = Quaternion.Euler(0, i * localAngle,0) * aimDir;
            RaycastHit hit;
            if(Physics.Raycast(transform.position + Vector3.up * offset, dir,out hit,float.PositiveInfinity,targetableLayers))
            {
                if(hit.collider.gameObject.GetComponent<Health>() != null)
                {
                    hits.Add(hit);
                }
            }
        }
        if(hits.Count == 0)
        {
            return aimDir;
        }
        Vector3 targetdir = Vector3.zero;
        float bestVal = float.PositiveInfinity;
        GameObject enemy = null;
        foreach(RaycastHit hit in hits)
        {
            Vector3 h = hit.collider.transform.position;
            Vector3 p = transform.position;
            h.y = 0;
            p.y = 0;


            Vector3 dir = (h - p);
            //dir.Normalize();
            //float dist = Vector3.Distance(h, p);
            //float ang = Vector3.Distance(aimDir, dir);


            float val = Vector3.Angle(aimDir,dir);

            if(val < bestVal)
            {
                targetdir = dir;
                bestVal = val;
                enemy = hit.collider.gameObject;
            }
        }

       
        Vector3 compDir;
        Vector3 targetVel = Vector3.zero;
        NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
        if(agent != null)
        {
            targetVel += agent.velocity;
        }
        Rigidbody rb = enemy.GetComponent<Rigidbody>();
        if(rb != null)
        {
            targetVel += rb.velocity;
        }
        float time = Dist(enemy) / bulletVel;
        Vector3 newPos = enemy.transform.position + targetVel * time;
        compDir = newPos - transform.position;
        compDir.y = 0;
        compDir.Normalize();

        return compDir;

    }
    void DrawVector(Vector3 vect)
    {
        Gizmos.DrawLine(transform.position + Vector3.up * offset, transform.position + Vector3.up * offset + vect);
    }

    float Dist(GameObject hit)
    {
        Vector3 h = hit.transform.position;
        Vector3 p = transform.position;
        h.y = 0;
        p.y = 0;
        return Vector3.Distance(h, p);
    }
}
