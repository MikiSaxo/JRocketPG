using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class SpawnCredit : MonoBehaviour
{
    public TextMeshProUGUI[] Canvas;

    private void Start()
    {
        for (int i = 0; i < Canvas.Length; i++)
        {
            Canvas[i].DOFade(1, 5f);
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Application.Quit();
            print("oui c la fin fin fin");
        }
    }
}
