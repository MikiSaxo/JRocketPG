using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QTE : MonoBehaviour
{

    public TextMeshProUGUI sentenceToWrite;
    //public TextMeshProUGUI sentenceWritten;
    string convertPhrase;
    string getKeyStr;
    char[] getKeyCha;
    public int currentCharIndex;
    public string[] AttacksPelo1;
    public string[] AttacksPelo2;
    public int sizeOfLetterToWrite = 130;
    string getAndChangeColor;
    public int StageFailed;

    const int afterIndex2 = 2;
    const int afterIndex3 = 3;

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

    public void UpdateQTE(int whichButton, int whichChara)
    {
        if (whichChara == 1)
        {
            getAndChangeColor = AttacksPelo1[whichButton];
        }
        else
        {
            getAndChangeColor = AttacksPelo2[whichButton];
        }

        convertPhrase = getAndChangeColor;

        string after = getAndChangeColor.Substring(1, getAndChangeColor.Length - 1);
        getAndChangeColor = "<color=purple><size=" + sizeOfLetterToWrite + "%>" + getAndChangeColor[0] + "<size=100%></color>" + after;

        sentenceToWrite.text = getAndChangeColor;


        //Debug.Log("getand : " + convertPhrase);
        //Debug.Log("convertPh : " + convertPhrase);
        //Debug.Log("sentencetowrit : " + convertPhrase);
    }

    private void Update()
    {
        if (SelectionManager.Instance.QTEObject.activeInHierarchy)
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(vKey))
                {
                    if (currentCharIndex == convertPhrase.Length - 1)
                    {
                        Debug.Log("Fini");
                        EndOfQTE();
                    }

                    getKeyStr = vKey.ToString();
                    getKeyCha = getKeyStr.ToCharArray();
                    //Debug.Log(vKey);

                    string str = convertPhrase;
                    string before = str.Substring(0, currentCharIndex);
                    string after = str.Substring(Mathf.Clamp((currentCharIndex + afterIndex2), 0, str.Length -1), Mathf.Clamp((str.Length - before.Length - afterIndex2), 0, str.Length - 1));
                    string after2 = str.Substring(Mathf.Clamp((currentCharIndex + afterIndex3), 0, str.Length -1), Mathf.Clamp((str.Length - before.Length - afterIndex3), 0, str.Length - 1));
                    //Debug.Log("string after : " + after);
                    //result = "<color=red>" + before + str[currentCharIndex] + "</color>" + "<color=green>" + str[currentCharIndex+1] + "</color>" + after;
                    
                    result = "<color=red>" + before + str[currentCharIndex] + "<size=" + sizeOfLetterToWrite + "%><color=purple>" + str[currentCharIndex + 1] + "</color><size=100%></color>" + after;
                    

                    if (getKeyCha[0] == convertPhrase[currentCharIndex])
                    {
                        //sentenceWritten.text += getKeyCha[0];
                        currentCharIndex++;
                        Debug.Log("Lettre à écrire " + convertPhrase[currentCharIndex]);
                        if (convertPhrase[currentCharIndex] == ' ')
                        {
                            result = "<color=red>" + before + str[currentCharIndex-1] + str[currentCharIndex] + "<size=" + sizeOfLetterToWrite + "%><color=purple>" + str[currentCharIndex + 1] + "</color><size=100%></color>" + after2;
                            currentCharIndex++;
                            Debug.Log("Lettre à écrire " + convertPhrase[currentCharIndex]);
                        }
                        sentenceToWrite.text = result;
                    }
                }
            }
        }
    }

    public void EndOfQTE()
    {
        Debug.Log("StageFailed : " + StageFailed);
        currentCharIndex = 0;
        SelectionManager.Instance.QTEObject.SetActive(false);
    }

}
