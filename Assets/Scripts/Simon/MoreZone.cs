using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreZone : MonoBehaviour
{
    private int nbZone = 1;

    public GameObject zone2;
    public GameObject zone3;
    public GameObject zone4;

    public GameObject fog2;
    public GameObject fog3;
    public GameObject fog4;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            nbZone += 1;
        }

        if (nbZone == 2)
        {
            zone2.SetActive(true);
            fog2.SetActive(false);
        }
        if (nbZone == 3)
        {
            zone3.SetActive(true);
            fog3.SetActive(false);
        }
        if (nbZone == 4)
        {
            zone4.SetActive(true);
            fog4.SetActive(false);
        }
    }
}
