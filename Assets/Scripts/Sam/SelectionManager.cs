using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

        PPText.text = "PP : " + _selectedCharacter.NumberOfPP;
    }


    public void SpawnQTE()
    {
        QTEObject.SetActive(true);
        DurationBar.Instance.LaunchTime = 1;
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
        
        if(!_selectedCharacter.IsEnnemi)
            _selectedCharacter.NumberOfPP += WinPP;
        
        

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
            ButtonsAttacks1.text = NomsAttacksPelo[3] + " - Cout PP : " + Allies[0].CoutPPAttacks[0];
            ButtonsAttacks2.text = NomsAttacksPelo[4] + " - Cout PP : " + Allies[0].CoutPPAttacks[1];
            ButtonsAttacks3.text = NomsAttacksPelo[5] + " - Cout PP : " + Allies[0].CoutPPAttacks[2];
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
        if (_randomChooseDrowned == 0)
        {
            Allies[0].SetHealth(DamageOfDrowned);
            _randomChooseDrowned++;
        }
        else
        {
            Allies[1].SetHealth(DamageOfDrowned);
            _randomChooseDrowned--;
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
