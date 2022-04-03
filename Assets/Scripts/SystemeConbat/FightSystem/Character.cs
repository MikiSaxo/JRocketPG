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

    public Image[] SpritePortrait;
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
    int _damageToTake;
    Character _defender;
    public bool[] _saveEffets = new bool[3];

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
            //go.GetComponent<Image>().DOFade(0f, 0.01f);
            go.SetActive(false);
            EffetsAfterSpawn.Add(go);
        }
    }

    public void SetEffets(int whichEffect, Character charaToFocus)
    {
        //Debug.Log("SetEffets appelé");
        //Debug.Log("Effets which" + EffetsAfterSpawn[whichEffect]);
        //EffetsFB[whichEffect].transform.position = charaToFocus.transform.position;
        //EffetsAfterSpawn[whichEffect].GetComponent<Image>().DOFade(1, 0.01f);
        //EffetsAfterSpawn[whichEffect].GetComponent<Image>().transform.DOScale(4, .001f);
        EffetsAfterSpawn[whichEffect].SetActive(true);
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

    public void SetHealth(Character defender, int damage)
    {
        Life -= damage;
        Slider.value = Life;
        _damageToTake = damage;
        _defender = defender;
        //SelectionManager.Instance.SndDamageCharacter();
        //Hit();
        if (Life <= 0)
        {
            SndDeathCharacter();
            AudioManager.Instance.Play("Dead_Chara");
            LifeBar.SetActive(false);
            if (IsEnnemi)
                Visual.DOFade(0, 2f);
            else
            {
                SpritePortrait[0].DOFade(0, 2f);
                SpritePortrait[1].DOFade(0, 2f);
            }

            if (SelectionManager.Instance.Allies[0].Life <= 0 && SelectionManager.Instance.Allies[1].Life <= 0)
            {
                SelectionManager.Instance.FadeIn();
                SelectionManager.Instance.LoseGame();
                print("ils sont mort");
            }

            if (Name == "Bako" || Name == "Visco")
            {
                Shadow.DOFade(1, 2f);
                print("oui " + Name);
            }
            else
                Shadow.DOFade(0, 2f);

            GamePaused();

            SelectionManager.Instance.PPObject.SetActive(true);
            SelectionManager.Instance.ParentsButtonsAttacks.SetActive(true);
        }
        else
        {
            StartCoroutine(Blinking());
        }

        Fill.color = Grad.Evaluate(Slider.normalizedValue);
        TextLife.text = $"{Life}/{LifeMax}";

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

    IEnumerator Blinking()
    {
        yield return new WaitForSeconds(TimeOfBlinking * 10);
        _defender.Hit();
        yield return new WaitForSeconds(TimeOfBlinking * 10);
        FB_Damage.Instance.MakeDmg(_defender, _damageToTake);

        if (IsEnnemi)
        {
            SelectionManager.Instance.PPObject.SetActive(true);
            SelectionManager.Instance.ParentsButtonsAttacks.SetActive(true);
        }

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
        float w = Visual.sprite.rect.width;
        float h = Visual.sprite.rect.height;
        Visual.rectTransform.sizeDelta = new Vector2(w, h);
    }

    public void GamePaused()
    {
        for (int i = 0; i < EffetsAfterSpawn.Count; i++)
        {
            EffetsAfterSpawn[i].SetActive(false);
        }
    }

    public void GameNotPaused()
    {
        if (IsBurning)
            EffetsAfterSpawn[0].SetActive(true);
        if (IsShattered)
            EffetsAfterSpawn[1].SetActive(true);
        if (IsCancel)
            EffetsAfterSpawn[2].SetActive(true);
    }


    internal void Attack(Character defender, string NameOfAttack)
    {
        print("NameOfAttack " + NameOfAttack);
        Animator.SetTrigger(NameOfAttack);

    }

    internal void Hit()
    {
        SndDamageCharacter();
        if (Name == "Visco")
            Animator.SetTrigger("Vis_Hit");
        else if (Name == "Bako")
            Animator.SetTrigger("Bak_Hit");
        else
            Animator.SetTrigger("Hit");
        Debug.Log(gameObject + "a été hit");
    }

    public void SndDamageCharacter()
    {
        if (Name == "Visco")
            AudioManager.Instance.PlaySeveral("Vis_Def", 3);
        if (Name == "Bako")
            AudioManager.Instance.PlaySeveral("Bak_Def", 6);
        if (Name == "Bulldog")
            AudioManager.Instance.PlaySeveral("Bull_Def", 6);
        if (Name == "Hammer")
            AudioManager.Instance.PlaySeveral("Ham_Def", 4);
        if (Name == "Drowned")
            AudioManager.Instance.PlaySeveral("Dro_Def", 3);
        if (Name == "Squid")
            AudioManager.Instance.PlaySeveral("Squi_Def", 5);
    }

    public void SndDeathCharacter()
    {
        if (Name == "Visco")
            AudioManager.Instance.PlaySeveral("Vis_Death", 2);
        if (Name == "Bako")
            AudioManager.Instance.PlaySeveral("Bak_Death", 4);
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


