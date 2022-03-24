using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QTE : MonoBehaviour
{

    public TextMeshProUGUI sentenceToWrite;
    public Character CharaToAttack;
    public GameObject DmgEndQTE;
    public Image TextDmgEndQTE;
    public Sprite[] SprTextDmgEndQTE;
    Character _selectedChara;
    int _damageToPut;
    int _whichButton;
    //public TextMeshProUGUI sentenceWritten;
    string convertPhrase;
    string getKeyStr;
    char[] getKeyCha;
    [HideInInspector]
    public int currentCharIndex;
    
    public int sizeOfLetterToWrite = 130;
    string getAndChangeColor;
    [HideInInspector]
    public int StageFailed;

    public bool[] GPEEffects;

    const int afterIndex2 = 2;
    const int afterIndex3 = 3;
    const int numberOfAttacks = 3;

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

    public void UpdateQTE(int whichButton, Character whichChara)
    {
        _selectedChara = whichChara;
        _whichButton = whichButton;
        getAndChangeColor = _selectedChara.QTEAttack[whichButton];

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
                        Debug.Log("Fini le QTE");
                        EndOfQTE();
                    }

                    if (vKey == KeyCode.Escape)
                    {
                        //currentCharIndex = convertPhrase.Length - 1;
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
                        //Debug.Log("Lettre à écrire " + convertPhrase[currentCharIndex]);
                        if (convertPhrase[currentCharIndex] == ' ')
                        {
                            result = "<color=red>" + before + str[currentCharIndex-1] + str[currentCharIndex] + "<size=" + sizeOfLetterToWrite + "%><color=purple>" + str[currentCharIndex + 1] + "</color><size=100%></color>" + after2;
                            currentCharIndex++;
                            //Debug.Log("Lettre à écrire " + convertPhrase[currentCharIndex]);
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

        

        _selectedChara.NumberOfPP -= _selectedChara.CoutPPAttacks[_whichButton];
        //Debug.Log("cout attaque : " + _selectedChara.CoutPPAttacks[_whichButton]);
        //Debug.Log("whichButton: " + _whichButton);

        currentCharIndex = 0;
        
        SelectionManager.Instance.QTEObject.SetActive(false);
        StartCoroutine(SpawnTextDmg());
        //SelectionManager.Instance.PPText.text = "PP : " + _selectedChara.NumberOfPP;
        //SelectionManager.Instance.LaunchOnTurn();
        //GameObject go = Instantiate(FB_Damage, CharaToAttack.transform.position, CharaToAttack.transform.rotation);
        //Debug.Log(go);



        //Debug.Log("charaToAttack " + CharaToAttack.transform.position);
        //Debug.Log("FB_Damage " + FB_Damage.transform.position);
        //FB_Damage.GetComponentInChildren<TextMeshProUGUI>().text = "-" + _damageToPut;
        for (int i = 0; i < StageStepSlider.Instance.StepPoints.Length; i++)
        {
            StageStepSlider.Instance.StepPoints[i].SetActive(true);
        }
    }


    IEnumerator SpawnTextDmg()
    {
        DmgEndQTE.SetActive(true);
        TextDmgEndQTE.sprite = SprTextDmgEndQTE[StageFailed];
        if (CharaToAttack.IsShattered && StageFailed == 0 && _selectedChara.QTEAttack[_whichButton] == SelectionManager.Instance.Allies[0].QTEAttack[2])
        {
            Debug.Log("c bien le shat");
            SelectionManager.Instance.ShatFB();
        }
        else
        {
            //SelectionManager.Instance.ShatteredMan = null;
            Debug.Log("c nul le shat");
        }
        yield return new WaitForSeconds(2f);
        DmgEndQTE.SetActive(false);
        _damageToPut = _selectedChara.DmgOfAttack[_whichButton + (numberOfAttacks * StageFailed)];
        CharaToAttack.SetHealth(_damageToPut);
        FB_Damage.Instance.MakeDmg(CharaToAttack, _damageToPut);
        SelectionManager.Instance.ResetAttackMode();
        StageFailed = 0;
    }
}
