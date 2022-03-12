using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Character[] InstancesInGame;
    int whichChara;
    //public bool isAttacking;

    public static SelectionManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OnTurn(InstancesInGame[0]);
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
                if (chara != null && chara.isEnnemi)
                {
                    OnPointerEnter(chara);
                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log("Click sur ennemi " + chara);
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

    public void OnTurn(Character chara2)
    {
        _selectedCharacter = chara2;
        if (chara2 == InstancesInGame[0])
            whichChara = 1;
        else
            whichChara = 2;
        Debug.Log(chara2);
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
        QTE.Instance.UpdateQTE(whichButton, whichChara);
        //isAttacking = true;
        //Debug.Log(QTE.Instance.attackToChoose);
    }
}
