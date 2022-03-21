using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
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

    public GameObject ParentsButtonsAttacks;
    public TextMeshProUGUI ButtonsAttacks1;
    public TextMeshProUGUI ButtonsAttacks2;
    public TextMeshProUGUI ButtonsAttacks3;
    public string[] NomsAttacksPelo;
    public int WinPP;
    public TextMeshProUGUI PPText;

    //int _whichChara;
    public int IndexTurn;
    [HideInInspector]
    public Character WhoAttackHammer;
    public int DamageOfSquid;
    public int DamageOfDrowned;
    int _randomChooseDrowned;
    public int DamageOfHammer;
    public int DamageOfBulldog;
    //public bool isAttacking;
    int whichButtonChoose;
    Character _burningMan;
    public int BurningDamage;
    public GameObject FB_Fire;
    public Character ShatteredMan;
    public GameObject FB_Shattered;
    public int DamageShattered;
    public string StockAttackBStr;
    public int StockAttackBInt;
    Character _cancelMan;
    public GameObject FB_Cancel;

    const int NUMBER_OF_ATTACKS = 3;

    public static SelectionManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OnTurn(OrderOfTurn[IndexTurn]);
        _randomChooseDrowned = Random.Range(0, 1);
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
                if (_hoverCharacter != null && _hoverCharacter.IsEnnemi)
                {
                    OnPointerEnter(_hoverCharacter);
                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log("Click sur ennemi " + _hoverCharacter);
                        if (_selectedCharacter == Allies[0])
                        {
                            StockAttackBStr = _selectedCharacter.QTEAttack[whichButtonChoose];
                            for (int i = 0; i < 4; i++)
                            {
                                //Debug.Log("dmg alli 1 " + i * NUMBER_OF_ATTACKS + whichButtonChoose);
                                Allies[1].DmgOfAttack[i * NUMBER_OF_ATTACKS] = Allies[0].DmgOfAttack[i* NUMBER_OF_ATTACKS + whichButtonChoose];
                            }
                            Allies[1].QTEAttack[0] = Allies[0].QTEAttack[1];
                        }

                        if(_hoverCharacter == Hammeru)
                        {
                            WhoAttackHammer = _selectedCharacter;
                            Debug.Log("WhoAttackHammer " + WhoAttackHammer);
                        }

                        if (_selectedCharacter.QTEAttack[whichButtonChoose] == Allies[0].QTEAttack[1])
                        {
                            _burningMan = _hoverCharacter;
                            BurnFB();
                            Debug.Log("BURNNNNN");
                        }

                        if (_selectedCharacter.QTEAttack[whichButtonChoose] == Allies[0].QTEAttack[2])
                        {
                            ShatteredMan = _hoverCharacter;
                            ShatteredMan.IsShattered = true;
                            //ShatFB();
                            Debug.Log("Shattereddddd");
                        }

                        if (_selectedCharacter == Allies[1] && whichButtonChoose == 0)
                        {
                            Allies[1].QTEAttack[0] = StockAttackBStr;
                            Debug.Log("REPEETTTTTTTT");
                        }

                        if (_selectedCharacter.QTEAttack[whichButtonChoose] == Allies[1].QTEAttack[2])
                        {
                            _cancelMan = _hoverCharacter;
                            CancelFB();
                            Debug.Log("CANNNNCELLLLUUU");
                        }

                        QTE.Instance.CharaToAttack = _hoverCharacter;
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

        PPText.text = "PP : " + _selectedCharacter.NumberOfPP;
    }

    private void CancelFB()
    {
        _hoverCharacter.SetEffets(2, _hoverCharacter);
    }

    public void ShatFB()
    {
        Debug.Log("Call ShatFB");
        _hoverCharacter.SetEffets(1, _hoverCharacter);
        _hoverCharacter.IsShattered = true;
        //FB_Shattered.transform.position = _hoverCharacter.transform.position + new Vector3(0, -5, 0);
        //FB_Shattered.GetComponent<Image>().DOFade(1, 0.01f);
    }

    private void BurnFB()
    {
        Debug.Log("Call Fire");
        _hoverCharacter.SetEffets(0, _hoverCharacter);
        //FB_Fire.transform.position = _hoverCharacter.transform.position + new Vector3(0, -5, 0);
        //FB_Fire.GetComponent<Image>().DOFade(1, 0.01f);
    }

    public void ResetAttackMode()
    {
        _currentMode = SelectionMode.Default;
        OnPointerQuit(_hoverCharacter);
        //Debug.Log("_hoverCharacter " + _hoverCharacter);

    }

    public void SpawnQTE()
    {
        QTEObject.SetActive(true);
        DurationBar.Instance.LaunchTime = 1;
        for (int i = 0; i < StageStepSlider.Instance.StepPoints.Length; i++)
        {
            StageStepSlider.Instance.StepPoints[i].SetActive(true);
        }
        Debug.Log("SpawnQTE");
        //_selectedCharacter.NumberOfPP -= _selectedCharacter.CoutPPAttacks[whichButtonChoose];
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

        if (_selectedCharacter.Life <= 0)
        {
            LaunchOnTurn();
            return;
        }
        
        if(!_selectedCharacter.IsEnnemi)
            _selectedCharacter.NumberOfPP += WinPP;
        
        if(_selectedCharacter == _burningMan)
        {
            _selectedCharacter.EndEffets(0, _selectedCharacter);
            _burningMan.SetHealth(BurningDamage);
            FB_Damage.Instance.MakeDmg(_selectedCharacter, BurningDamage);
            //FB_Fire.GetComponent<Image>().DOFade(0, 1f);
            _burningMan = null;
        }

        if (_selectedCharacter == _cancelMan)
        {
            LaunchOnTurn();
            _selectedCharacter.EndEffets(2, _selectedCharacter);
            //FB_Cancel.GetComponent<Image>().DOFade(0, 1f);
            _cancelMan = null;
            return;
        }


        if (chara2 == Allies[0])
        {
            ParentsButtonsAttacks.SetActive(true);
            ButtonsAttacks1.text = NomsAttacksPelo[0] + " - Cout PP : " + Allies[0].CoutPPAttacks[0]; // Pourquoi avec une array de TMPro le .text marche pas ???
            ButtonsAttacks2.text = NomsAttacksPelo[1] + " - Cout PP : " + Allies[0].CoutPPAttacks[1];
            ButtonsAttacks3.text = NomsAttacksPelo[2] + " - Cout PP : " + Allies[0].CoutPPAttacks[2];
        }
        else if (chara2 == Allies[1])
        {
            ParentsButtonsAttacks.SetActive(true);
            ButtonsAttacks1.text = NomsAttacksPelo[3] + " - Cout PP : " + Allies[1].CoutPPAttacks[0];
            ButtonsAttacks2.text = NomsAttacksPelo[4] + " - Cout PP : " + Allies[1].CoutPPAttacks[1];
            ButtonsAttacks3.text = NomsAttacksPelo[5] + " - Cout PP : " + Allies[1].CoutPPAttacks[2];
        }
        else
            ParentsButtonsAttacks.SetActive(false);
        //if (chara2 == InstancesInGame[0])
        //    whichChara = 1;
        //else
        //    whichChara = 2;
        //_whichChara = chara2.Index;
        Debug.Log("c le tour de " + chara2);
        if (chara2.Name == "Bulldog")
        {
            StartCoroutine(Bulldog());
            DamageShattered = DamageOfBulldog;
        }
        if (chara2.Name == "Hammer")
        {
            StartCoroutine(Hammer());
            DamageShattered = DamageOfHammer;
        }
        if (chara2.Name == "Drowned")
        {
            StartCoroutine(Drowned());
            DamageShattered = DamageOfDrowned;
        }
        if (chara2.Name == "Squid")
        {
            StartCoroutine(Squid());
            DamageShattered = DamageOfSquid;
        }


        if (_selectedCharacter.IsShattered)
        {
            Debug.Log("Lance Fin Shattered");
            ShatteredMan.SetHealth(DamageShattered / 2);
            FB_Damage.Instance.MakeDmg(_selectedCharacter, DamageShattered / 2);
            _selectedCharacter.EndEffets(1, _selectedCharacter);
            //FB_Shattered.GetComponent<Image>().DOFade(0, 1f);
            _selectedCharacter.IsShattered = false;
            ShatteredMan = null;
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
        {
            Allies[0].SetHealth(DamageOfBulldog);
            FB_Damage.Instance.MakeDmg(Allies[0], DamageOfDrowned);
        }
        else
        {
            Allies[1].SetHealth(DamageOfBulldog);
            FB_Damage.Instance.MakeDmg(Allies[1], DamageOfDrowned);
        }
        LaunchOnTurn();
    }

    IEnumerator Hammer()
    {
        yield return new WaitForSeconds(1f);
        if (WhoAttackHammer != null)
        {
            WhoAttackHammer.SetHealth(DamageOfHammer);
            FB_Damage.Instance.MakeDmg(WhoAttackHammer, DamageOfDrowned);
        }
        else
        {
            int _whichHasMoreLife = Allies[0].Life - Allies[1].Life;
            if (_whichHasMoreLife > 0)
            {
                Allies[0].SetHealth(DamageOfHammer);
                FB_Damage.Instance.MakeDmg(Allies[0], DamageOfDrowned);
            }
            else
            {
                Allies[1].SetHealth(DamageOfHammer);
                FB_Damage.Instance.MakeDmg(Allies[1], DamageOfDrowned);
            }
        }
        WhoAttackHammer = null;
        LaunchOnTurn();
    }

    IEnumerator Drowned()
    {
        yield return new WaitForSeconds(1f);
        if (_randomChooseDrowned == 0)
        {
            Allies[0].SetHealth(DamageOfDrowned);
            FB_Damage.Instance.MakeDmg(Allies[0], DamageOfDrowned);
            _randomChooseDrowned++;
        }
        else
        {
            Allies[1].SetHealth(DamageOfDrowned);
            FB_Damage.Instance.MakeDmg(Allies[1], DamageOfDrowned);
            _randomChooseDrowned--;
        }
        LaunchOnTurn();
    }

    IEnumerator Squid()
    {
        yield return new WaitForSeconds(1f);
        int randomTarget = Random.Range(0, 1);
        Allies[randomTarget].SetHealth(DamageOfHammer);
        FB_Damage.Instance.MakeDmg(Allies[randomTarget], DamageOfHammer);
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
        whichButtonChoose = whichButton;
        Debug.Log("NumberOfPP " + _selectedCharacter.NumberOfPP);
        Debug.Log("Cout PP " + _selectedCharacter.CoutPPAttacks[whichButtonChoose]);

        if (_selectedCharacter.NumberOfPP - _selectedCharacter.CoutPPAttacks[whichButtonChoose] < 0)
        {
            Debug.Log("Impossible car pas assez de PP");
            _currentMode = SelectionMode.Default;
        }
        else
        {
            _currentMode = SelectionMode.Attack;
            QTE.Instance.UpdateQTE(whichButtonChoose, _selectedCharacter);
        }

        //if (_selectedCharacter == null)
        //    return;
        //QTE.Instance.attackToChoose = whichButton;
        //isAttacking = true;
        //Debug.Log(QTE.Instance.attackToChoose);
    }
}
