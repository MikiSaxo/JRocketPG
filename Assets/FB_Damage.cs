using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FB_Damage : MonoBehaviour
{
    private void Start()
    {
        gameObject.transform.DOMoveY(gameObject.transform.position.y + 100, 1f).OnComplete(DestroyObject);
    }

    void DestroyObject()
    {
        //Destroy(gameObject);
    }
}
