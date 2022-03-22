using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FB_Fleche : MonoBehaviour
{
    public static FB_Fleche Instance;
    public Transform[] TpPoints;
    int countTp;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        
    }

    public void LaunchArrow()
    {
        gameObject.GetComponent<Image>().DOFade(1, .001f);
        gameObject.transform.position = TpPoints[countTp].position;
        countTp++;
        if (countTp >= 2)
        {
            countTp = 0;
        }
        StartCoroutine(MoveHorizontaly());
    }

    IEnumerator MoveHorizontaly()
    {
        for (int i = 0; i < 10; i++)
        {
            gameObject.transform.DOMoveX(gameObject.transform.position.x + 10, .5f);
            yield return new WaitForSeconds(.5f);
            gameObject.transform.DOMoveX(gameObject.transform.position.x - 10, .5f);
            yield return new WaitForSeconds(.5f);
            i++;
        }
        gameObject.GetComponent<Image>().DOFade(0, .5f);
    }
}
