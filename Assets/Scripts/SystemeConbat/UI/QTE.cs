using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QTE : MonoBehaviour
{

    public TextMeshProUGUI sentenceToWrite;
    [HideInInspector]
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
    public int DmgSuppLongueVuePercent;
    public int DmgSuppTromblonPercent;
    string TromblonSuppQTE = "";
    public string[] TextTromblonSupp;
    int _saveAtkViscoForRepetBako;

    const int afterIndex2 = 2;
    const int afterIndex3 = 3;
    const int numberOfAttacks = 3;
    const float START_LAUNCH_ATTACK_ALLIES = 1.3f;
    const float END_LAUNCH_ATTACK_ALLIES = .6f;

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

    public void RhumOnChangement()
    {
        GPEEffects[0] = true; //Sert pas à grand chose mais au cas où
        sentenceToWrite.transform.localScale = new Vector3(-1, 1, 1);
    }

    public void LongueVueOnChangement()
    {
        GPEEffects[1] = true;
        sentenceToWrite.transform.localScale = new Vector3(1, -1, 1);
    }

    public void TromblonOnChangement()
    {
        GPEEffects[2] = true;
        int randomWord = Random.Range(0, 2);
        TromblonSuppQTE = TextTromblonSupp[randomWord];
        Debug.Log(TextTromblonSupp[randomWord]);
    }

    public void UpdateQTE(int whichButton, Character whichChara)
    {
        _selectedChara = whichChara;
        _whichButton = whichButton;
        getAndChangeColor = _selectedChara.QTEAttack[whichButton] + TromblonSuppQTE;
        Debug.Log("TromblonSUpp " + TromblonSuppQTE);
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

                    if (vKey == KeyCode.KeypadEnter)
                    {
                        //currentCharIndex = convertPhrase.Length - 1;
                        EndOfQTE();
                    }

                    getKeyStr = vKey.ToString();
                    getKeyCha = getKeyStr.ToCharArray();
                    //Debug.Log(vKey);

                    string str = convertPhrase;
                    string before = str.Substring(0, currentCharIndex);
                    string after = str.Substring(Mathf.Clamp((currentCharIndex + afterIndex2), 0, str.Length - 1), Mathf.Clamp((str.Length - before.Length - afterIndex2), 0, str.Length - 1));
                    string after2 = str.Substring(Mathf.Clamp((currentCharIndex + afterIndex3), 0, str.Length - 1), Mathf.Clamp((str.Length - before.Length - afterIndex3), 0, str.Length - 1));
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
                            result = "<color=red>" + before + str[currentCharIndex - 1] + str[currentCharIndex] + "<size=" + sizeOfLetterToWrite + "%><color=purple>" + str[currentCharIndex + 1] + "</color><size=100%></color>" + after2;
                            currentCharIndex++;
                            AudioManager.Instance.Play("Word_Succeed");
                            //Debug.Log("Lettre à écrire " + convertPhrase[currentCharIndex]);
                        }
                        sentenceToWrite.text = result;
                        AudioManager.Instance.Play("Letter_Succeed");
                    }
                    else
                    {
                        AudioManager.Instance.Play("Letter_Error");
                    }
                }
            }
        }
    }

    public void EndOfQTE()
    {
        Debug.Log("StageFailed : " + StageFailed);
        AudioManager.Instance.Stop("Burning_Wick");

        if (GPEEffects[0] == true)
        {
            GPEEffects[0] = false;
        }
        sentenceToWrite.transform.localScale = new Vector3(1, 1, 1);


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

        yield return new WaitForSeconds(2f);
        DmgEndQTE.SetActive(false);

        int tempButton = _whichButton;
        if ((_whichButton + 1) == 2)
            tempButton -= 1;


        _damageToPut = _selectedChara.DmgOfAttack[_whichButton + (numberOfAttacks * StageFailed)];

        if (_selectedChara.Name == "Bako")
        {
            //AudioManager.Instance.PlaySeveral("Bak_Atk", 4);
            if (_whichButton == 0)
            {
                _selectedChara.Attack(CharaToAttack, "Vis_Atk" + (tempButton + 1));
                StartCoroutine(LaunchFBAtk("Bako", _saveAtkViscoForRepetBako));
            }
            _selectedChara.Attack(CharaToAttack, "Bak_Atk" + (_whichButton + 1));

            SelectionManager.Instance.PPObject.SetActive(false);
            SelectionManager.Instance.GPEObject.SetActive(false);
            SelectionManager.Instance.ParentsButtonsAttacks.SetActive(false);
            //if (_whichButton + 1 == 2)
            //{
            //    StartCoroutine(LaunchFBAtk(3));
            //}
            //else if (_whichButton + 1 == 3)
            //{
            //    StartCoroutine(LaunchFBAtk(4));
            //}
        }
        if (_selectedChara.Name == "Visco")
        {
            //AudioManager.Instance.PlaySeveral("Vis_Atk", 8);
            _selectedChara.Attack(CharaToAttack, "Vis_Atk" + (tempButton + 1));
            SelectionManager.Instance.PPObject.SetActive(false);
            SelectionManager.Instance.GPEObject.SetActive(false);
            SelectionManager.Instance.ParentsButtonsAttacks.SetActive(false);

            if (tempButton + 1 == 1)
            {
                StartCoroutine(LaunchFBAtk("Visco", 0));
                _saveAtkViscoForRepetBako = tempButton + 1;
            }
            else if (tempButton + 1 == 2)
            {
                StartCoroutine(LaunchFBAtk("Visco", 1));
                _saveAtkViscoForRepetBako = tempButton + 1;
            }
            else
            {
                StartCoroutine(LaunchFBAtk("Visco", 2));
                _saveAtkViscoForRepetBako = tempButton + 1;
            }
        }

        yield return new WaitForSeconds(1f);

        if (GPEEffects[1] == true)
        {
            GPEEffects[1] = false;
            _damageToPut += (int)(_damageToPut * (DmgSuppLongueVuePercent / 100f));
        }
        if (GPEEffects[2] == true)
        {
            GPEEffects[2] = false;
            TromblonSuppQTE = "";

            int temp = (int)((_damageToPut * DmgSuppTromblonPercent) / 100f);


            for (int i = 0; i < SelectionManager.Instance.OrderOfTurn.Length; i++)
            {
                if (SelectionManager.Instance.OrderOfTurn[i].IsEnnemi && SelectionManager.Instance.OrderOfTurn[i] != CharaToAttack)
                {
                    SelectionManager.Instance.OrderOfTurn[i].SetHealth(SelectionManager.Instance.OrderOfTurn[i], temp);
                }
            }
        }

        CharaToAttack.SetHealth(CharaToAttack, _damageToPut);
        SelectionManager.Instance.ResetAttackMode();

        StageFailed = 0;
    }

    IEnumerator LaunchFBAtk(string ally, int whichAtk)
    {
        yield return new WaitForSeconds(START_LAUNCH_ATTACK_ALLIES);
        SelectionManager.Instance.FBLaunchAttack[whichAtk].SetActive(true);
        SelectionManager.Instance.FBLaunchAttack[whichAtk].transform.position = CharaToAttack.transform.position;
        if(ally == "Visco")
            AudioManager.Instance.PlaySeveral("Vis_Atk", 8);
        else
            AudioManager.Instance.PlaySeveral("Bak_Atk", 4);
        yield return new WaitForSeconds(END_LAUNCH_ATTACK_ALLIES);
        SelectionManager.Instance.FBLaunchAttack[whichAtk].SetActive(false);
    }
}
