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
    public string NameOfHit;
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
        //SelectionManager.Instance.SndDamageCharacter();
        //Hit();
        if (Life <= 0)
        {
            SndDeathCharacter();
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
        else
            SndDamageCharacter();

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && Name == "Visco")
        {
            Attack(SelectionManager.Instance.Allies[1], "Bak_Atk2");
        }

        float w = Visual.sprite.rect.width;
        float h = Visual.sprite.rect.height;
        Visual.rectTransform.sizeDelta = new Vector2(w, h);
    }

    internal void Attack(Character defender, string NameOfAttack)
    {
        print("NameOfAttack " + NameOfAttack);
        Animator.SetTrigger(NameOfAttack);

        //if (NameOfAttack == "Vis_Atk1")
        //{
        //    StartCoroutine(Vis_Atk1());
        //}
        //else if (NameOfAttack == "Vis_Atk3")
        //{
        //    StartCoroutine(Vis_Atk3());
        //}
        //if (NameOfAttack == "Bak_Atk2")
        //{
        //    StartCoroutine(Bak_Atk2());
           
        //}

        Debug.Log(gameObject + "attaqueee");
        //defender.Hit();
    }

    IEnumerator Vis_Atk1()
    {
        //Visual.sprite.pi

        Visual.rectTransform.sizeDelta = new Vector2(809, 863);
        yield return new WaitForSeconds(1.19f);
        Visual.rectTransform.sizeDelta = new Vector2(580, 863);
    }
    
    public float WaitAtk2;
    public float WaitAtk3;
    IEnumerator Vis_Atk3()
    {
        Visual.rectTransform.sizeDelta = new Vector2(620, 863);
        yield return new WaitForSeconds(.68f);
        Visual.rectTransform.sizeDelta = new Vector2(580, 863);
    }

    IEnumerator Bak_Atk2()
    {
        //print("bak_atkkkk");
        yield return new WaitForSeconds(WaitAtk2);
        //Visual.rectTransform.sizeDelta = new Vector2(1080, 863);
        SelectionManager.Instance.Allies[0].transform.position += new Vector3(10, 0, 0);
        yield return new WaitForSeconds(WaitAtk3);
        //Visual.rectTransform.sizeDelta = new Vector2(580, 863);
        SelectionManager.Instance.Allies[0].transform.position += new Vector3(-10, 0, 0);
    }

    internal void Hit()
    {
        Animator.SetTrigger(NameOfHit);
        Debug.Log(gameObject + "a été hit");
    }

    public void SndDamageCharacter()
    {
        if (Name == "Visco")
            AudioManager.Instance.PlaySeveral("Vis_Def", 3);
        if (Name == "Bako")
            AudioManager.Instance.PlaySeveral("Bak_Def", 6);
        if (Name== "Bulldog")
            AudioManager.Instance.PlaySeveral("Bull_Def", 6);
        if (Name== "Hammer")
            AudioManager.Instance.PlaySeveral("Ham_Def", 4);
        if (Name== "Drowned")
            AudioManager.Instance.PlaySeveral("Dro_Def", 3);
        if (Name== "Squid")
            AudioManager.Instance.PlaySeveral("Squi_Def", 5);
    }

    public void SndDeathCharacter()
    {
        if (Name == "Visco")
            AudioManager.Instance.PlaySeveral("Vis_Death", 2);
        if (Name == "Bako")
            AudioManager.Instance.PlaySeveral("Bak_Death", 6);
        if (Name == "Bulldog")
            AudioManager.Instance.PlaySeveral("Bull_Death", 3);
        if (Name == "Hammer")
            AudioManager.Instance.PlaySeveral("Ham_Death", 3);
        if (Name == "Drowned")
            AudioManager.Instance.PlaySeveral("Dro_Death", 4);
        if (Name == "Squid")
            AudioManager.Instance.PlaySeveral("Squi_Death", 3);
    }

    

}


