using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSkills : MonoBehaviour
{
    public Image image1;
    public Image image2;
    public Image image3;

    public void FirstPlus()
    {
        image1.fillAmount += 0.2f;
    }

    public void FirstMoins()
    {
        image1.fillAmount -= 0.2f;
    }
    public void SecondaryPlus()
    {
        image2.fillAmount += 0.2f;
    }

    public void SecondaryMoins()
    {
        image2.fillAmount -= 0.2f;
    }
    public void TertiaryPlus()
    {
        image3.fillAmount += 0.2f;
    }

    public void TertiaryMoins()
    {
        image3.fillAmount -= 0.2f;
    }
}
