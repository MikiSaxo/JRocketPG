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

    private void Awake()
    {

        if(GameData.initialized == false)
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

    public void NextScene()
    {
        SceneManager.LoadScene("Sam2");
    }

    public void NextScene2()
    {
        SceneManager.LoadScene("Upgrade");
    }

    public void IncreasePowerUpVisco_0()
    {
        GameData.PowerUpVisco[0]++;
        PowerUpVisco[0]++;
        if (GameData.PowerUpVisco[0] >= 4)
            GameData.PowerUpVisco[0] = 4;
        if (PowerUpVisco[0] >= 4)
            PowerUpVisco[0] = 4;
        text0.SetText("PowerUpVisco [0] = " + GameData.PowerUpVisco[0]);
    }
    public void IncreasePowerUpVisco_1()
    {
        GameData.PowerUpVisco[1]++;
        PowerUpVisco[1]++;
        if (GameData.PowerUpVisco[1] >= 4)
            GameData.PowerUpVisco[1] = 4;
        if (PowerUpVisco[1] >= 4)
            PowerUpVisco[1] = 4;
        text1.SetText("PowerUpVisco [1] = " + GameData.PowerUpVisco[1]);
    }
    public void IncreasePowerUpVisco_2()
    {
        GameData.PowerUpVisco[2]++;
        PowerUpVisco[2]++;
        if (GameData.PowerUpVisco[2] >= 4)
            GameData.PowerUpVisco[2] = 4;
        if (PowerUpVisco[2] >= 4)
            PowerUpVisco[2] = 4;
        text2.SetText("PowerUpVisco [2] = " + GameData.PowerUpVisco[2]);
    }
    public void IncreasePowerUpBako_0()
    {
        GameData.PowerUpBako[0]++;
        PowerUpBako[0]++;
        if (GameData.PowerUpBako[0] >= 4)
            GameData.PowerUpBako[0] = 4;
        if (PowerUpBako[0] >= 4)
            PowerUpBako[0] = 4;
        text3.SetText("PowerUpBako [0] = " + GameData.PowerUpBako[0]);
    }
    public void IncreasePowerUpBako_1()
    {
        GameData.PowerUpBako[1]++;
        PowerUpBako[1]++;
        if (GameData.PowerUpBako[1] >= 4)
            GameData.PowerUpBako[1] = 4;
        if (PowerUpBako[1] >= 4)
            PowerUpBako[1] = 4;
        text4.SetText("PowerUpBako [1] = " + GameData.PowerUpBako[1]);
    }
    public void IncreaseBoostLife_0()
    {
        GameData.BoostLife[0]++;
        BoostLife[0]++;
        if (GameData.BoostLife[0] >= 4)
            GameData.BoostLife[0] = 4;
        if (BoostLife[0] >= 4)
            BoostLife[0] = 4;
        text5.SetText("BoostLife [0] = " + GameData.BoostLife[0]);
    }
    public void IncreaseBoostLife_1()
    {
        GameData.BoostLife[1]++;
        BoostLife[1]++;
        if (GameData.BoostLife[1] >= 4)
            GameData.BoostLife[1] = 4;
        if (BoostLife[1] >= 4)
            BoostLife[1] = 4;
        text6.SetText("BoostLife [1] = " + GameData.BoostLife[1]);
    }
    public void IncreaseBoostGpl_0()
    {
        GameData.BoostGpl[0]++;
        BoostGpl[0]++;
        if (GameData.BoostGpl[0] >= 4)
            GameData.BoostGpl[0] = 4;
        if (BoostGpl[0] >= 4)
            BoostGpl[0] = 4;
        text7.SetText("BoostGpl [0] = " + GameData.BoostGpl[0]);
    }
    public void IncreaseBoostGpl_1()
    {
        GameData.BoostGpl[1]++;
        BoostGpl[1]++;
        if (GameData.BoostGpl[1] >= 4)
            GameData.BoostGpl[1] = 4;
        if (BoostGpl[1] >= 4)
            BoostGpl[1] = 4;
        text8.SetText("BoostGpl [0] = " + GameData.BoostGpl[1]);
    }
    private void Update()
    {
        text0.SetText("PowerUpVisco [0] = " + GameData.PowerUpVisco[0]);
        text1.SetText("PowerUpVisco [1] = " + GameData.PowerUpVisco[1]);
        text2.SetText("PowerUpVisco [2] = " + GameData.PowerUpVisco[2]);
        text3.SetText("PowerUpBako [0] = " + GameData.PowerUpBako[0]);
        text4.SetText("PowerUpBako [1] = " + GameData.PowerUpBako[1]);
        text5.SetText("BoostLife [0] = " + GameData.BoostLife[0]);
        text6.SetText("BoostLife [1] = " + GameData.BoostLife[1]);
        text7.SetText("BoostGpl [0] = " + GameData.BoostGpl[0]);
        text8.SetText("BoostGpl [1] = " + GameData.BoostGpl[1]);
    }
}
