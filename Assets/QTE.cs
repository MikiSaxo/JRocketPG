using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QTE : MonoBehaviour
{

    public TextMeshProUGUI sentenceToWrite;
    public TextMeshProUGUI sentenceWritten;
    string convertPhrase;
    string getKeyStr;
    char[] getKeyCha;
    public int currentCharIndex;

    public string[] Attacks;
    //public int attackToChoose = 0;
    string result;

    public static QTE Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        convertPhrase = sentenceToWrite.text;
    }

    public void UpdateQTE(int whichButton)
    {
        sentenceToWrite.text = Attacks[whichButton];
        convertPhrase = sentenceToWrite.text;
    }

    private void Update()
    {
        if (SelectionManager.Instance.QTEObject.activeInHierarchy)
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(vKey))
                {
                    getKeyStr = vKey.ToString();
                    getKeyCha = getKeyStr.ToCharArray();
                    //Debug.Log(vKey);

                    string str = convertPhrase;

                    string before = str.Substring(0, currentCharIndex);
                    string after = str.Substring(currentCharIndex + 1, str.Length - before.Length - 1);

                        //result = "<color=red>" + before + str[currentCharIndex] + "</color>" + "<color=green>" + str[currentCharIndex+1] + "</color>" + after;
                        result =  "<color=red>" + before + str[currentCharIndex] + "</color>" + after;



                    if (getKeyCha[0] == convertPhrase[currentCharIndex])
                    {
                        sentenceWritten.text += getKeyCha[0];
                        currentCharIndex++;
                        Debug.Log("Lettre à écrire " + convertPhrase[currentCharIndex]);
                        if (convertPhrase[currentCharIndex] == ' ')
                        {
                            currentCharIndex++;
                            Debug.Log("Lettre à écrire " + convertPhrase[currentCharIndex]);
                        }
                        sentenceToWrite.text = result;
                    }


                }
                if (sentenceToWrite.text == sentenceWritten.text)
                {
                    Debug.Log("Fini");

                }

            }
        }
    }

}
