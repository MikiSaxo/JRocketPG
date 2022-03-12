using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public int LifeMax = 100;
    public int Life = 100;

    public Sprite SpritePortrait;
    public Image Visual;
    public Animator Animator;
    public bool IsEnnemi;
    public int Index;

    public string[] QTEAttack;
    public int[] DmgOfAttack;

    public static Character Instance;

    private void Awake()
    {
        Instance = this;
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
