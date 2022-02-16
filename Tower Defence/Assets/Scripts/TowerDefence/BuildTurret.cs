using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTurret : MonoBehaviour
{
    public bool canBuild;
    public int colliders;

    public GameObject RangeObj;
    public int cost;

    private void Start()
    {
        colliders = 0;
    }

    private void Update()
    {
        if (colliders > 0)
        {
            canBuild = false;
        }
        else
        {
            canBuild = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        /*
        if (other.gameObject.name == "Range")
        {
            Physics.IgnoreCollision(other, gameObject.GetComponent<Collider>());
        }
        */
        if ((other.gameObject.CompareTag("Turret(TD)") || other.gameObject.CompareTag("Path(TD)"))) // && other.gameObject.name != "Range"
        {
            colliders++;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.CompareTag("Turret(TD)") || other.gameObject.CompareTag("Path(TD)")))
        {
            colliders--;
        }
    }
}
