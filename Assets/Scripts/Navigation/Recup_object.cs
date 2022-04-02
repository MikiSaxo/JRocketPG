using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Recup_object : MonoBehaviour
{
    public Collider[] caisses;

    public GameObject[] objCaisses;

    public bool[] IsGPEActive;

    private void Awake()
    {
        if (GameData.initialized == false)
        {
            GameData.IsGPEActive = new bool[IsGPEActive.Length];
            Array.Copy(IsGPEActive, GameData.IsGPEActive, IsGPEActive.Length);
        }
    }

    private void Update()
    {
        IsGPEActive = GameData.IsGPEActive;

        if (GameData.IsGPEActive[0] == false)
            objCaisses[0].SetActive(true);
        else
            objCaisses[0].SetActive(false);

        if (GameData.IsGPEActive[1] == false)
            objCaisses[1].SetActive(true);
        else
            objCaisses[1].SetActive(false);

        if (GameData.IsGPEActive[2] == false)
            objCaisses[2].SetActive(true);
        else
            objCaisses[2].SetActive(false);

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Tonneau 1")
        {
            print("caisses 1");
            GameData.IsGPEActive[0] = true;
            objCaisses[0].SetActive(false);
        }
        if (other.gameObject.name == "Tonneau 2")
        {
            print("caisses 2");
            GameData.IsGPEActive[1] = true;
            objCaisses[1].SetActive(false);
        }
        if (other.gameObject.name == "Tonneau 3")
        {
            print("caisses 3");
            GameData.IsGPEActive[2] = true;
            objCaisses[2].SetActive(false);
        }
    }
}
