using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Random = UnityEngine.Random;

public class SelectionManager : MonoBehaviour
{
    public Material OutlineMat;
    public Material HoverMat;
    public Material DefaultMat;
    public GameUI UI;
    //public QTE QTEList;
    Character _selectedCharacter;
    //public Character[] HeroCharacter;
    Character _hoverCharacter;
    bool isEnter;
    private SelectionMode _currentMode;
    public GameObject QTEObject;
    public Character[] OrderOfTurn;
    public Character[] Allies;
    public Character Hammeru;

    public GameObject ParentsButtonsAttacks;
    public GameObject PPObject;
    public GameObject GPEObject;
    public TextMeshProUGUI ButtonsAttacks1;
    public TextMeshProUGUI ButtonsAttacks2;
    public TextMeshProUGUI ButtonsAttacks3;
    public TextMeshProUGUI EffectAttacks1;
    public TextMeshProUGUI EffectAttacks2;
    public TextMeshProUGUI EffectAttacks3;
    public Sprite[] SprButtonAttack;
    public Image[] ImgButtonAttack;

    //public GameObject RangeGPE;
    //List<GameObject> _gPEAfterSpawn = new List<GameObject>();
    public string[] NomsAttacksPelo;
    public string[] NomsEffectAttacksPelo;
    public int WinPP;
    public TextMeshProUGUI PPText;

    //int _whichChara;
    public int IndexTurn;
    [HideInInspector]
    public Character WhoAttackHammer;
    public int DamageOfSquid;
    public int DamageOfDrowned;
    int _randomChooseDrowned;
    public int DamageOfHammer;
    public int DamageOfBulldog;
    //public bool isAttacking;
    public int WhichButtonChoose;
    public int BurningDamage;

    [HideInInspector]
    public Character ShatteredMan;
    [HideInInspector]
    public int DamageShattered;
    [HideInInspector]
    public string StockAttackBStr;
    [HideInInspector]
    public int StockAttackBInt;


    public int BonusDmgTirCanon;
    public int BonusDmgBrulure;
    public GameObject[] Inks;

    public int BonusDmgPicorage;

    public int BonusLifeVisco;
    public int BonusLifeBako;

    public int BonusTimeQTE;
    public int BonusPP;

    public GameObject[] FBLaunchAttack;

    public GameObject Fade;
    public GameObject Lose;
    public GameObject Win;

    const int NUMBER_OF_ATTACKS = 3;
    const float WAIT_ATTACK_ENNEMY = 1.5f;
    const float START_LAUNCH_ATTACK_ENNEMY = 1.1f;
    const float END_LAUNCH_ATTACK_ENNEMY = .7f;
    bool _isGameFinished;

    public static SelectionManager Instance;

    private void Awake()
    {
        Instance = this;
        GameData.initialized = true;
    }

    private void Start()
    {
        OnTurn(OrderOfTurn[IndexTurn]);
        _randomChooseDrowned = Random.Range(0, 1);
        AudioManager.Instance.Play("MusicCombat");
        FadeOut();
        UpdateBonus();
    }

    public void UpdateBonus()
    {
        PowerViscoTirCanon();
        PowerViscoBrulure();
        PowerBakoPico();
        PowerBakoCroa();
        PowerLife();
        PowerTimeQTE();
        PowerPPBonus();

        Allies[1].SetMaxHealth();
    }

    public void PowerViscoTirCanon()
    {
        for (int i = 0; i < Allies[0].DmgOfAttack.Length - 3; i += 3)
        {
            Allies[0].DmgOfAttack[i] += GameData.PowerUpVisco[0] * BonusDmgTirCanon;
        }
    }
    public void PowerViscoBrulure()
    {
        BurningDamage += BonusDmgBrulure * GameData.PowerUpVisco[1];
    }

    public void PowerBakoPico()
    {
        for (int i = 1; i < Allies[1].DmgOfAttack.Length - 3; i += 3)
        {
            Allies[1].DmgOfAttack[i] += GameData.PowerUpBako[0] * BonusDmgPicorage;
        }
    }

