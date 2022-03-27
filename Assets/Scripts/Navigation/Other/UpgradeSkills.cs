using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSkills : MonoBehaviour
{
    public Image image1;
    public Image image2;
    public Image image3;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            UpgradeFirst();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            UpgradeSecondary();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            UpgradeThirth();
        }
    }

    public void UpgradeFirst()
    {
        image1.fillAmount += 0.2f;
    }

    public void UpgradeSecondary()
    {
        image2.fillAmount += 0.2f;
    }

    public void UpgradeThirth()
    {
        image3.fillAmount += 0.2f;
    }
}
