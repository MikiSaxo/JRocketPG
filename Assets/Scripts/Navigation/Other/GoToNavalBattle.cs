using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNavalBattle : MonoBehaviour
{
    public Collider player;

    AudioSource audioData;
    public GameObject fadeOut;


    private void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider player)
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        fadeOut.SetActive(true);
        audioData.Play(0);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(5);
    }
}
