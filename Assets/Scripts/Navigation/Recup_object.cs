using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Recup_object : MonoBehaviour
{
    public Collider[] caisses;

    public GameObject[] objCaisses;

    public bool[] IsGPEActive;

    public int nbRhum;
    public int nbLongueVue;
    public int nbTroublon;

    public int UpgradeStacks = 0;

    private void Awake()
    {
        if (GameData.initialized == false)
        {
            GameData.IsGPEActive = new bool[IsGPEActive.Length];
            Array.Copy(IsGPEActive, GameData.IsGPEActive, IsGPEActive.Length);

            GameData.objCaisses = new GameObject[objCaisses.Length];
            Array.Copy(objCaisses, GameData.objCaisses, objCaisses.Length);

            GameData.nbRhum = nbRhum;
            GameData.nbLongueVue = nbLongueVue;
            GameData.nbTroublon = nbTroublon;

            GameData.UpgradeStacks = UpgradeStacks;
        }
    }

    private void Update()
    {
        IsGPEActive = GameData.IsGPEActive;

        objCaisses = GameData.objCaisses;

        nbRhum = GameData.nbRhum;
        nbLongueVue = GameData.nbLongueVue;
        nbTroublon = GameData.nbTroublon;

        UpgradeStacks = GameData.UpgradeStacks;

        /*for (int i = 0; i < GameData.objCaisses.Length; i++)
        {
            if (GameData.objCaisses[i].activeSelf == false)
                objCaisses[i].SetActive(false);
        }*/

        if (nbRhum > 0)
            GameData.IsGPEActive[0] = true;
        if (nbLongueVue > 0)
            GameData.IsGPEActive[1] = true;
        if (nbTroublon > 0)
            GameData.IsGPEActive[2] = true;



        /*if (GameData.IsGPEActive[0] == false)
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
            objCaisses[2].SetActive(false);*/

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Tonneaux 0")
        {
            print("caisses 0");
            GameData.objCaisses[10].SetActive(false);
        }
        if (other.gameObject.name == "Tonneaux 1")
        {
            print("caisses 1");
            GameData.nbRhum++;
            GameData.objCaisses[0].SetActive(false);
        }
        if (other.gameObject.name == "Tonneaux 2")
        {
            print("caisses 2");
            GameData.nbLongueVue++;
            GameData.objCaisses[1].SetActive(false);
        }
        if (other.gameObject.name == "Tonneaux 3")
        {
            print("caisses 3");
            GameData.nbTroublon++;
            GameData.objCaisses[2].SetActive(false);
        }
        if (other.gameObject.name == "Tonneaux 4")
        {
            print("caisses 4");
            GameData.UpgradeStacks++;
            GameData.objCaisses[3].SetActive(false);
        }
        if (other.gameObject.name == "Tonneaux 5")
        {
            print("caisses 5");
            GameData.objCaisses[4].SetActive(false);
        }
        if (other.gameObject.name == "Tonneaux 6")
        {
            print("caisses 6");
            GameData.objCaisses[5].SetActive(false);
        }
        if (other.gameObject.name == "Tonneaux 7")
        {
            print("caisses 7");
            GameData.objCaisses[6].SetActive(false);
        }
        if (other.gameObject.name == "Tonneaux 8")
        {
            print("caisses 8");
            GameData.objCaisses[7].SetActive(false);
        }
        if (other.gameObject.name == "Tonneaux 9")
        {
            print("caisses 9");
            GameData.objCaisses[8].SetActive(false);
        }
        if (other.gameObject.name == "Tonneaux 10")
        {
            print("caisses 10");
            GameData.objCaisses[9].SetActive(false);
        }
    }
}