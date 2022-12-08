using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeByDamage : MonoBehaviour
{
    public bool damageChanges;
    public float sizeOffset;
    // Start is called before the first frame update
    void Awake()
    {
        if (damageChanges)
        {
            StartCoroutine(Change());
        }
        else
        {
            float size = GetComponent<PooledObj>().owner.gameObject.transform.localScale.x;// .GetComponent<Gun>().damage;
            transform.localScale = sizeOffset * new Vector3(size, size, size);
        }
    }

    IEnumerator Change()
    {
        while (gameObject.activeSelf && transform.localScale.x > 0)
        {
            float size = GetComponent<PooledObj>().owner.gameObject.GetComponent<Gun>().damage;
            transform.localScale = sizeOffset * new Vector3(size, size, size);
            yield return null;
        }
        transform.localScale = Vector3.zero;
        
    }
}
