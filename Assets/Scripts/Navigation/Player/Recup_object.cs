using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class Recup_object : MonoBehaviour
{
    public Collider[] caisses;

    public GameObject[] objCaisses;

    public bool[] IsGPEActive;

    public GameObject[] iconGPEcaisse;
    public GameObject[] iconSupp;

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

            GameData.numCaisse = new bool[12];

            GameData.nbRhum = nbRhum;
            GameData.nbLongueVue = nbLongueVue;
            GameData.nbTroublon = nbTroublon;

            GameData.UpgradeStacks = UpgradeStacks;
        }

        for (int i = 0; i < objCaisses.Length; i++)
        {
            if(GameData.initialized == true)
            {
                GameData.numCaisse[i] = true;
            }
        }
        for (int i = 0; i < GameData.numCaisse.Length; i++)
        {
            if (GameData.numCaisse[i] == true)
            {
                print("caisse " + i + " apparût");
                objCaisses[i].SetActive(true);
            }
            else
            {
                print("caisse " + i + " dispparût");
                objCaisses[i].SetActive(false);
            }
        }

        GameData.nbScene = 1;
    }

    private void Update()
    {

        IsGPEActive = GameData.IsGPEActive;

        nbRhum = GameData.nbRhum;
        nbLongueVue = GameData.nbLongueVue;
        nbTroublon = GameData.nbTroublon;

        UpgradeStacks = GameData.UpgradeStacks;

        if (nbRhum > 0)
            GameData.IsGPEActive[0] = true;
        if (nbLongueVue > 0)
            GameData.IsGPEActive[1] = true;
        if (nbTroublon > 0)
            GameData.IsGPEActive[2] = true;


    }

    public void AjoutRhum()
    {
        iconGPEcaisse[0].SetActive(true);
        StartCoroutine(Wait());
    }
    public void AjoutLongueVue()
    {
        iconGPEcaisse[1].SetActive(true);
        StartCoroutine(Wait());
    }
    public void AjoutTroublon()
    {
        iconGPEcaisse[2].SetActive(true);
        StartCoroutine(Wait());
    }
    public void AjoutUpgradeStacks()
    {
        iconGPEcaisse[3].SetActive(true);
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        if (iconGPEcaisse[0].activeSelf == true)
        {
            iconGPEcaisse[0].GetComponent<Image>().DOFade(0, 1.5f);
            iconSupp[0].GetComponent<Image>().DOFade(0, 1.5f);
            yield return new WaitForSeconds(1.5f);
            DestroyIconGPE(0);
        }
        else if (iconGPEcaisse[1].activeSelf == true)
        {
            iconGPEcaisse[1].GetComponent<Image>().DOFade(0, 1.5f);
            iconSupp[1].GetComponent<Image>().DOFade(0, 1.5f);
            yield return new WaitForSeconds(1.5f);
            DestroyIconGPE(1);
        }
        else if (iconGPEcaisse[2].activeSelf == true)
        {
            iconGPEcaisse[2].GetComponent<Image>().DOFade(0, 1.5f);
            iconSupp[2].GetComponent<Image>().DOFade(0, 1.5f);
            yield return new WaitForSeconds(1.5f);
            DestroyIconGPE(2);
        }
        else if (iconGPEcaisse[3].activeSelf == true)
        {
            iconGPEcaisse[3].GetComponent<Image>().DOFade(0, 1.5f);
            iconSupp[3].GetComponent<Image>().DOFade(0, 1.5f);
            yield return new WaitForSeconds(1.5f);
            DestroyIconGPE(3);
        }
    }

    public void DestroyIconGPE(int index)
    {
        iconGPEcaisse[index].SetActive(false);
        iconSupp[index].SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Tonneaux 0")
        {
            print("caisses 0");
            GameData.nbRhum++;
            AjoutRhum();
            objCaisses[10].SetActive(false);
        }
        if (other.gameObject.name == "Tonneaux 1")
        {
            print("caisses 1");
            GameData.nbLongueVue++;
            AjoutLongueVue();
            objCaisses[0].SetActive(false);
        }
        if (other.gameObject.name == "Tonneaux 2")
        {
            print("caisses 2");
            GameData.UpgradeStacks++;
            AjoutUpgradeStacks();
            objCaisses[1].SetActive(false);
        }
        if (other.gameObject.name == "Tonneaux 3")
        {
            print("caisses 3");
            GameData.UpgradeStacks++;
            AjoutUpgradeStacks();
            objCaisses[2].SetActive(false);
        }
        if (other.gameObject.name == "Tonneaux 4")
        {
            print("caisses 4");
            GameData.nbLongueVue++;
            AjoutLongueVue();
            objCaisses[3].SetActive(false);
        }
        if (other.gameObject.name == "Tonneaux 5")
        {
            print("caisses 5");
            GameData.UpgradeStacks++;
            AjoutUpgradeStacks();
            objCaisses[4].SetActive(false);
        }
        if (other.gameObject.name == "Tonneaux 6")
        {
            print("caisses 6");
            GameData.nbTroublon++;
            AjoutTroublon();
            objCaisses[5].SetActive(false);
        }
        if (other.gameObject.name == "Tonneaux 7")
        {
            print("caisses 7");
            GameData.nbRhum++;
            AjoutRhum();
            objCaisses[6].SetActive(false);
        }
        if (other.gameObject.name == "Tonneaux 8")
        {
            print("caisses 8");
            GameData.nbTroublon++;
            AjoutTroublon();
            objCaisses[7].SetActive(false);
        }
        if (other.gameObject.name == "Tonneaux 9")
        {
            print("caisses 9");
            GameData.UpgradeStacks++;
            AjoutUpgradeStacks();
            objCaisses[8].SetActive(false);
        }
        if (other.gameObject.name == "Tonneaux 10")
        {
            print("caisses 10");
            GameData.nbLongueVue++;
            AjoutLongueVue();
            objCaisses[9].SetActive(false);
        }
        if (other.gameObject.name == "Tonneaux 11")
        {
            print("caisses 11");
            GameData.nbRhum++;
            AjoutRhum();
            objCaisses[11].SetActive(false);
        }
    }
}