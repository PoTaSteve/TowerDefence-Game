using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Enemy : MonoBehaviour
{
    public Transform GFX;

    public PathCreator pathCreator;
    public float speed;
    public float distanceTravelled;
    public int health;
    public int strength;

    public Material[] materialsAtHealth;

    private void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
    }

    public void UpdateColor()
    {
        GFX.GetChild(0).GetComponent<Renderer>().sharedMaterial = materialsAtHealth[health - 1];
    }

    public void UpdateSpeed()
    {
        if (health == 1)
        {
            speed = 6;
        }
        else if (health == 2)
        {
            speed = 8;
        }
        else if (health == 3)
        {
            speed = 10;
        }
        else if (health == 4)
        {
            speed = 12;
        }
        else if (health == 5)
        {
            speed = 15;
        }
        else if (health == 6)
        {
            speed = 10;
        }
        else if (health == 7)
        {
            speed = 10.5f;
        }
    }
}
