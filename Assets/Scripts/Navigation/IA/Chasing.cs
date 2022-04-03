using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : MonoBehaviour
{
    public Patroller patroller;
    public Collider player;

    public void OnTriggerEnter(Collider player)
    {
        patroller.chasing = true;
    }
    public void OnTriggerExit(Collider player)
    {
        patroller.chasing = false;
    }
}
