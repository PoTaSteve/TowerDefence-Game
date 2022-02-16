using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : MonoBehaviour
{
    public float range;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateEffect", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateEffect()
    {
        Collider[] nearTurrets = Physics.OverlapSphere(transform.position, range);

        foreach (Collider coll in nearTurrets)
        {
            if (coll.gameObject.TryGetComponent<Turret>(out Turret turr))
            {
                if (!turr.affectedByVillage)
                {
                    turr.range += turr.range * 0.1f;
                    turr.fireRate -= turr.fireRate * 0.1f;
                    turr.affectedByVillage = true;
                }                
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
