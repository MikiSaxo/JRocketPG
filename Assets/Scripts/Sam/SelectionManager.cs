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
    Character _selectedCharacter;
    Character _ClickSlctCharacter;
    Character _hoverCharacter;
    private SelectionMode _currentMode;
   

    
    enum SelectionMode
    {
        Default,
        Attack
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
        if (hit.collider != null)
        {
            Character chara = hit.collider.gameObject.GetComponent<Character>();
            
            if (chara != null)
            {
                OnPointerEnter(chara);
                if (Input.GetMouseButtonDown(0))
                {
                    Character chara2 = hit.collider.gameObject.GetComponent<Character>();
                    OnPointerClick(chara2);
                }
            }
        }
        else //if (hit.collider != _hoverCharacter)
        {
            OnPointerQuit(_hoverCharacter);
            //Debug.Log("quit chara");
        }

        /*if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Jclique");

            if(hit.collider != null)
            {
                Debug.Log("Jclique un collider");
                Character chara = hit.collider.gameObject.GetComponent<Character>();
                if (chara != null)
                {
                    if (_currentMode == SelectionMode.Default)
                    {
                        if (_selectedCharacter != null)
                           _selectedCharacter.Visual.material = DefaultMat; //remet en defaut l'autre qui était sélectionné

                        _selectedCharacter = chara;
                        chara.Visual.material = OutlineMat;
                        UI.SetCharacter(chara);
                    }
                    else
                    {
                        _selectedCharacter.Attack(chara);
                        _currentMode = SelectionMode.Default;
                    }
                }
            }
        }*/
    }

    public void OnPointerClick(Character chara2)
    {
        //Debug.Log("Jclique un collider");
        if (_currentMode == SelectionMode.Default)
        {
            if (_ClickSlctCharacter != null)
                _ClickSlctCharacter.Visual.material = DefaultMat; //remet en defaut l'autre qui était sélectionné
            _ClickSlctCharacter = chara2;
            chara2.Visual.material = OutlineMat;
            UI.SetCharacter(chara2);
        }
        else
        {
            _ClickSlctCharacter.Attack(chara2);
            _currentMode = SelectionMode.Default;
        }
    }

    public void OnPointerEnter(Character chara)
    {
        //Debug.Log("Jpasse au-dessus d'un collider : " + chara);
        _selectedCharacter = chara;
        if (_selectedCharacter.Visual.material != OutlineMat)
            _selectedCharacter.Visual.material = HoverMat;
        _hoverCharacter = chara;
    }

    public void OnPointerQuit(Character chara)
    {
        //Debug.Log("Jquitte le collider : " + chara);
        _selectedCharacter = chara;
        if (_selectedCharacter.Visual.material != OutlineMat)
            _selectedCharacter.Visual.material = DefaultMat;
    }

    public void SetAttackMode()
    {
        if (_selectedCharacter == null)
        {
            return;
        }
        _currentMode = SelectionMode.Attack;
    }
}
