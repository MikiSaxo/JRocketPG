using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image[] ImgPortrait;
    //public TMPro.TextMeshProUGUI TextLife;


    public void SetCharacter(Character chara)
    {
        for (int i = 0; i < ImgPortrait.Length; i++)
        {
            if (ImgPortrait[i].sprite == chara.SpritePortrait[0])
            {
                ImgPortrait[i].material = SelectionManager.Instance.OutlineMat;
            }
            else
            {
                ImgPortrait[i].material = SelectionManager.Instance.DefaultMat;
            }
        }
        //TextLife.text = $"{chara.Life}/{chara.LifeMax}";
    }
}
    
