using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Execute : MonoBehaviour
{
    PlayerAim aim;
    [SerializeField] float detectRange;
    [SerializeField] LayerMask enemy;
    [SerializeField] float soundTime, timebetweenhits;

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

    IEnumerator Run()
	{
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
                    Vector3 origin = hit.transform.position;

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

                                GameObject line = lines.SpawnObj();
                                LineRenderer lr = line.GetComponent<LineRenderer>();
                                lr.SetPosition(0, origin);
                                lr.SetPosition(1, b.transform.position);
                            }
                            
						}
					}
                    newHits = newhitList.ToArray();
                    //play hit sound

                    yield return new WaitForSecondsRealtime(timebetweenhits);
                }
			}
		}
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
}
