using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToBattle : MonoBehaviour
{
    public Collider player;
    private bool can_moor = false;
    public GameObject feedBack;

    public int index;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && can_moor == true)
        {
            if(index == 1)
                SceneManager.LoadScene("Sam");
            if(index == 2)
                SceneManager.LoadScene("Sam");
            if(index == 3)
                SceneManager.LoadScene("Sam");
            if(index == 4)
                SceneManager.LoadScene("Credits");
        }
    }
    public void OnTriggerEnter(Collider player)
    {
        can_moor = true;
        AudioManager.Instance.PlaySeveral("Bak_Bato_Amarres_", 3);
        feedBack.SetActive(true);
    }
    public void OnTriggerExit(Collider player)
    {
        can_moor = false;
        feedBack.SetActive(false);
    }
}
