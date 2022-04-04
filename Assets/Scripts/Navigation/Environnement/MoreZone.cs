using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreZone : MonoBehaviour
{
    public int nbZone = 1;

    public GameObject zone2;
    public GameObject zone3;
    public GameObject zone4;
    public GameObject zone5;

    public GameObject fog2;
    public GameObject fog3;
    public GameObject fog4;
    public GameObject fog5;

    private void Awake()
    {
        if(GameData.initialized == false)
        {
            GameData.nbZone = nbZone;
        }

        nbZone = GameData.nbZone;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameData.nbZone += 1;
        }

        if (GameData.nbZone == 2)
        {
            print("nbZone = 2");
            zone2.SetActive(true);
            fog2.SetActive(false);
        }
        if (GameData.nbZone == 3)
        {
            zone3.SetActive(true);
            fog3.SetActive(false);
            zone4.SetActive(true);
            fog4.SetActive(false);
        }
        if (GameData.nbZone == 4)
        {
            zone5.SetActive(true);
            fog5.SetActive(false);
        }
    }
}
