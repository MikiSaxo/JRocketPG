using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AZERTY : MonoBehaviour
{
    public void GoToSimon()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
}
