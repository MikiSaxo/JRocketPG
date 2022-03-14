using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Character : MonoBehaviour
{
    public string Name;
    public int LifeMax = 100;
    public int Life = 100;

    public Sprite SpritePortrait;
    public Image Visual;
    public Animator Animator;
    public bool IsEnnemi;
    //public int Index;

    public string[] QTEAttack;
    public int[] DmgOfAttack;

    public Slider Slider;
    public Gradient Grad;
    public Image Fill;
    public TextMeshProUGUI TextLife;

    public static Character Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetMaxHealth();
    }

    public void SetMaxHealth()
    {
        Slider.maxValue = LifeMax;
        Slider.value = Life;
        TextLife.text = $"{Life}/{LifeMax}";

        Fill.color = Grad.Evaluate(1f);
    }

    public void SetHealth(int damage)
    {
        Life -= damage;
        Slider.value = Life;

        Fill.color = Grad.Evaluate(Slider.normalizedValue);
        TextLife.text = $"{Life}/{LifeMax}";
    }


    internal void Attack(Character defender)
    {
        //Animator.SetTrigger("Attack");
        Debug.Log(gameObject + "attaqueee");
        defender.Hit();
    }

    internal void Hit()
    {
        //Animator.SetTrigger("Hit");
        Debug.Log(gameObject + "a été hit");
    }
}
