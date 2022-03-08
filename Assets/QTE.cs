using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QTE : MonoBehaviour
{

    public TextMeshProUGUI sentenceToWrite;
    public TextMeshProUGUI sentenceWritten;
    public string convertPhrase;
    string getKeyStr;
    public char[] getKeyCha;
    public int i;

    private void Start()
    {
        convertPhrase = sentenceToWrite.text;
    }

    private void Update()
    {
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(vKey))
            {
                getKeyStr = vKey.ToString();
                getKeyCha = getKeyStr.ToCharArray();

                if (getKeyCha[0] == convertPhrase[i])
                {
                    sentenceWritten.text += getKeyCha[0];
                    i++;
                    Debug.Log("Lettre à écrire " + convertPhrase[i]);
                }
            }
        }
    }

}
