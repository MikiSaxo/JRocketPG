using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class FB_Damage : MonoBehaviour
{
    public GameObject FB_Dmg;
    GameObject _endGo;

    public static FB_Damage Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void MakeDmg(Character chara, int dmg)
    {
        GameObject go = Instantiate(FB_Dmg, chara.transform);
        _endGo = go;
        go.GetComponentInChildren<TextMeshProUGUI>().text = "-" + dmg;
        go.transform.position = chara.transform.position + new Vector3(0, 20, 0);
        go.GetComponentInChildren<TextMeshProUGUI>().DOFade(1, .001f);
        go.transform.DOMoveY(gameObject.transform.position.y + 15, 3f);
        go.GetComponentInChildren<TextMeshProUGUI>().DOFade(0, 3f).OnComplete(OnDestroyObject);
    }

    private void OnDestroyObject()
    {
        Destroy(_endGo);
    }
}
