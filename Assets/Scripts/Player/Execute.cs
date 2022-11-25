using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Execute : MonoBehaviour
{
    PlayerAim aim;
    [SerializeField] float detectRange,damage, range;
    [SerializeField] LayerMask enemy;
    [SerializeField] float soundTime, timebetweenhits,timeAfterDmg;
    [SerializeField] Volume volume;
    [SerializeField] ObjectPooler lines, particles;
    [SerializeField] Holster holster;
    [SerializeField] float ppSpeed= 1;
    List<Health> healths = new List<Health>();
    bool running = false;
    
    [SerializeField] SoundPlayer use, chain, hit;

    CombatMeter cm;
    // Start is called before the first frame update
    void Start()
    {
        aim = GetComponentInParent<PlayerAim>();
        cm = GetComponentInParent<CombatMeter>();
        
        
    }
    PlayerInputs inputActions;
    InputAction executeInput;
    private void Awake()
    {
        inputActions = new PlayerInputs();
    }
    private void OnEnable()
    {
        executeInput = inputActions.Player.Execute;
        executeInput.Enable();
        executeInput.performed += TryExecute;
    }
    private void OnDisable()
    {
        executeInput.Disable();
    }

    private void Update()
    {
        volume.weight = running ? Mathf.Lerp(volume.weight, 1, Time.unscaledDeltaTime * ppSpeed) : Mathf.Lerp(volume.weight, 0, Time.unscaledDeltaTime * ppSpeed);
    }

    void TryExecute(InputAction.CallbackContext context)
    {
        if (!running && cm.meter >= cm.maxMeter)
        {

            healths.Clear();
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up * aim.offset, aim.GetAssistedDir(20), out hit, range))
            {
                if (hit.collider.gameObject != gameObject && hit.collider.gameObject.GetComponent<Health>())
                {
                    healths.Add(hit.collider.gameObject.GetComponent<Health>());
                    holster.transform.GetChild(holster.selectedWeapon).GetComponent<Gun>().CancelReload();
                    StartCoroutine("Run");
                    particles.DespawnAllActive();
                    cm.Discharge();
                }
            }
        }
    }
    //List<GameObject> lineList = new List<GameObject>();
    IEnumerator Run()
	{
        running = true;
        particles.DespawnAllActive();
        Time.timeScale = 0;
        //volume.SetActive(true);
        use.Play();
        SpawnLine(transform.position, healths[0].transform.position);
        yield return new WaitForSecondsRealtime(soundTime);
        
        
        
        Collider[] newHits;
        newHits = Physics.OverlapSphere(healths[0].transform.position, detectRange, enemy);

        chain.Play();
        while (newHits.Length > 1)
		{
           
            List<Collider> cols = new List<Collider>();
            for(int i = 0; i < newHits.Length; i++)
			{
				if (healths.Contains(newHits[i].GetComponent<Health>()))
				{
                    continue;
				}
                
                healths.Add(newHits[i].GetComponent<Health>());
                Collider[] tempCols = Physics.OverlapSphere(newHits[i].transform.position, detectRange, enemy);
                foreach(Collider c in tempCols)
				{
                    //if(!healths.Contains(c.gameObject.GetComponent<Health>()))
                    SpawnLine(newHits[i].transform.position, c.transform.position);
                    
				}

                cols.AddRange(tempCols);
                
            }
            newHits = cols.ToArray();
            
            if (newHits.Length > 0)
            {
                chain.Play();
                yield return new WaitForSecondsRealtime(timebetweenhits);
            }
        }


        //yield return new WaitForSecondsRealtime(timebetweenhits);

        //dealDmg
        GameObject g = null;
        foreach (Health h in healths)
		{
            h.TakeDmg(damage);
            g = particles.SpawnObj();
            g.transform.position = h.transform.position;
            g.GetComponent<ParticleSystem>().Play();
		}
        lines.DespawnAllActive();
        hit.Play();


        yield return new WaitForSecondsRealtime(timeAfterDmg);
        particles.DespawnAllActive();
        
        
        healths.Clear();
        Time.timeScale = 1;
        running = false;
        //volume.SetActive(false);
    }
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
        foreach(Health h in healths)
		{
            Gizmos.DrawWireSphere(h.transform.position,detectRange);
		}
	}

    void SpawnLine(Vector3 origin, Vector3 point)
	{
        GameObject line = lines.SpawnObj();
        LineRenderer lr = line.GetComponent<LineRenderer>();
        lr.SetPosition(0, origin);
        lr.SetPosition(1, point);
        //lineList.Add(line);
    }
}


