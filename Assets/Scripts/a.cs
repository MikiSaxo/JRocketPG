using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class a : MonoBehaviour
{
    public GameObject fadeOut;

    AudioSource audioData;


    public void gotoUpgrade()
    {
        SceneManager.LoadScene("Upgrade");
    }

    public void gotoMainMenu()
    {
        StartCoroutine(Wait());    
    }

    IEnumerator Wait()
    {
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(0);
    }
}
