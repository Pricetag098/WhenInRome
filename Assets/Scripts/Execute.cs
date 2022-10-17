using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Execute : MonoBehaviour
{
    PlayerAim aim;
    [SerializeField] float detectRange,damage;
    [SerializeField] LayerMask enemy;
    [SerializeField] float soundTime, timebetweenhits,timeBetweenDmg;
    
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
				}
			}
		}
    }
    List<GameObject> lineList = new List<GameObject>();
    IEnumerator Run()
	{
        //playSound

        yield return new WaitForSecondsRealtime(soundTime);
        Time.timeScale = 0;
        
        Collider[] newHits;
        newHits = Physics.OverlapSphere(healths[0].transform.position, detectRange, enemy);
        Debug.Log(newHits.Length);
        
        while(newHits.Length > 1)
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
                    SpawnLine(newHits[i].transform.position, c.transform.position);
				}
                cols.AddRange(tempCols);
            }
            newHits = cols.ToArray();
            yield return new WaitForSecondsRealtime(timebetweenhits);
		}
        foreach (GameObject l in lineList)
        {
            lines.DespawnObj(l);
        }
        //lines.DespawnAllActive();
        //dealDmg
        foreach(Health h in healths)
		{
            h.TakeDmg(damage);
            GameObject g = particles.SpawnObj();
            g.transform.position = h.transform.position;
            g.GetComponent<ParticleSystem>().Play();
		}


        //spawn particle


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
        lineList.Add(line);
    }
}


/*
 
 
 
 
 //playSound

        yield return new WaitForSecondsRealtime(soundTime);
        Time.timeScale = 0;
        
        Collider[] newHits;
        newHits = Physics.OverlapSphere(healths[0].transform.position, detectRange, enemy);
        Debug.Log(newHits.Length);
        
        if (newHits.Length > 1)
		{
            
			while (newHits.Length >1)
			{
                List<Collider> newhitList = new List<Collider>();
                
                foreach(Collider hit in newHits)
				{
                    Vector3 origin = hit.gameObject.transform.position;

                    Collider[] a = Physics.OverlapSphere(hit.transform.position, detectRange, enemy);
                    foreach(Collider b in a)
					{
                        Health health = b.GetComponent<Health>();
						if (health)
						{
							if (!healths.Contains(health))
							{
                                healths.Add(health);
                                newhitList.Add(b);
                                SpawnLine(origin, b.transform.position);
                                
                            }
                            
						}
					}
                    newHits = newhitList.ToArray();
                    
                    //play hit sound

                    yield return new WaitForSecondsRealtime(timebetweenhits);
                }
			}
            

            
		}
        foreach (GameObject l in lineList)
        {
            lines.DespawnObj(l);
        }
        //dealDmg

        //spawn particle


        healths.Clear();
        Time.timeScale = 1;
 
 
 
 
 
 
 */
