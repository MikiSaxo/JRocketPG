using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Character : MonoBehaviour
{
    public string Name;
    public int LifeMax = 100;
    public int Life = 100;

    public Image SpritePortrait;
    public Image Visual;
    public Image Shadow;
    public GameObject LifeBar;
    public Animator Animator;
    public bool IsEnnemi;
    public int NumberOfPP;
    public int[] CoutPPAttacks;
    //public int Index;

    public string[] QTEAttack;
    public int[] DmgOfAttack;

    public Slider Slider;
    public Gradient Grad;
    public Image Fill;
    public TextMeshProUGUI TextLife;
    public GameObject[] EffetsFB;
    List<GameObject> EffetsAfterSpawn = new List<GameObject>();
    public bool IsShattered;
    public bool IsBurning;
    public bool IsCancel;

    public int NumberOfBlinking;
    public float TimeOfBlinking;
    const float _timeChangeOpacityBlinking = .001f;

    public static Character Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetMaxHealth();
        for (int i = 0; i < EffetsFB.Length; i++)
        {
            GameObject go = Instantiate(EffetsFB[i], gameObject.transform);
            go.transform.position = gameObject.transform.position;
            go.GetComponent<Image>().DOFade(0f, 0.01f);
            EffetsAfterSpawn.Add(go);
        }
    }

    public void SetEffets(int whichEffect, Character charaToFocus)
    {
        Debug.Log("SetEffets appelé");
        Debug.Log("Effets which" + EffetsAfterSpawn[whichEffect]);
        //EffetsFB[whichEffect].transform.position = charaToFocus.transform.position;
        EffetsAfterSpawn[whichEffect].GetComponent<Image>().DOFade(1, 0.01f);
    }

    public void EndEffets(int whichEffect, Character charaToFocus)
    {
        charaToFocus.EffetsAfterSpawn[whichEffect].GetComponent<Image>().DOFade(0, 1f);
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

        if(Life <= 0)
        {
            LifeBar.SetActive(false);
            if (IsEnnemi)
                Visual.DOFade(0, 2f);
            else
                SpritePortrait.DOFade(0, 2f);
            Shadow.DOFade(0, 2f);
            for (int i = 0; i < EffetsFB.Length; i++)
            {
                EffetsAfterSpawn[i].GetComponent<Image>().DOFade(0, 1f);
            }
        }

        Fill.color = Grad.Evaluate(Slider.normalizedValue);
        TextLife.text = $"{Life}/{LifeMax}";

        StartCoroutine(Blinking());
    }

    public void SetBonusHealth(int heal)
    {
        Life += heal;
        if (Life >= LifeMax)
            Life = LifeMax;

        Slider.value = Life;
        Fill.color = Grad.Evaluate(Slider.normalizedValue);
        TextLife.text = $"{Life}/{LifeMax}";
    }

    //public void UpdateHealth()
    //{
    //    Slider.value = Life;
    //    Fill.color = Grad.Evaluate(Slider.normalizedValue);
    //    TextLife.text = $"{Life}/{LifeMax}";
    //}

    IEnumerator Blinking()
    {
        for (int i = 0; i < NumberOfBlinking; i++)
        {
            Visual.DOFade(0, _timeChangeOpacityBlinking);
            yield return new WaitForSeconds(TimeOfBlinking);
            Visual.DOFade(1, _timeChangeOpacityBlinking);
            yield return new WaitForSeconds(TimeOfBlinking);
        }
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
