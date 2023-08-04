using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FunTargetBet : MonoBehaviour
{
    public SpinTheWheel spinTheWheel;
    public FunTargetAPIManager funTargetAPIManager;

    bool isCancelSpecificBet = false;
    public bool isTake = false;
    public bool isDataNull = false;
    //public bool isDataSendOnClick = false;
    public bool countdownStarted = false;
    public bool isBetOk = false;

    public GameObject btnHider;

    public TMP_Text scoreTxt;
    public TMP_Text winText;
    public TMP_Text timerText;
    public TMP_Text bottomPanelMsg;
    public TMP_Text bet1_text, bet2_text, bet3_text, bet4_text, bet5_text, bet6_text, bet7_text, bet8_text,
                    bet9_text, bet0_text, showAllAmt;

    public TMP_Text[] last10WinText;

    public int clickbetData = 0;
    public int tempBetData = 0;
    public int clickCounter = 0;

    public int betClickCounter_Data0 = 0;
    public int tempClick_Data0 = 0;

    public int betClickCounter_Data1 = 0;
    public int tempClick_Data1 = 0;

    public int betClickCounter_Data2 = 0;
    public int tempClick_Data2 = 0;

    public int betClickCounter_Data3 = 0;
    public int tempClick_Data3 = 0;

    public int betClickCounter_Data4 = 0;
    public int tempClick_Data4 = 0;

    public int betClickCounter_Data5 = 0;
    public int tempClick_Data5 = 0;

    public int betClickCounter_Data6 = 0;
    public int tempClick_Data6 = 0;

    public int betClickCounter_Data7 = 0;
    public int tempClick_Data7 = 0;

    public int betClickCounter_Data8 = 0;
    public int tempClick_Data8 = 0;

    public int betClickCounter_Data9 = 0;
    public int tempClick_Data9 = 0;

    public int allAmt = 0;
    public int allAmtClickCounter = 0;

    //public List<int> winNumToShow = new();
    public List<Animator> betBtn = new();

    [SerializeField] private float btnCounterValue = 0.2f;
    public Button takeBtn;
    public Button previousBtn, betokBtn, cancelBtn, cancelSpecificBetBtn;
    public Animator betOkBtn, takeBtnAnimator;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public void PrevoiusBetStatus()
    {
        if (isTake || isBetOk)
        {
            bet1_text.text = PlayerPrefs.GetString(Const.data1);
            bet2_text.text = PlayerPrefs.GetString(Const.data2);
            bet3_text.text = PlayerPrefs.GetString(Const.data3);
            bet4_text.text = PlayerPrefs.GetString(Const.data4);
            bet5_text.text = PlayerPrefs.GetString(Const.data5);
            bet6_text.text = PlayerPrefs.GetString(Const.data6);
            bet7_text.text = PlayerPrefs.GetString(Const.data7);
            bet8_text.text = PlayerPrefs.GetString(Const.data8);
            bet9_text.text = PlayerPrefs.GetString(Const.data9);
            bet0_text.text = PlayerPrefs.GetString(Const.data0);
            AllDataAmount();
        }
        else
        {
            ResetBetData();
        }
    }

    public void OnClickLoadPreviousData()
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        funTargetAPIManager.OnClickLoadPreviousBet();
    }

    public void CancelAllBet()
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        betokBtn.gameObject.SetActive(false);
        betokBtn.gameObject.SetActive(true);
        if (isTake)
        {
            PlayBottomAnim();
            return;
        }

        ResetBetData();
    }

    public void CancelspecificData()
    {
        if (isTake)
        {
            PlayBottomAnim();
            return;
        }

        isCancelSpecificBet = true;
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
    }

    public void PlayBottomAnim()
    {
        StartCoroutine(BottomPanelAnim());
    }

    IEnumerator BottomPanelAnim()
    {
        bottomPanelMsg.text = "Please Take Previous Win Amount";
        bottomPanelMsg.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        bottomPanelMsg.gameObject.SetActive(false);

        InvokeRepeating(nameof(PlayBottomAnim), 1.0f, 0.0f);

        yield return new WaitForSeconds(5.0f);
        CancelInvoke(nameof(PlayBottomAnim));
        AferBottomAnimPlay();
    }

    void AferBottomAnimPlay()
    {
        bottomPanelMsg.text = "";
        bottomPanelMsg.gameObject.SetActive(true);
    }

    private int currentIndex = 0;

    /*public void ShowWinNumInLast10Data()
    {
        if (winNumToShow.Count < last10WinText.Length)
        {
            winNumToShow.Add(PlayerPrefs.GetInt(Const.winNumber));
        }
        else
        {
            winNumToShow[currentIndex] = PlayerPrefs.GetInt(Const.winNumber);
            currentIndex = (currentIndex + 1) % last10WinText.Length;
        }

        for (int i = 0; i < last10WinText.Length; i++)
        {
            if (i < winNumToShow.Count)
            {
                PlayerPrefs.SetInt("winNum"+ i, winNumToShow[i]);
                last10WinText[i].text = PlayerPrefs.GetInt("winNum" + i).ToString();
            }
        }
    }*/

    #region OnClick Bet Numbers
    bool isPressingData = false;

    public void StopBetData0()
    {
        isPressingData = false;
    }

    private bool isButtonPressed = false;

    public void CommonMsgOnBet()
    {
        if (clickbetData == 0)
        {
            bottomPanelMsg.text = "Please Select Any Bet Amount";
            BetOkIdleAnim();
            return;
        }
        else
        {
            bottomPanelMsg.text = "";
        }

        if(allAmtClickCounter == 0)
        {
            showAllAmt.text = "";
            ResetOnNewBet();
        }

        previousBtn.gameObject.SetActive(false);
        betokBtn.gameObject.SetActive(true);
    }

    public void ResetOnNewBet()
    {
        allAmtClickCounter++;
        PlayerPrefs.SetString(Const.data0, "");
        PlayerPrefs.SetString(Const.data1, "");
        PlayerPrefs.SetString(Const.data2, "");
        PlayerPrefs.SetString(Const.data3, "");
        PlayerPrefs.SetString(Const.data4, "");
        PlayerPrefs.SetString(Const.data5, "");
        PlayerPrefs.SetString(Const.data6, "");
        PlayerPrefs.SetString(Const.data7, "");
        PlayerPrefs.SetString(Const.data8, "");
        PlayerPrefs.SetString(Const.data9, "");

        PlayerPrefs.SetString(Const.SetAllAmt, "");
    }

    public void SetYourBet_Data0()
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);

        if (isTake)
        {
            PlayBottomAnim();
            return;
        }

        if (isCancelSpecificBet)
        {
            isCancelSpecificBet = false;
            bet0_text.text = "";
            betClickCounter_Data0 = 0;
            tempClick_Data0 = 0;
            clickbetData = 0;
            PlayerPrefs.SetString(Const.data0, "");
            betBtn[0].SetTrigger("idle");
            AllDataAmount();
            return;
        }

        CommonMsgOnBet();

        StartCoroutine(IncrementBetValue0());

        if (clickbetData > 0 || bet0_text.text != "")
        {
            betBtn[0].SetTrigger("btnClick");
        }
        else
        {
            betBtn[0].SetTrigger("idle");
        }
    }

    private IEnumerator IncrementBetValue0()
    {
        int limtSet = allAmt + clickbetData;

        Debug.Log(limtSet + " set limit value");
        while (isButtonPressed && PlayerPrefs.GetFloat(Const.ft_score) >= limtSet)
        {
            limtSet = allAmt + clickbetData;
            if (clickbetData + tempClick_Data0 <= 5000 && clickbetData > 0)
            {
                if (!FT_SoundManager.instance.ft_AudioSorce.isPlaying)
                {
                    FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);
                }

                if (PlayerPrefs.GetFloat(Const.ft_score) >= limtSet)
                {
                    tempClick_Data0 += clickbetData;
                    PlayerPrefs.SetString(Const.data0, tempClick_Data0.ToString());
                    bet0_text.text = tempClick_Data0.ToString();
                    betClickCounter_Data0++;
                }
                else
                {
                    bottomPanelMsg.text = "Insufficient Fund";
                }
            }
            else
            {
                bottomPanelMsg.text = "Limit 5000 to each Number";
            }
            AllDataAmount();
            yield return new WaitForSeconds(btnCounterValue);
        }
    }

    public void SetYourBet_Data1() // on set bet on numbers 1
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);

        if (isTake)
        {
            PlayBottomAnim();
            return;
        }

        if (isCancelSpecificBet)
        {
            isCancelSpecificBet = false;
            bet1_text.text = "";
            betClickCounter_Data1 = 0;
            tempClick_Data1 = 0;
            clickbetData = 0;
            PlayerPrefs.SetString(Const.data1, "");
            betBtn[1].SetTrigger("idle");
            AllDataAmount();
            return;
        }

        CommonMsgOnBet();

        StartCoroutine(IncrementBetValue1());

        if (clickbetData > 0 || bet1_text.text != "")
        {
            betBtn[1].SetTrigger("btnClick");
        }
        else
        {
            betBtn[1].SetTrigger("idle");
        }
    }

    private IEnumerator IncrementBetValue1()
    {
        int limtSet = allAmt + clickbetData;
        while (isButtonPressed && PlayerPrefs.GetFloat(Const.ft_score) >= limtSet)
        {
            limtSet = allAmt + clickbetData;
            if (clickbetData + tempClick_Data1 <= 5000 && clickbetData > 0)
            {
                if (!FT_SoundManager.instance.ft_AudioSorce.isPlaying)
                {
                    FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);
                }

                if (PlayerPrefs.GetFloat(Const.ft_score) >= limtSet)
                {
                    tempClick_Data1 += clickbetData;
                    PlayerPrefs.SetString(Const.data1, tempClick_Data1.ToString());
                    bet1_text.text = tempClick_Data1.ToString();
                    betClickCounter_Data1++;
                }
                else
                {
                    bottomPanelMsg.text = "Insufficient Fund";
                }
            }
            else
            {
                bottomPanelMsg.text = "Limit 5000 to each Number";
            }
            AllDataAmount();
            yield return new WaitForSeconds(btnCounterValue);
        }
    }

    public void SetYourBet_Data2() // on set bet on numbers 2
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);

        if (isTake)
        {
            PlayBottomAnim();
            return;
        }

        if (isCancelSpecificBet)
        {
            isCancelSpecificBet = false;
            bet2_text.text = "";
            betClickCounter_Data2 = 0;
            tempClick_Data2 = 0;
            clickbetData = 0;
            PlayerPrefs.SetString(Const.data2, "");
            betBtn[2].SetTrigger("idle");
            AllDataAmount();
            return;
        }

        CommonMsgOnBet();

        StartCoroutine(IncrementBetValue2());

        if (clickbetData > 0 || bet2_text.text != "")
        {
            betBtn[2].SetTrigger("btnClick");
        }
        else
        {
            betBtn[2].SetTrigger("idle");
        }
    }

    private IEnumerator IncrementBetValue2()
    {
        int limtSet = allAmt + clickbetData;

        while (isButtonPressed && PlayerPrefs.GetFloat(Const.ft_score) >= limtSet)
        {
            limtSet = allAmt + clickbetData;
            if (clickbetData + tempClick_Data2 <= 5000 && clickbetData > 0)
            {
                if (!FT_SoundManager.instance.ft_AudioSorce.isPlaying)
                {
                    FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);
                }

                if (PlayerPrefs.GetFloat(Const.ft_score) >= limtSet)
                {
                    tempClick_Data2 += clickbetData;
                    PlayerPrefs.SetString(Const.data2, tempClick_Data2.ToString());
                    bet2_text.text = tempClick_Data2.ToString();
                    betClickCounter_Data2++;
                }
                else
                {
                    bottomPanelMsg.text = "Insufficient Fund";
                }
            }
            else
            {
                bottomPanelMsg.text = "Limit 5000 to each Number";
            }
            AllDataAmount();
            yield return new WaitForSeconds(btnCounterValue);
        }
    }

    public void SetYourBet_Data3() // on set bet on numbers 3
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);

        if (isTake)
        {
            PlayBottomAnim();
            return;
        }

        if (isCancelSpecificBet)
        {
            isCancelSpecificBet = false;
            bet3_text.text = "";
            betClickCounter_Data3 = 0;
            tempClick_Data3 = 0;
            clickbetData = 0;
            PlayerPrefs.SetString(Const.data3, "");
            betBtn[3].SetTrigger("idle");
            AllDataAmount();
            return;
        }

        CommonMsgOnBet();

        StartCoroutine(IncrementBetValue3());

        if (clickbetData > 0 || bet3_text.text != "")
        {
            betBtn[3].SetTrigger("btnClick");
        }
        else
        {
            betBtn[3].SetTrigger("idle");
        }
    }

    private IEnumerator IncrementBetValue3()
    {
        int limtSet = allAmt + clickbetData;

        while (isButtonPressed && PlayerPrefs.GetFloat(Const.ft_score) >= limtSet)
        {
            limtSet = allAmt + clickbetData;
            if (clickbetData + tempClick_Data3 <= 5000 && clickbetData > 0)
            {
                if (!FT_SoundManager.instance.ft_AudioSorce.isPlaying)
                {
                    FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);
                }

                if (PlayerPrefs.GetFloat(Const.ft_score) >= limtSet)
                {
                    tempClick_Data3 += clickbetData;
                    PlayerPrefs.SetString(Const.data3, tempClick_Data3.ToString());
                    bet3_text.text = tempClick_Data3.ToString();
                    betClickCounter_Data3++;
                }
                else
                {
                    bottomPanelMsg.text = "Insufficient Fund";
                }
            }
            else
            {
                bottomPanelMsg.text = "Limit 5000 to each Number";
            }
            AllDataAmount();
            yield return new WaitForSeconds(btnCounterValue);
        }
    }

    public void SetYourBet_Data4() // on set bet on numbers 4
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);

        if (isTake)
        {
            PlayBottomAnim();
            return;
        }

        if (isCancelSpecificBet)
        {
            isCancelSpecificBet = false;
            bet4_text.text = "";
            betClickCounter_Data4 = 0;
            tempClick_Data4 = 0;
            clickbetData = 0;
            PlayerPrefs.SetString(Const.data4, "");
            betBtn[4].SetTrigger("idle");
            AllDataAmount();
            return;
        }

        CommonMsgOnBet();

        StartCoroutine(IncrementBetValue4());

        if (clickbetData > 0 || bet4_text.text != "")
        {
            betBtn[4].SetTrigger("btnClick");
        }
        else
        {
            betBtn[4].SetTrigger("idle");
        }
    }

    private IEnumerator IncrementBetValue4()
    {
        int limtSet = allAmt + clickbetData;

        while (isButtonPressed && PlayerPrefs.GetFloat(Const.ft_score) >= limtSet)
        {
            limtSet = allAmt + clickbetData;
            if (clickbetData + tempClick_Data4 <= 5000 && clickbetData > 0)
            {
                if (!FT_SoundManager.instance.ft_AudioSorce.isPlaying)
                {
                    FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);
                }

                if (PlayerPrefs.GetFloat(Const.ft_score) >= limtSet)
                {
                    tempClick_Data4 += clickbetData;
                    PlayerPrefs.SetString(Const.data4, tempClick_Data4.ToString());
                    bet4_text.text = tempClick_Data4.ToString();
                    betClickCounter_Data4++;
                }
                else
                {
                    bottomPanelMsg.text = "Insufficient Fund";
                }
            }
            else
            {
                bottomPanelMsg.text = "Limit 5000 to each Number";
            }
            AllDataAmount();
            yield return new WaitForSeconds(btnCounterValue);
        }
    }

    public void SetYourBet_Data5() // on set bet on numbers 5
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);

        if (isTake)
        {
            PlayBottomAnim();
            return;
        }

        if (isCancelSpecificBet)
        {
            isCancelSpecificBet = false;
            bet5_text.text = "";
            betClickCounter_Data5 = 0;
            tempClick_Data5 = 0;
            clickbetData = 0;
            PlayerPrefs.SetString(Const.data5, "");
            betBtn[5].SetTrigger("idle");
            AllDataAmount();
            return;
        }

        CommonMsgOnBet();

        StartCoroutine(IncrementBetValue5());

        if (clickbetData > 0 || bet5_text.text != "")
        {
            betBtn[5].SetTrigger("btnClick");
        }
        else
        {
            betBtn[5].SetTrigger("idle");
        }
    }

    private IEnumerator IncrementBetValue5()
    {
        int limtSet = allAmt + clickbetData;
        while (isButtonPressed && PlayerPrefs.GetFloat(Const.ft_score) >= limtSet)
        {
            limtSet = allAmt + clickbetData;
            if (clickbetData + tempClick_Data5 <= 5000 && clickbetData > 0)
            {
                if (!FT_SoundManager.instance.ft_AudioSorce.isPlaying)
                {
                    FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);
                }

                if (PlayerPrefs.GetFloat(Const.ft_score) >= limtSet)
                {
                    tempClick_Data5 += clickbetData;
                    PlayerPrefs.SetString(Const.data5, tempClick_Data5.ToString());
                    bet5_text.text = tempClick_Data5.ToString();
                    betClickCounter_Data5++;
                }
                else
                {
                    bottomPanelMsg.text = "Insufficient Fund";
                }
            }
            else
            {
                bottomPanelMsg.text = "Limit 5000 to each Number";
            }
            AllDataAmount();
            yield return new WaitForSeconds(btnCounterValue);
        }
    }

    public void SetYourBet_Data6() // on set bet on numbers 6
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);

        if (isTake)
        {
            PlayBottomAnim();
            return;
        }

        if (isCancelSpecificBet)
        {
            isCancelSpecificBet = false;
            bet6_text.text = "";
            betClickCounter_Data6 = 0;
            tempClick_Data6 = 0;
            clickbetData = 0;
            PlayerPrefs.SetString(Const.data6, "");
            betBtn[6].SetTrigger("idle");
            AllDataAmount();
            return;
        }

        CommonMsgOnBet();

        StartCoroutine(IncrementBetValue6());

        if (clickbetData > 0 || bet6_text.text != "")
        {
            betBtn[6].SetTrigger("btnClick");
        }
        else
        {
            betBtn[6].SetTrigger("idle");
        }
    }

    private IEnumerator IncrementBetValue6()
    {
        int limtSet = allAmt + clickbetData;
        while (isButtonPressed && PlayerPrefs.GetFloat(Const.ft_score) >= limtSet)
        {
            limtSet = allAmt + clickbetData;
            if (clickbetData + tempClick_Data6 <= 5000 && clickbetData > 0)
            {
                if (!FT_SoundManager.instance.ft_AudioSorce.isPlaying)
                {
                    FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);
                }

                if (PlayerPrefs.GetFloat(Const.ft_score) >= limtSet)
                {
                    tempClick_Data6 += clickbetData;
                    PlayerPrefs.SetString(Const.data6, tempClick_Data6.ToString());
                    bet6_text.text = tempClick_Data6.ToString();
                    betClickCounter_Data6++;
                }
                else
                {
                    bottomPanelMsg.text = "Insufficient Fund";
                }
            }
            else
            {
                bottomPanelMsg.text = "Limit 5000 to each Number";
            }
            AllDataAmount();
            yield return new WaitForSeconds(btnCounterValue);
        }
    }

    public void SetYourBet_Data7() // on set bet on numbers 7
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);

        if (isTake)
        {
            PlayBottomAnim();
            return;
        }

        if (isCancelSpecificBet)
        {
            isCancelSpecificBet = false;
            bet7_text.text = "";
            betClickCounter_Data7 = 0;
            tempClick_Data7 = 0;
            clickbetData = 0;
            PlayerPrefs.SetString(Const.data7, "");
            betBtn[7].SetTrigger("idle");
            AllDataAmount();
            return;
        }

        CommonMsgOnBet();

        StartCoroutine(IncrementBetValue7());

        if (clickbetData > 0 || bet7_text.text != "")
        {
            betBtn[7].SetTrigger("btnClick");
        }
        else
        {
            betBtn[7].SetTrigger("idle");
        }
    }

    private IEnumerator IncrementBetValue7()
    {
        int limtSet = allAmt + clickbetData;
        while (isButtonPressed && PlayerPrefs.GetFloat(Const.ft_score) >= limtSet)
        {
            limtSet = allAmt + clickbetData;
            if (clickbetData + tempClick_Data7 <= 5000 && clickbetData > 0)
            {
                if (!FT_SoundManager.instance.ft_AudioSorce.isPlaying)
                {
                    FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);
                }

                if (PlayerPrefs.GetFloat(Const.ft_score) >= limtSet)
                {
                    tempClick_Data7 += clickbetData;
                    PlayerPrefs.SetString(Const.data7, tempClick_Data7.ToString());
                    bet7_text.text = tempClick_Data7.ToString();
                    betClickCounter_Data7++;
                }
                else
                {
                    bottomPanelMsg.text = "Insufficient Fund";
                }
            }
            else
            {
                bottomPanelMsg.text = "Limit 5000 to each Number";
            }
            AllDataAmount();
            yield return new WaitForSeconds(btnCounterValue);
        }
    }

    public void SetYourBet_Data8() // on set bet on numbers 8
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);

        if (isTake)
        {
            PlayBottomAnim();
            return;
        }

        if (isCancelSpecificBet)
        {
            isCancelSpecificBet = false;
            bet8_text.text = "";
            betClickCounter_Data8 = 0;
            tempClick_Data8 = 0;
            clickbetData = 0;
            PlayerPrefs.SetString(Const.data8, "");
            betBtn[8].SetTrigger("idle");
            AllDataAmount();
            return;
        }

        CommonMsgOnBet();

        StartCoroutine(IncrementBetValue8());

        if (clickbetData > 0 || bet8_text.text != "")
        {
            betBtn[8].SetTrigger("btnClick");
        }
        else
        {
            betBtn[8].SetTrigger("idle");
        }
    }

    private IEnumerator IncrementBetValue8()
    {
        int limtSet = allAmt + clickbetData;

        while (isButtonPressed && PlayerPrefs.GetFloat(Const.ft_score) >= limtSet)
        {
            limtSet = allAmt + clickbetData;
            if (clickbetData + tempClick_Data8 <= 5000 && clickbetData > 0)
            {
                if (!FT_SoundManager.instance.ft_AudioSorce.isPlaying)
                {
                    FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);
                }

                if (PlayerPrefs.GetFloat(Const.ft_score) >= limtSet)
                {
                    tempClick_Data8 += clickbetData;
                    PlayerPrefs.SetString(Const.data8, tempClick_Data8.ToString());
                    bet8_text.text = tempClick_Data8.ToString();
                    betClickCounter_Data8++;
                }
                else
                {
                    bottomPanelMsg.text = "Insufficient Fund";
                }
            }
            else
            {
                bottomPanelMsg.text = "Limit 5000 to each Number";
            }
            AllDataAmount();
            yield return new WaitForSeconds(btnCounterValue);
        }
    }

    public void SetYourBet_Data9() // on set bet on numbers 9
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);

        if (isTake)
        {
            PlayBottomAnim();
            return;
        }

        if (isCancelSpecificBet)
        {
            isCancelSpecificBet = false;
            bet9_text.text = "";
            betClickCounter_Data9 = 0;
            tempClick_Data9 = 0;
            clickbetData = 0;
            PlayerPrefs.SetString(Const.data9, "");
            betBtn[9].SetTrigger("idle");
            AllDataAmount();
            return;
        }

        CommonMsgOnBet();

        StartCoroutine(IncrementBetValue9());

        if (clickbetData > 0 || bet9_text.text != "")
        {
            betBtn[9].SetTrigger("btnClick");
        }
        else
        {
            betBtn[9].SetTrigger("idle");
        }
    }

    private IEnumerator IncrementBetValue9()
    {
        int limtSet = allAmt + clickbetData;
        while (isButtonPressed && PlayerPrefs.GetFloat(Const.ft_score) >= limtSet)
        {
            limtSet = allAmt + clickbetData;
            if (clickbetData + tempClick_Data9 <= 5000 && clickbetData > 0)
            {
                if (!FT_SoundManager.instance.ft_AudioSorce.isPlaying)
                {
                    FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);
                }

                if (PlayerPrefs.GetFloat(Const.ft_score) >= limtSet)
                {
                    tempClick_Data9 += clickbetData;
                    PlayerPrefs.SetString(Const.data9, tempClick_Data9.ToString());
                    bet9_text.text = tempClick_Data9.ToString();
                    betClickCounter_Data9++;
                }
                else
                {
                    bottomPanelMsg.text = "Insufficient Fund";
                }
            }
            else
            {
                bottomPanelMsg.text = "Limit 5000 to each Number";
            }
            AllDataAmount();
            yield return new WaitForSeconds(btnCounterValue);
        }
    }

    public void OnButtonDown()
    {
        isButtonPressed = true;
    }

    public void OnButtonUp()
    {
        isButtonPressed = false;
    }

    public void BetBtnAnimation()
    {
        betokBtn.enabled = true;
        cancelBtn.enabled = true;
        cancelSpecificBetBtn.enabled = true;
        betOkBtn.SetTrigger("betokanim");
    }

    public void BetOkIdleAnim()
    {
        betokBtn.enabled = false;
        cancelBtn.enabled = false;
        cancelSpecificBetBtn.enabled = false;
        betOkBtn.SetTrigger("idle");
    }

    public void TakeBtnDisabledState()
    {
        isTake = false;
        takeBtn.enabled = false;
        takeBtnAnimator.SetTrigger("idle");
    }

    public void TakeBtnEnbledState()
    {
        takeBtnAnimator.SetTrigger("takebtnokanim");
        isTake = true;
        takeBtn.enabled = true;
        BetOkIdleAnim(); 
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Win);
    }

    void AllDataAmount()
    {
        allAmt = tempClick_Data0 + tempClick_Data1 + tempClick_Data2 + tempClick_Data3
               + tempClick_Data4 + tempClick_Data5 + tempClick_Data6 + tempClick_Data7 
               + tempClick_Data8 + tempClick_Data9;

        BetBtnAnimation();
        showAllAmt.text = allAmt + ".00";
        PlayerPrefs.SetString("SetAllAmt", allAmt.ToString());
        float tempShowScore;
        tempShowScore = PlayerPrefs.GetFloat(Const.ft_score) - allAmt;
        scoreTxt.text = tempShowScore + ".00";
    }
    #endregion

    #region on click on bet 1,5,10,50,100,500,1000,5000

    public void SelectBetAmt(int betAmt) // on click on bet 1,5,10,50,100,500,1000,5000
    {
        previousBtn.gameObject.SetActive(false);
        betokBtn.gameObject.SetActive(true);

        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);

        switch (betAmt)
        {
            case 1:
                OnClickBetAmt(betAmt);
                clickbetData = betAmt;

                break;

            case 5:
                OnClickBetAmt(betAmt);
                clickbetData = betAmt;
                break;

            case 10:
                OnClickBetAmt(betAmt);
                clickbetData = betAmt;
                break;

            case 50:
                OnClickBetAmt(betAmt);
                clickbetData = betAmt;
                break;

            case 100:
                OnClickBetAmt(betAmt);
                clickbetData = betAmt;
                break;

            case 500:
                OnClickBetAmt(betAmt);
                clickbetData = betAmt;
                break;

            case 1000:
                OnClickBetAmt(betAmt);
                clickbetData = betAmt;
                break;

            case 5000:
                OnClickBetAmt(betAmt);
                clickbetData = betAmt;
                break;
        }
        clickCounter = 0;
    }

    public void OnClickBetAmt(int betAmt_click)
    {
        if (PlayerPrefs.GetInt(Const.startNewGame) == 0)
        {
            ResetBetDataOnWinZero();
            for (int i = 0; i < betBtn.Count; i++)
            {
                betBtn[i].SetTrigger("idle");
            }
            PlayerPrefs.SetInt(Const.startNewGame, 1);
        }

        if (PlayerPrefs.GetFloat(Const.ft_score) > betAmt_click)
        {
            tempBetData = betAmt_click;
        }
        else
        {
            bottomPanelMsg.text = "Insufficient Fund";
        }
    }
    #endregion

    #region Resetbet Data
    public void ResetBetData()
    {
        clickbetData = 0;
        tempBetData = 0;
        clickCounter = 0;

        betClickCounter_Data0 = 0;
        tempClick_Data0 = 0;

        betClickCounter_Data1 = 0;
        tempClick_Data1 = 0;

        betClickCounter_Data2 = 0;
        tempClick_Data2 = 0;

        betClickCounter_Data3 = 0;
        tempClick_Data3 = 0;

        betClickCounter_Data4 = 0;
        tempClick_Data4 = 0;

        betClickCounter_Data5 = 0;
        tempClick_Data5 = 0;

        betClickCounter_Data6 = 0;
        tempClick_Data6 = 0;

        betClickCounter_Data7 = 0;
        tempClick_Data7 = 0;

        betClickCounter_Data8 = 0;
        tempClick_Data8 = 0;

        betClickCounter_Data9 = 0;
        tempClick_Data9 = 0;

        allAmt = 0;

        bet1_text.text = "";
        bet2_text.text = "";
        bet3_text.text = "";
        bet4_text.text = "";
        bet5_text.text = "";
        bet6_text.text = "";
        bet7_text.text = "";
        bet8_text.text = "";
        bet9_text.text = "";
        bet0_text.text = "";
        showAllAmt.text = "";
        BetOkIdleAnim();
        ResetBetDataOnWinZero();
    }

    void ResetBetDataOnWinZero()
    {
        PlayerPrefs.SetString(Const.data0, "");
        PlayerPrefs.SetString(Const.data1, "");
        PlayerPrefs.SetString(Const.data2, "");
        PlayerPrefs.SetString(Const.data3, "");
        PlayerPrefs.SetString(Const.data4, "");
        PlayerPrefs.SetString(Const.data5, "");
        PlayerPrefs.SetString(Const.data6, "");
        PlayerPrefs.SetString(Const.data7, "");
        PlayerPrefs.SetString(Const.data8, "");
        PlayerPrefs.SetString(Const.data9, "");

        PlayerPrefs.SetString(Const.SetAllAmt, "");

        for (int i = 0; i < betBtn.Count; i++)
        {
            betBtn[i].SetTrigger("idle");
        }
    }
    #endregion

    public void LoadToGameSelection()
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        SceneManager.LoadScene("GameSelection");
    }

    public void OnMouseDownEnter(Button buttonDown)
    {
        if (buttonDown.interactable == true)
        {
            buttonDown.transform.localScale = new Vector3(1.0f, 1.0f);
        }
    }

    public void OnMouseDownExit(Button buttonExit)
    {
        if (buttonExit.interactable == true)
        {
            buttonExit.transform.localScale = new Vector3(1.15f, 1.15f);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
            SceneManager.LoadScene(Const.GameSelection);
        }
    }
}
