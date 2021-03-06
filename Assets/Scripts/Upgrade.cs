using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Upgrade : MonoBehaviour
{
    [Range(0f, 4f)]
    public int[] PowerUpVisco;

    [Range(0f, 4f)]
    public int[] PowerUpBako;

    [Range(0f, 4f)]
    public int[] BoostLife;

    [Range(0f, 4f)]
    public int[] BoostGpl;

    public TextMeshProUGUI text0;
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;
    public TextMeshProUGUI text4;
    public TextMeshProUGUI text5;
    public TextMeshProUGUI text6;
    public TextMeshProUGUI text7;
    public TextMeshProUGUI text8;

    public TextMeshProUGUI textStacks;

    public GameObject fadeOut;

    AudioSource audioData;

    private void Awake()
    {
        audioData = GetComponent<AudioSource>();

        if (GameData.firstOpening == false)
            StartCoroutine(Wait1());

        if (GameData.initialized == false)
        {
            GameData.initialized = true;

            GameData.PowerUpVisco = new int[PowerUpVisco.Length];
            Array.Copy(PowerUpVisco, GameData.PowerUpVisco, PowerUpVisco.Length);

            GameData.PowerUpBako = new int[PowerUpBako.Length];
            Array.Copy(PowerUpBako, GameData.PowerUpBako, PowerUpBako.Length);

            GameData.BoostLife = new int[BoostLife.Length];
            Array.Copy(BoostLife, GameData.BoostLife, BoostLife.Length);

            GameData.BoostGpl = new int[BoostGpl.Length];
            Array.Copy(BoostGpl, GameData.BoostGpl, BoostGpl.Length);
        }
    }

    IEnumerator Wait1()
    {
        yield return new WaitForSeconds(1);
        GameData.firstOpening = true;
        SceneManager.LoadScene(1);
    }

    IEnumerator Wait2()
    {
        fadeOut.SetActive(true);
        audioData.Play(0);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
    public void NextScene()
    {
        StartCoroutine(Wait2());
    }

    public void NextScene2()
    {
        SceneManager.LoadScene("Upgrade");
    }

    public void IncreasePowerUpVisco_0()
    {
        if (GameData.PowerUpVisco[0] >= 4)
            GameData.PowerUpVisco[0] = 4;
        else if (GameData.PowerUpVisco[0] <= 4 && GameData.UpgradeStacks > 0)
        {
            GameData.UpgradeStacks--;
            GameData.PowerUpVisco[0]++;
        }
    }
    public void IncreasePowerUpVisco_1()
    {
        if (GameData.PowerUpVisco[1] >= 4)
            GameData.PowerUpVisco[1] = 4;
        else if (GameData.PowerUpVisco[1] <= 4 && GameData.UpgradeStacks > 0)
        {
            GameData.UpgradeStacks--;
            GameData.PowerUpVisco[1]++;
        }
    }
    public void IncreasePowerUpVisco_2()
    {
        if (GameData.PowerUpVisco[2] >= 4)
            GameData.PowerUpVisco[2] = 4;
        else if (GameData.PowerUpVisco[2] <= 4 && GameData.UpgradeStacks > 0)
        {
            GameData.UpgradeStacks--;
            GameData.PowerUpVisco[2]++;
        }
    }
    public void IncreasePowerUpBako_0()
    {
        if (GameData.PowerUpBako[0] >= 4)
            GameData.PowerUpBako[0] = 4;
        else if (GameData.PowerUpBako[0] <= 4 && GameData.UpgradeStacks > 0)
        {
            GameData.UpgradeStacks--;
            GameData.PowerUpBako[0]++;
        }
    }
    public void IncreasePowerUpBako_1()
    {
        if (GameData.PowerUpBako[1] >= 4)
            GameData.PowerUpBako[1] = 4;
        else if (GameData.PowerUpBako[1] <= 4 && GameData.UpgradeStacks > 0)
        {
            GameData.UpgradeStacks--;
            GameData.PowerUpBako[1]++;
        }
    }
    public void IncreaseBoostLife_0()
    {
        if (GameData.BoostLife[0] >= 4)
            GameData.BoostLife[0] = 4;
        else if (GameData.BoostLife[0] <= 4 && GameData.UpgradeStacks > 0)
        {
            GameData.UpgradeStacks--;
            GameData.BoostLife[0]++;
        }
    }
    public void IncreaseBoostLife_1()
    {
        if (GameData.BoostLife[1] >= 4)
            GameData.BoostLife[1] = 4;
        else if (GameData.BoostLife[1] <= 4 && GameData.UpgradeStacks > 0)
        {
            GameData.UpgradeStacks--;
            GameData.BoostLife[1]++;
        }
    }
    public void IncreaseBoostGpl_0()
    {
        if (GameData.BoostGpl[0] >= 4)
            GameData.BoostGpl[0] = 4;
        else if (GameData.BoostGpl[0] <= 4 && GameData.UpgradeStacks > 0)
        {
            GameData.UpgradeStacks--;
            GameData.BoostGpl[0]++;
        }
    }
    public void IncreaseBoostGpl_1()
    {
        if (GameData.BoostGpl[1] >= 4)
            GameData.BoostGpl[1] = 4;
        else if (GameData.BoostGpl[1] <= 4 && GameData.UpgradeStacks > 0)
        {
            GameData.UpgradeStacks--;
            GameData.BoostGpl[1]++;
        }
    }
    public void LessPowerUpVisco_0()
    {
        if (GameData.PowerUpVisco[0] <= 0)
            GameData.PowerUpVisco[0] = 0;
        else
        {
            GameData.UpgradeStacks++;
            GameData.PowerUpVisco[0]--;
        }
    }
    public void LessPowerUpVisco_1()
    {
        if (GameData.PowerUpVisco[1] <= 0)
            GameData.PowerUpVisco[1] = 0;
        else
        {
            GameData.UpgradeStacks++;
            GameData.PowerUpVisco[1]--;
        }
    }
    public void LessPowerUpVisco_2()
    {
        if (GameData.PowerUpVisco[2] <= 0)
            GameData.PowerUpVisco[2] = 0;
        else
        {
            GameData.UpgradeStacks++;
            GameData.PowerUpVisco[2]--;
        }
    }
    public void LessPowerUpBako_0()
    {
        if (GameData.PowerUpBako[0] <= 0)
            GameData.PowerUpBako[0] = 0;
        else
        {
            GameData.UpgradeStacks++;
            GameData.PowerUpBako[0]--;
        }
    }
    public void LessPowerUpBako_1()
    {
        if (GameData.PowerUpBako[1] <= 0)
            GameData.PowerUpBako[1] = 0;
        else
        {
            GameData.UpgradeStacks++;
            GameData.PowerUpBako[1]--;
        }
    }
    public void LessBoostLife_0()
    {
        if (GameData.BoostLife[0] <= 0)
            GameData.BoostLife[0] = 0;
        else
        {
            GameData.UpgradeStacks++;
            GameData.BoostLife[0]--;
        }
    }
    public void LessBoostLife_1()
    {
        if (GameData.BoostLife[1] <= 0)
            GameData.BoostLife[1] = 0;
        else
        {
            GameData.UpgradeStacks++;
            GameData.BoostLife[1]--;
        }
    }
    public void LessBoostGpl_0()
    {
        if (GameData.BoostGpl[0] <= 0)
            GameData.BoostGpl[0] = 0;
        else
        {
            GameData.UpgradeStacks++;
            GameData.BoostGpl[0]--;
        }
    }
    public void LessBoostGpl_1()
    {
        if (GameData.BoostGpl[1] <= 0)
            GameData.BoostGpl[1] = 0;
        else
        {
            GameData.UpgradeStacks++;
            GameData.BoostGpl[1]--;
        }
    }

    public void Update()
    {
        print("allo");

        text0.SetText("Tir de canon : <color=yellow>" + GameData.PowerUpVisco[0] + "</color>/4");
        text1.SetText("Boulet enflamme : <color=yellow>" + GameData.PowerUpVisco[1] + "</color>/4");
        text2.SetText("Crachat d'encre : <color=yellow>" + GameData.PowerUpVisco[2] + "</color>/4");
        text3.SetText("Picorage : <color=yellow>" + GameData.PowerUpBako[0] + "</color>/4");
        text4.SetText("Croassement : <color=yellow>" + GameData.PowerUpBako[1] + "</color>/4");
        text5.SetText("Sante capitaine : <color=yellow>" + GameData.BoostLife[0] + "</color>/4");
        text6.SetText("Sante Bako : <color=yellow>" + GameData.BoostLife[1] + "</color>/4");
        text7.SetText("Temps d'ecriture : <color=yellow>" + GameData.BoostGpl[0] + "</color>/4");
        text8.SetText("Nombre de PP : <color=yellow>" + GameData.BoostGpl[1] + "</color>/4");

        textStacks.SetText("Points disponibles : " + GameData.UpgradeStacks);
    }
}
