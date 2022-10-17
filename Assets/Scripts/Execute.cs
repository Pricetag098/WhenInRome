using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Execute : MonoBehaviour
{
    PlayerAim aim;
    [SerializeField] float detectRange,damage;
    [SerializeField] LayerMask enemy;
    [SerializeField] float soundTime, timebetweenhits,timeAfterDmg;
    
    [SerializeField] ObjectPooler lines, particles;
    List<Health> healths = new List<Health>();
    // Start is called before the first frame update
    void Start()
    {
        aim = GetComponent<PlayerAim>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
            healths.Clear();
            RaycastHit hit;
            if(Physics.Raycast(transform.position, aim.aimDir, out hit, float.PositiveInfinity))
			{
                if(hit.collider.gameObject != gameObject && hit.collider.gameObject.GetComponent<Health>())
				{
                    healths.Add(hit.collider.gameObject.GetComponent<Health>());
                    StartCoroutine("Run");
                    particles.DespawnAllActive();
                }
			}
		}
    }
    //List<GameObject> lineList = new List<GameObject>();
    IEnumerator Run()
	{
        particles.DespawnAllActive();
        //playSound

        yield return new WaitForSecondsRealtime(soundTime);
        Time.timeScale = 0;
        
        Collider[] newHits;
        newHits = Physics.OverlapSphere(healths[0].transform.position, detectRange, enemy);
        
        
        while(newHits.Length > 1)
		{
            yield return new WaitForSecondsRealtime(timebetweenhits);
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
                    SpawnLine(newHits[i].transform.position, c.transform.position);
				}
                cols.AddRange(tempCols);
            }
            newHits = cols.ToArray();
            
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



        
        yield return new WaitForSecondsRealtime(timeAfterDmg);
        lines.DespawnAllActive();
        
        
        healths.Clear();
        Time.timeScale = 1;
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


