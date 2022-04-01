using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Recup_object : MonoBehaviour
{
    public Collider[] caisses;

    public GameObject[] GPEElements;
    public bool[] IsGPEActive;

    private void Awake()
    {
        if (GameData.initialized == false)
        {
            
            //GameData.GPEElements = new GameObject[GPEElements.Length];
            //Array.Copy(GPEElements, GameData.GPEElements, GPEElements.Length);

            GameData.IsGPEActive = new bool[IsGPEActive.Length];
            Array.Copy(IsGPEActive, GameData.IsGPEActive, IsGPEActive.Length);
        }
    }

    public void OnTriggerEnter(Collider caisses)
    {
        
    }
}
