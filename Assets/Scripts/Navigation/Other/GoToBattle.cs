using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToBattle : MonoBehaviour
{
    public Collider player;
    private bool can_moor = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && can_moor == true)
        {
            SceneManager.LoadScene("Sam2");
        }    
    }
    public void OnTriggerEnter(Collider player)
    {
        can_moor = true;
    }
    public void OnTriggerExit(Collider player)
    {
        can_moor = false;
    }
}
