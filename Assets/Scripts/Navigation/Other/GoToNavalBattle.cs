using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNavalBattle : MonoBehaviour
{
    public Collider player;

    public void OnTriggerEnter(Collider player)
    {
        SceneManager.LoadScene(5);
    }
}
