using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public Material OutlineMat;
    public Material DefaultMat;
    public GameUI UI;
    Character _selectedCharacter;
    //int _currentMode;

    enum SelectionMode
    {
        Default,
        Attack
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Jclique");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if(hit.collider != null)
            {
                Debug.Log("Jclique un collider");
                Character chara = hit.collider.gameObject.GetComponent<Character>();
                if (chara != null)
                {
                    if (_selectedCharacter != null)
                    {
                        _selectedCharacter.Visual.material = DefaultMat;
                    }
                    _selectedCharacter = chara;
                    chara.Visual.material = OutlineMat;
                    UI.SetCharacter(chara);
                }
            }
        }
    }

    public void SetAttackMode()
    {
        if (_selectedCharacter == null)
        {
            return;
        }
        //_currentMode = SelectionMode.Attack;
    }
}