    public void PowerBakoCroa()
    {
        Allies[1].CoutPPAttacks[2] -= GameData.PowerUpBako[1];
    }

    public void PowerLife()
    {
        Allies[0].LifeMax += GameData.BoostLife[0] * BonusLifeVisco;
        Allies[0].Life += GameData.BoostLife[0] * BonusLifeVisco;
        Allies[1].LifeMax += GameData.BoostLife[1] * BonusLifeBako;
        Allies[1].Life += GameData.BoostLife[1] * BonusLifeBako;
    }

    public void PowerTimeQTE()
    {
        QTEObject.SetActive(true);
        DurationBar.Instance.DurationTime += BonusTimeQTE * GameData.BoostGpl[0];
        QTEObject.SetActive(false);
    }

    public void PowerPPBonus()
    {
        WinPP += BonusPP * GameData.BoostGpl[1]; ;
    }

    public void AfficherEncre()
    {
        Inks[0].SetActive(true);
        //for (int i = GameData.PowerUpVisco[2]; i > 0; i--)
        for (int i = 3; i > 0; i--)
        {
            Inks[i].SetActive(false);
        }
    }

    enum SelectionMode
    {
        Default,
        Attack
    }

    private void Update()
    {
        if (_currentMode == SelectionMode.Attack)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            if (hit.collider != null)
            {
                Character chara = hit.collider.gameObject.GetComponent<Character>();
                _hoverCharacter = chara;
                if (_hoverCharacter != null && _hoverCharacter.IsEnnemi)
                {
                    OnPointerEnter(_hoverCharacter);
                    if (Input.GetMouseButtonDown(0))
                    {
                        QTE.Instance.UpdateQTE(WhichButtonChoose, _selectedCharacter);
                        //Debug.Log("Click sur ennemi " + _hoverCharacter);
                        if (_selectedCharacter == Allies[0])
                        {
                            //StockAttackBStr = _selectedCharacter.QTEAttack[whichButtonChoose];
                            for (int i = 0; i < 4; i++)
                            {
                                //Debug.Log("dmg alli 1 " + i * NUMBER_OF_ATTACKS + whichButtonChoose);
                                Allies[1].DmgOfAttack[i * NUMBER_OF_ATTACKS] = Allies[0].DmgOfAttack[i * NUMBER_OF_ATTACKS + WhichButtonChoose];
                            }
                            Allies[1].QTEAttack[0] = Allies[0].QTEAttack[WhichButtonChoose];
                        }

                        if (_hoverCharacter == Hammeru)
                        {
                            WhoAttackHammer = _selectedCharacter;
                            //Debug.Log("WhoAttackHammer " + WhoAttackHammer);
                        }

                        if (_selectedCharacter.QTEAttack[WhichButtonChoose] == Allies[0].QTEAttack[0] && _selectedCharacter == Allies[0])
                        {
                            AudioManager.Instance.Play("Vis_Launch_Atk1");
                        }

                        if (_selectedCharacter.QTEAttack[WhichButtonChoose] == Allies[0].QTEAttack[1] && _selectedCharacter == Allies[0])
                        {
                            _hoverCharacter.IsBurning = true;
                            //BurnFB();
                            //Debug.Log("BURNNNNN");
                            AudioManager.Instance.Play("Vis_Launch_Atk2");
                        }

                        if (_selectedCharacter.QTEAttack[WhichButtonChoose] == Allies[0].QTEAttack[2] && _selectedCharacter == Allies[0])
                        {
                            _hoverCharacter.IsShattered = true;
                            //ShatFB();
                            AfficherEncre();
                            //Debug.Log("Shattereddddd");
                            AudioManager.Instance.Play("Vis_Launch_Atk3");
                        }
                        else
                            Inks[0].SetActive(false);

                        if (_selectedCharacter == Allies[1] && WhichButtonChoose == 0 && _selectedCharacter == Allies[1])
                        {
                            //Allies[1].QTEAttack[0] = StockAttackBStr;
                            //Debug.Log("REPEETTTTTTTT");
                            if (Allies[1].QTEAttack[0] == Allies[0].QTEAttack[0])
                                AudioManager.Instance.Play("Bak_Launch_Atk_Visco1");
                            if (Allies[1].QTEAttack[0] == Allies[0].QTEAttack[1])
                                AudioManager.Instance.Play("Bak_Launch_Atk_Visco2");
                            if (Allies[1].QTEAttack[0] == Allies[0].QTEAttack[2])
                                AudioManager.Instance.Play("Bak_Launch_Atk_Visco3");
                        }

                        if (_selectedCharacter.QTEAttack[WhichButtonChoose] == Allies[1].QTEAttack[1])
                        {
                            AudioManager.Instance.Play("Bak_Launch_Atk2");
                        }

                        if (_selectedCharacter.QTEAttack[WhichButtonChoose] == Allies[1].QTEAttack[2])
                        {
                            _hoverCharacter.IsCancel = true;
                            //CancelFB();
                            AudioManager.Instance.Play("Bak_Launch_Atk3");
                            //Debug.Log("CANNNNCELLLLUUU");
                        }

                        QTE.Instance.CharaToAttack = _hoverCharacter;
                        SpawnQTE();

                        //Character chara2 = hit.collider.gameObject.GetComponent<Character>();
                        ///OnPointerClick(chara2);
                    }
                }
            }
            else
            {
                OnPointerQuit(_hoverCharacter);
            }
        }

