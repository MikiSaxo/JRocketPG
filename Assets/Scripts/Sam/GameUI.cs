using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image ImgPortrait;
    public TMPro.TextMeshProUGUI TextLife;


    public void SetCharacter(Character chara)
    {
        ImgPortrait.sprite = chara.SpritePortrait;
        TextLife.text = chara.Life.ToString();
    }
}
    
