using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBuilder : MonoBehaviour
{
    public LayerMask groundMask;
    public float clickRange = 100f;
    GameManagerTD gm;

    public bool isTryingToBuild;

    public GameObject Turret1;
    public GameObject Turret2;
    public GameObject Turret3;
    public GameObject Turret4;
    public GameObject Turret5;
    
    public GameObject PlacingTurret;
    BuildTurret turr;
    public Transform TurretsParent;

    public Vector3 clickPos;

    public void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManagerTD>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !isTryingToBuild)
        {
            SelectTurret();
        }

        if (isTryingToBuild && PlacingTurret != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, clickRange, groundMask))
            {
                clickPos = hit.point;
                //Debug.Log(clickPos);
            }

            PlacingTurret.transform.position = clickPos;

            if (turr.canBuild)
            {
                // Set materials to normal

                if (Input.GetKey(KeyCode.Mouse0))
                {
                    //Place turret
                    PlacingTurret = null;

                    turr.RangeObj.SetActive(true);

                    isTryingToBuild = false;
                }
            }
            else
            {
                // Set transparent red material
            }
        }
    }

    public void SelectTurret()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlacingTurret = Instantiate(Turret1, TurretsParent);
            turr = PlacingTurret.GetComponent<BuildTurret>();
            isTryingToBuild = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlacingTurret = Instantiate(Turret2, TurretsParent);
            turr = PlacingTurret.GetComponent<BuildTurret>();
            isTryingToBuild = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlacingTurret = Instantiate(Turret3, TurretsParent);
            turr = PlacingTurret.GetComponent<BuildTurret>();
            isTryingToBuild = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlacingTurret = Instantiate(Turret4, TurretsParent);
            turr = PlacingTurret.GetComponent<BuildTurret>();
            isTryingToBuild = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlacingTurret = Instantiate(Turret5, TurretsParent);
            turr = PlacingTurret.GetComponent<BuildTurret>();
            isTryingToBuild = true;
        }        
    }
}
