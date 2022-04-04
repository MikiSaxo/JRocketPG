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

    AudioSource audioData;

    private void Start()
    {
        audioData = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && can_moor == true)
        {
            StartCoroutine(Bruit());
        }
    }

    IEnumerator Bruit()
    {
        audioData.Play(0);
        yield return new WaitForSeconds(7);
        if (index == 1)
            SceneManager.LoadScene(3);
        if (index == 2)
            SceneManager.LoadScene(4);
        if (index == 3)
            SceneManager.LoadScene(6);
        if (index == 4)
            SceneManager.LoadScene(7);
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
