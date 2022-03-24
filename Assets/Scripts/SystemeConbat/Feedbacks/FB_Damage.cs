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

    const int _timeDmgDisappear = 3;
    const int _timeHealDisappear = 5;
    const int _whereSpawnFB = 20;
    const int _whereHasToGoFB = 10;

    private void Awake()
    {
        Instance = this;
    }

    public void MakeDmg(Character chara, int dmg)
    {
        GameObject go = Instantiate(FB_Dmg, chara.transform);
        _endGo = go;
        go.GetComponentInChildren<TextMeshProUGUI>().text = "-" + dmg;
        go.transform.position = chara.transform.position + new Vector3(0, _whereSpawnFB, 0);
        go.GetComponentInChildren<TextMeshProUGUI>().DOFade(1, .001f);
        go.transform.DOMoveY(gameObject.transform.position.y + _whereHasToGoFB, _timeDmgDisappear);
        go.GetComponentInChildren<TextMeshProUGUI>().DOFade(0, _timeDmgDisappear).OnComplete(OnDestroyObject);
    }

    public void MakeHeal(Character chara, int heal)
    {
        GameObject go = Instantiate(FB_Dmg, chara.transform);
        _endGo = go;
        go.GetComponentInChildren<TextMeshProUGUI>().text = "<color=green>" + heal + "</color>";
        go.transform.position = chara.transform.position + new Vector3(_whereSpawnFB/5, _whereSpawnFB, 0);
        go.GetComponentInChildren<TextMeshProUGUI>().DOFade(1, .001f);
        go.transform.DOMoveY(gameObject.transform.position.y + _whereHasToGoFB, _timeHealDisappear);
        go.GetComponentInChildren<TextMeshProUGUI>().DOFade(0, _timeHealDisappear).OnComplete(OnDestroyObject);
    }

    private void OnDestroyObject()
    {
        Destroy(_endGo);
    }
}
