using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    public Transform[] waypoints;
    public int speed;

    private int wayPointIndex;
    private float dist;

    public Transform player;
    public Collider playerCollider;
    public bool chasing = false;

    void Start()
    {
        wayPointIndex = 0;
        transform.LookAt(waypoints[wayPointIndex].position);
    }


    void Update()
    {
        if(chasing == false)
        {
            speed = 15;
            transform.LookAt(waypoints[wayPointIndex].position);
            dist = Vector3.Distance(transform.position, waypoints[wayPointIndex].position);
            if(dist < 1f)
            {
                IncreaseIndex();
            }
            Patrol();
        }

        if(chasing == true)
        {
            speed = 25;
            transform.LookAt(player.position);
            dist = Vector3.Distance(transform.position, player.position);
            Patrol();
        }
    }

    void Patrol()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void IncreaseIndex()
    {
        wayPointIndex++;
        if(wayPointIndex >= waypoints.Length)
        {
            wayPointIndex = 0;
        }
        transform.LookAt(waypoints[wayPointIndex].position);
    }
}