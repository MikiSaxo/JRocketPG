using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject UIPause;

    public bool isPaused = false;
    public static PauseMenu Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIPause.SetActive(!UIPause.activeSelf);

            if (isPaused == false)
            {
                isPaused = true;
                Time.timeScale = 0;
                if(GameData.nbScene == 2)
                {
                    ActivePause();
                }
            }
            else
            {
                isPaused = false;
                Time.timeScale = 1;
                if(GameData.nbScene == 2)
                {
                    NotActivePause();
                }
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
            if (GameData.nbScene == 2)
            {
                NotActivePause();
            }
        }

    }

    public void ActivePause()
    {
        for (int i = 0; i < SelectionManager.Instance.OrderOfTurn.Length; i++)
        {
            SelectionManager.Instance.OrderOfTurn[i].GamePaused();
        }
    }

    public void NotActivePause()
    {
        for (int i = 0; i < SelectionManager.Instance.OrderOfTurn.Length; i++)
        {
            SelectionManager.Instance.OrderOfTurn[i].GameNotPaused();
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
