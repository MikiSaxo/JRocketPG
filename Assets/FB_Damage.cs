using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class FB_Damage : MonoBehaviour
{
    public TextMeshProUGUI Dmg;

    public static FB_Damage Instance;

    private void Awake()
    {
        Instance = this;
    }
    public void MakeDmg(Character chara, int dmg)
    {
        Dmg.text = "-" + dmg;
        gameObject.transform.position = chara.transform.position + new Vector3(0, 20, 0);
        Dmg.DOFade(1, .001f);
        gameObject.transform.DOMoveY(gameObject.transform.position.y + 15, 3f);
        Dmg.DOFade(0, 3f);
    }

  
}
