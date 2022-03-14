using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SelectionManager : MonoBehaviour
{
    public Material OutlineMat;
    public Material HoverMat;
    public Material DefaultMat;
    public GameUI UI;
    //public QTE QTEList;
    Character _selectedCharacter;
    //public Character[] HeroCharacter;
    Character _hoverCharacter;
    private SelectionMode _currentMode;
    public GameObject QTEObject;
    public Character[] OrderOfTurn;
    public Character[] Allies;
    public Character Hammeru;
    //int _whichChara;
    public int IndexTurn;

    public Character WhoAttackHammer;
    public int DamageOfSquid;
    public int DamageOfDrowned;
    int randomChooseDrowned;
    public int DamageOfHammer;
    public int DamageOfBulldog;
    //public bool isAttacking;

    public static SelectionManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OnTurn(OrderOfTurn[IndexTurn]);
        randomChooseDrowned = Random.Range(0, 1);
    }


    enum SelectionMode
    {
        Default,
        Attack
    }

    private void Update()
    {
        if (_currentMode == SelectionMode.Attack)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            if (hit.collider != null)
            {
                Character chara = hit.collider.gameObject.GetComponent<Character>();
                _hoverCharacter = chara;
                if (chara != null && chara.IsEnnemi)
                {
                    OnPointerEnter(chara);
                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log("Click sur ennemi " + chara);
                        if(chara == Hammeru)
                        {
                            WhoAttackHammer = _selectedCharacter;
                            Debug.Log("WhoAttackHammer " + WhoAttackHammer);
                        }
                        QTE.Instance.CharaToAttack = chara;
                        SpawnQTE();

                        //Character chara2 = hit.collider.gameObject.GetComponent<Character>();
                        ///OnPointerClick(chara2);
                    }
                }
            }
            else
            {
                OnPointerQuit(_hoverCharacter);
            }
        }

    }


    public void SpawnQTE()
    {
        QTEObject.SetActive(true);
        DurationBar.Instance.LaunchTime = 1;
    }

    public void LaunchOnTurn()
    {
        //Debug.Log("IndexTurn " + IndexTurn);
        IndexTurn++;
        if (IndexTurn == OrderOfTurn.Length)
        {
            IndexTurn = 0;
        }
        _selectedCharacter.Visual.material = DefaultMat;
        //Debug.Log("selected chara " + _selectedCharacter);
        //Debug.Log("selected chara " + _selectedCharacter.Visual.material);
        _currentMode = SelectionMode.Default;
        OnTurn(OrderOfTurn[IndexTurn]);
    }

    public void OnTurn(Character chara2)
    {
        OnPointerQuit(_hoverCharacter);
        _selectedCharacter = chara2;
        //if (chara2 == InstancesInGame[0])
        //    whichChara = 1;
        //else
        //    whichChara = 2;
        //_whichChara = chara2.Index;
        Debug.Log("c le tour de " + chara2);
        if (chara2.Name == "Bulldog")
        {
            StartCoroutine(Bulldog());
        }
        if (chara2.Name == "Hammer")
        {
            StartCoroutine(Hammer());
        }
        if (chara2.Name == "Drowned")
        {
            StartCoroutine(Drowned());
        }
        if (chara2.Name == "Squid")
        {
            StartCoroutine(Squid());
        }
        //Debug.Log("Jclique un collider");
        if (_currentMode == SelectionMode.Default)
        {
            //if (_ClickSlctCharacter != null)
              //  _ClickSlctCharacter.Visual.material = DefaultMat; //remet en defaut l'autre qui était sélectionné
            //_ClickSlctCharacter = chara2;
            chara2.Visual.material = OutlineMat;
            UI.SetCharacter(chara2);
        }
        /*else
        {
            _ClickSlctCharacter.Attack(chara2);
            _currentMode = SelectionMode.Default;
        }*/
    }

    IEnumerator Bulldog()
    {
        yield return new WaitForSeconds(1f);
        int _whichHasMoreLife = Allies[0].Life - Allies[1].Life;
        if (_whichHasMoreLife > 0)
            Allies[0].SetHealth(DamageOfBulldog);
        else
            Allies[1].SetHealth(DamageOfBulldog);
        LaunchOnTurn();
    }

    IEnumerator Hammer()
    {
        yield return new WaitForSeconds(1f);
        if (WhoAttackHammer != null)
        {
            WhoAttackHammer.SetHealth(DamageOfHammer);
        }
        else
        {
            int _whichHasMoreLife = Allies[0].Life - Allies[1].Life;
            if (_whichHasMoreLife > 0)
                Allies[0].SetHealth(DamageOfHammer);
            else
                Allies[1].SetHealth(DamageOfHammer);
        }
        WhoAttackHammer = null;
        LaunchOnTurn();
    }

    IEnumerator Drowned()
    {
        yield return new WaitForSeconds(1f);
        if (randomChooseDrowned == 0)
        {
            Allies[0].SetHealth(DamageOfDrowned);
            randomChooseDrowned++;
        }
        else
        {
            Allies[1].SetHealth(DamageOfDrowned);
            randomChooseDrowned--;
        }
        LaunchOnTurn();
    }

    IEnumerator Squid()
    {
        yield return new WaitForSeconds(1f);
        int randomTarget = Random.Range(0, 1);
        Allies[randomTarget].SetHealth(DamageOfHammer);
         
        LaunchOnTurn();
    }


    public void OnPointerEnter(Character chara)
    {
        //Debug.Log("Jpasse au-dessus d'un collider : " + chara);
        //_selectedCharacter = chara;
        if (chara.Visual.material != OutlineMat)
            chara.Visual.material = HoverMat;
        //_hoverCharacter = chara;
    }

    public void OnPointerQuit(Character chara)
    {
        //Debug.Log("Jquitte le collider : " + chara);
        //_selectedCharacter = chara;
        if (chara == null)
        {
            return;
        }
        //Debug.Log("quit chara");
        if (chara.Visual.material != OutlineMat)
            chara.Visual.material = DefaultMat;
    }

    public void SetAttackMode(int whichButton)
    {
        //if (_selectedCharacter == null)
        //    return;
        _currentMode = SelectionMode.Attack;
        //QTE.Instance.attackToChoose = whichButton;
        QTE.Instance.UpdateQTE(whichButton, _selectedCharacter);
        //isAttacking = true;
        //Debug.Log(QTE.Instance.attackToChoose);
    }
}
