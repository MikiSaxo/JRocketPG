using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject UIPause;

    public bool isPaused = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIPause.SetActive(!UIPause.activeSelf);

            if (isPaused == false)
            {
                isPaused = true;
                Time.timeScale = 0;
            }
            else
            {
                isPaused = false;
                Time.timeScale = 1;
            }
        }
    }

    public void Continue()
    {
        UIPause.SetActive(false);
        Time.timeScale = 1;
        if (isPaused)
        {
            isPaused = false;
        }

    }

    public void Quit()
    {

#if UNITY_STANDALONE

        Application.Quit();
#endif

#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