        PPText.text = "PP : " + _selectedCharacter.NumberOfPP;
    }

    public void CancelFB(Character chara)
    {
        chara.SetEffets(2, chara);
    }

    public void ShatFB(Character chara)
    {
        Debug.Log("Call ShatFB");
        chara.SetEffets(1, chara);
        chara.IsShattered = true;
        //FB_Shattered.transform.position = _hoverCharacter.transform.position + new Vector3(0, -5, 0);
        //FB_Shattered.GetComponent<Image>().DOFade(1, 0.01f);
    }

    public void BurnFB(Character chara)
    {
        Debug.Log("Call Fire");
        chara.SetEffets(0, chara);
        //FB_Fire.transform.position = _hoverCharacter.transform.position + new Vector3(0, -5, 0);
        //FB_Fire.GetComponent<Image>().DOFade(1, 0.01f);
    }

    public void ResetAttackMode()
    {
        _currentMode = SelectionMode.Default;
        OnPointerQuit(_hoverCharacter);
        //Debug.Log("_hoverCharacter " + _hoverCharacter);

    }

    public void SpawnQTE()
    {
        QTEObject.SetActive(true);
        DurationBar.Instance.LaunchTime = 1;
        Debug.Log("SpawnQTE");
        AudioManager.Instance.Play("Paper_Open");
        PauseMenu.Instance.ActivePause();
        //_selectedCharacter.NumberOfPP -= _selectedCharacter.CoutPPAttacks[whichButtonChoose];
    }

    public void LoseGame()
    {
        _isGameFinished = true;
        AudioManager.Instance.Stop("MusicCombat");
        Debug.LogWarning("c finiii t mort");
        StartCoroutine(EndGame());
    }

    public void WinGame()
    {
        _isGameFinished = true;
        AudioManager.Instance.Stop("MusicCombat");
        Debug.LogWarning("c finiii t'as gagné");
        StartCoroutine(WinnGame());
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.Play("LoseGame");
        Lose.SetActive(true);
        Lose.GetComponent<TextMeshProUGUI>().DOFade(1, 5f);
        Lose.transform.DOScale(1.2f, 5f);
        yield return new WaitForSeconds(5f);
        Lose.GetComponent<TextMeshProUGUI>().DOFade(0, 1f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        print("ichhh");
    }

    IEnumerator WinnGame()
    {
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.Play("WinGame");
        Win.SetActive(true);
        Win.GetComponent<TextMeshProUGUI>().DOFade(1, 5f);
        Win.transform.DOScale(1.2f, 5f);
        yield return new WaitForSeconds(5f);
        Win.GetComponent<TextMeshProUGUI>().DOFade(0, 1f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
        print("ichhedrfhehh");
    }

    public void FadeIn()
    {
        Fade.SetActive(true);
        Fade.GetComponent<Image>().DOFade(1, 1f);
    }
    public void FadeOut()
    {
        StartCoroutine(DesacFadeOut());
    }

    IEnumerator DesacFadeOut()
    {
        Fade.GetComponent<Image>().DOFade(0, 2f);
        yield return new WaitForSeconds(2f);
        Fade.SetActive(false);
    }

    public void LaunchOnTurn()
    {
        //Debug.Log("IndexTurn " + IndexTurn);

        

        if (_isGameFinished)
            return;

        IndexTurn++;
        if (IndexTurn == OrderOfTurn.Length)
        {
            IndexTurn = 0;
        }
        Allies[0].Shadow.material = DefaultMat;
        //print(_selectedCharacter.Shadow.material);
        _selectedCharacter.Shadow.material = DefaultMat;
        //print(_selectedCharacter.Shadow.material);
        //Debug.Log("selected chara " + _selectedCharacter);
        //Debug.Log("selected chara " + _selectedCharacter.Visual.material);
        _currentMode = SelectionMode.Default;
        OnTurn(OrderOfTurn[IndexTurn]);
    }

    public void OnTurn(Character chara2)
    {
        OnPointerQuit(_hoverCharacter);
        _selectedCharacter = chara2;

        GPEChangements.Instance.whichTurnChara = _selectedCharacter;

        if (Allies[0].Life <= 0 && Allies[1].Life <= 0)
        {
            LoseGame();
        }

        if (_selectedCharacter.Life <= 0)
        {
            LaunchOnTurn();
            return;
        }

        if (!_selectedCharacter.IsEnnemi)
        {
            _selectedCharacter.NumberOfPP += WinPP;
            PPObject.SetActive(true);
            FB_Fleche.Instance.LaunchArrow();
        }

        if (_selectedCharacter.IsBurning)
        {
            _selectedCharacter.EndEffets(0, _selectedCharacter);
            _selectedCharacter.SetHealth(_selectedCharacter, BurningDamage);
            _selectedCharacter.IsBurning = false;
            FB_Damage.Instance.MakeDmg(_selectedCharacter, BurningDamage);
            //FB_Fire.GetComponent<Image>().DOFade(0, 1f);
        }

        for (int i = 0; i < OrderOfTurn.Length; i++)
        {
            if (OrderOfTurn[i].IsBurning)
            {
                OrderOfTurn[i].SetHealth(OrderOfTurn[i], BurningDamage);
                FB_Damage.Instance.MakeDmg(OrderOfTurn[i], BurningDamage);
            }
        }

        if (_selectedCharacter.IsCancel)
        {
            _selectedCharacter.EndEffets(2, _selectedCharacter);
            _selectedCharacter.IsCancel = false;
            LaunchOnTurn();
            //FB_Cancel.GetComponent<Image>().DOFade(0, 1f);
            return;
        }


        if (chara2 == Allies[0])
        {
            ParentsButtonsAttacks.SetActive(true);
            ButtonsAttacks1.text = NomsAttacksPelo[0] + " - <color=yellow>Cout PP : " + Allies[0].CoutPPAttacks[0]; // Pourquoi avec une array de TMPro le .text marche pas ???
            ButtonsAttacks2.text = NomsAttacksPelo[1] + " - <color=yellow>Cout PP : " + Allies[0].CoutPPAttacks[1];
            ButtonsAttacks3.text = NomsAttacksPelo[2] + " - <color=yellow>Cout PP : " + Allies[0].CoutPPAttacks[2];

            EffectAttacks1.text = NomsEffectAttacksPelo[0] + "\n<color=orange>Degat max : " + Allies[0].DmgOfAttack[0];
            EffectAttacks2.text = NomsEffectAttacksPelo[1] + "\n<color=orange>Degat max : " + Allies[0].DmgOfAttack[1];
            EffectAttacks3.text = NomsEffectAttacksPelo[2] + "\n<color=orange>Degat max : " + Allies[0].DmgOfAttack[2];

            for (int i = 0; i < 3; i++)
            {
                ImgButtonAttack[i].sprite = SprButtonAttack[i];
            }
        }
        else if (chara2 == Allies[1])
        {
            ParentsButtonsAttacks.SetActive(true);
            ButtonsAttacks1.text = NomsAttacksPelo[3] + " - <color=yellow>Cout PP : " + Allies[1].CoutPPAttacks[0];
            ButtonsAttacks2.text = NomsAttacksPelo[4] + " - <color=yellow>Cout PP : " + Allies[1].CoutPPAttacks[1];
            ButtonsAttacks3.text = NomsAttacksPelo[5] + " - <color=yellow>Cout PP : " + Allies[1].CoutPPAttacks[2];

            EffectAttacks1.text = NomsEffectAttacksPelo[3] + "\n<color=orange>Degat max : " + Allies[1].DmgOfAttack[0];
            EffectAttacks2.text = NomsEffectAttacksPelo[4] + "\n<color=orange>Degat max : " + Allies[1].DmgOfAttack[1];
            EffectAttacks3.text = NomsEffectAttacksPelo[5] + "\n<color=orange>Degat max : " + Allies[1].DmgOfAttack[2];

            for (int i = 0; i < 3; i++)
            {
                ImgButtonAttack[i].sprite = SprButtonAttack[i + 3];
            }
        }
        else
        {
            ParentsButtonsAttacks.SetActive(false);
            PPObject.SetActive(false);
            GPEObject.SetActive(false);
        }
        //if (chara2 == InstancesInGame[0])
        //    whichChara = 1;
        //else
        //    whichChara = 2;
        //_whichChara = chara2.Index;
        Debug.Log("c le tour de " + chara2);
        SndHoverCharacter(chara2);

        if (chara2.Name == "Bulldog")
        {
            StartCoroutine(Bulldog());
            DamageShattered = DamageOfBulldog;
        }
        if (chara2.Name == "Hammer")
        {
            StartCoroutine(Hammer());
            DamageShattered = DamageOfHammer;
        }
        if (chara2.Name == "Drowned")
        {
            StartCoroutine(Drowned());
            DamageShattered = DamageOfDrowned;
        }
        if (chara2.Name == "Squid")
        {
            StartCoroutine(Squid());
            DamageShattered = DamageOfSquid;
            //print(DamageShattered);
            //print(DamageOfSquid);
        }



        if (_selectedCharacter.IsShattered)
        {
            Debug.Log("Lance Fin Shattered");
            _selectedCharacter.SetHealth(_selectedCharacter, DamageShattered / 2);
            FB_Damage.Instance.MakeDmg(_selectedCharacter, DamageShattered / 2);
            _selectedCharacter.EndEffets(1, _selectedCharacter);
            //FB_Shattered.GetComponent<Image>().DOFade(0, 1f);

            SndDamageCharacter();

            _selectedCharacter.IsShattered = false;
            ShatteredMan = null;
        }

        //Debug.Log("Jclique un collider");
        if (_currentMode == SelectionMode.Default)
        {
            //if (_ClickSlctCharacter != null)
            //  _ClickSlctCharacter.Visual.material = DefaultMat; //remet en defaut l'autre qui était sélectionné
            //_ClickSlctCharacter = chara2;
            if (chara2 == Allies[1])
            {
                Allies[0].Shadow.material = OutlineMat;
            }
            else
                chara2.Shadow.material = OutlineMat;

            UI.SetCharacter(chara2);
        }
        /*else
        {
            _ClickSlctCharacter.Attack(chara2);
            _currentMode = SelectionMode.Default;
        }*/
    }

    IEnumerator Bulldog()
    {
        yield return new WaitForSeconds(WAIT_ATTACK_ENNEMY/2);
        AudioManager.Instance.PlaySeveral("Bull_Atk", 4);
        yield return new WaitForSeconds(WAIT_ATTACK_ENNEMY/2);
        int _whichHasMoreLife = Allies[0].Life - Allies[1].Life;
        if (_whichHasMoreLife > 0)
        {
            Allies[0].SetHealth(Allies[0], DamageOfBulldog);
            _selectedCharacter.Attack(Allies[0], "Attack");
        }
        else
        {
            Allies[1].SetHealth(Allies[1], DamageOfBulldog);
            _selectedCharacter.Attack(Allies[1], "Attack");
        }

        yield return new WaitForSeconds(START_LAUNCH_ATTACK_ENNEMY);
        FBLaunchAttack[6].SetActive(true);
        yield return new WaitForSeconds(END_LAUNCH_ATTACK_ENNEMY);
        FBLaunchAttack[6].SetActive(false);

        yield return new WaitForSeconds(WAIT_ATTACK_ENNEMY);
        LaunchOnTurn();
    }

    IEnumerator Hammer()
    {
        yield return new WaitForSeconds(WAIT_ATTACK_ENNEMY/2);
        AudioManager.Instance.PlaySeveral("Ham_Atk", 4);
        yield return new WaitForSeconds(WAIT_ATTACK_ENNEMY/2);
        if (WhoAttackHammer != null)
        {
            _selectedCharacter.Attack(WhoAttackHammer, "Attack");
            WhoAttackHammer.SetHealth(WhoAttackHammer, DamageOfHammer);
        }
        else
        {
            int _whichHasMoreLife = Allies[0].Life - Allies[1].Life;
            if (_whichHasMoreLife > 0)
            {
                _selectedCharacter.Attack(Allies[0], "Attack");
                Allies[0].SetHealth(Allies[0], DamageOfHammer);
            }
            else
            {
                _selectedCharacter.Attack(Allies[1], "Attack");
                Allies[1].SetHealth(Allies[1], DamageOfHammer);
            }
        }
        WhoAttackHammer = null;

        yield return new WaitForSeconds(START_LAUNCH_ATTACK_ENNEMY);
        FBLaunchAttack[5].SetActive(true);
        yield return new WaitForSeconds(END_LAUNCH_ATTACK_ENNEMY);
        FBLaunchAttack[5].SetActive(false);

        yield return new WaitForSeconds(WAIT_ATTACK_ENNEMY);
        LaunchOnTurn();
    }

    IEnumerator Drowned()
    {
        yield return new WaitForSeconds(WAIT_ATTACK_ENNEMY/2);
        AudioManager.Instance.PlaySeveral("Dro_Atk", 4);
        yield return new WaitForSeconds(WAIT_ATTACK_ENNEMY/2);
        if (_randomChooseDrowned == 0)
        {
            _selectedCharacter.Attack(Allies[0], "Attack");
            Allies[0].SetHealth(Allies[0], DamageOfDrowned);
            _randomChooseDrowned++;
        }
        else
        {
            _selectedCharacter.Attack(Allies[1], "Attack");
            Allies[1].SetHealth(Allies[1], DamageOfDrowned);
            _randomChooseDrowned--;
        }

        yield return new WaitForSeconds(START_LAUNCH_ATTACK_ENNEMY);
        FBLaunchAttack[4].SetActive(true);
        yield return new WaitForSeconds(END_LAUNCH_ATTACK_ENNEMY);
        FBLaunchAttack[4].SetActive(false);

        yield return new WaitForSeconds(WAIT_ATTACK_ENNEMY);
        LaunchOnTurn();
    }

    IEnumerator Squid()
    {
        yield return new WaitForSeconds(WAIT_ATTACK_ENNEMY/2);
        AudioManager.Instance.PlaySeveral("Squi_Atk", 5);
        yield return new WaitForSeconds(WAIT_ATTACK_ENNEMY/2);
        int randomTarget = Random.Range(0, 2);
        _selectedCharacter.Attack(Allies[randomTarget], "Attack");
        Allies[randomTarget].SetHealth(Allies[randomTarget], DamageOfSquid);
        //print("Allies[randomTarget] " + Allies[randomTarget]);
        yield return new WaitForSeconds(START_LAUNCH_ATTACK_ENNEMY);
        FBLaunchAttack[3].SetActive(true);
        yield return new WaitForSeconds(END_LAUNCH_ATTACK_ENNEMY);
        FBLaunchAttack[3].SetActive(false);

        yield return new WaitForSeconds(WAIT_ATTACK_ENNEMY);


        LaunchOnTurn();
    }


    public void OnPointerEnter(Character chara)
    {
        //Debug.Log("Jpasse au-dessus d'un collider : " + chara);
        //_selectedCharacter = chara;
        if (chara.Shadow.material != OutlineMat)
            chara.Shadow.material = HoverMat;

        if (!isEnter)
        {
            SndHoverCharacter(chara);
            isEnter = true;
        }
        //_hoverCharacter = chara;
    }

    public void OnPointerQuit(Character chara)
    {
        //Debug.Log("Jquitte le collider : " + chara);
        //_selectedCharacter = chara;
        if (chara == null)
        {
            return;
        }
        //Debug.Log("quit chara");
        if (chara.Shadow.material != OutlineMat)
            chara.Shadow.material = DefaultMat;
        isEnter = false;
    }

    public void SetAttackMode(int whichButton)
    {
        WhichButtonChoose = whichButton;
        Debug.Log("NumberOfPP " + _selectedCharacter.NumberOfPP);
        Debug.Log("Cout PP " + _selectedCharacter.CoutPPAttacks[WhichButtonChoose]);

        if (_selectedCharacter.NumberOfPP - _selectedCharacter.CoutPPAttacks[WhichButtonChoose] < 0)
        {
            Debug.Log("Impossible car pas assez de PP");
            _currentMode = SelectionMode.Default;
        }
        else
        {
            _currentMode = SelectionMode.Attack;
            QTE.Instance.UpdateQTE(WhichButtonChoose, _selectedCharacter);
            Debug.Log("Update le QTE");
        }

        //if (_selectedCharacter == null)
        //    return;
        //QTE.Instance.attackToChoose = whichButton;
        //isAttacking = true;
        //Debug.Log(QTE.Instance.attackToChoose);
    }

    public void SndDamageCharacter()
    {
        if (_selectedCharacter.Name == "Visco")
            AudioManager.Instance.PlaySeveral("Vis_Def", 3);
        if (_selectedCharacter.Name == "Bako")
            AudioManager.Instance.PlaySeveral("Bak_Def", 6);
        if (_selectedCharacter.Name == "Bulldog")
            AudioManager.Instance.PlaySeveral("Bull_Def", 6);
        if (_selectedCharacter.Name == "Hammer")
            AudioManager.Instance.PlaySeveral("Ham_Def", 4);
        if (_selectedCharacter.Name == "Drowned")
            AudioManager.Instance.PlaySeveral("Dro_Def", 3);
        if (_selectedCharacter.Name == "Squid")
            AudioManager.Instance.PlaySeveral("Squi_Def", 5);
    }

    public void SndHoverCharacter(Character chara)
    {
        if (chara.Name == "Visco")
            AudioManager.Instance.PlaySeveral("Vis_Hover", 5);
        if (chara.Name == "Bako")
            AudioManager.Instance.PlaySeveral("Bak_Hover", 5);
        if (chara.Name == "Bulldog")
            AudioManager.Instance.PlaySeveral("Bull_Hover", 4);
        if (chara.Name == "Hammer")
            AudioManager.Instance.PlaySeveral("Ham_Hover", 3);
        if (chara.Name == "Drowned")
            AudioManager.Instance.PlaySeveral("Dro_Hover", 3);
        if (chara.Name == "Squid")
            AudioManager.Instance.PlaySeveral("Squi_Hover", 3);
    }

    public void SndAtkMonster(Character chara)
    {
        if (chara.Name == "Bulldog")
            AudioManager.Instance.PlaySeveral("Bull_Atk", 4);
        if (chara.Name == "Hammer")
            AudioManager.Instance.PlaySeveral("Ham_Atk", 3);
        if (chara.Name == "Drowned")
            AudioManager.Instance.PlaySeveral("Dro_Atk", 3);
        if (chara.Name == "Squid")
            AudioManager.Instance.PlaySeveral("Squi_Atk", 3);
    }
}
