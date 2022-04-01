using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GPEChangements : MonoBehaviour
{
    public GameObject[] GPEElements;
    public bool[] IsGPEActive;
    [HideInInspector]
    public Character whichTurnChara;


    public static GPEChangements Instance;

    private void Awake()
    {
        Instance = this;


    }

    private void Start()
    {

        for (int i = 0; i< IsGPEActive.Length; i++)
            {
                if (IsGPEActive[i])
                {
                    //GameObject go = Instantiate(GPEElements[i], RangeGPE.transform);
                    //go.SetActive(true);
                    GPEElements[i].SetActive(true);
        //_gPEAfterSpawn.Add(go);
                }
            }
        
    }

    public void RhumOn(int index)
    {
        if (!whichTurnChara.IsEnnemi)
        {
            whichTurnChara.SetBonusHealth(whichTurnChara.LifeMax / 2);
            FB_Damage.Instance.MakeHeal(whichTurnChara, whichTurnChara.LifeMax / 2);
        }
        QTE.Instance.RhumOnChangement();
        GPEElements[index].SetActive(false);
    }

    public void LongueVueOn(int index)
    {
        QTE.Instance.LongueVueOnChangement();
        GPEElements[index].SetActive(false);
    }

    public void TromblonOn(int index)
    {
        SelectionManager.Instance.SetAttackMode(SelectionManager.Instance.WhichButtonChoose);
        QTE.Instance.TromblonOnChangement();
        GPEElements[index].SetActive(false);
    }

   

    
}
