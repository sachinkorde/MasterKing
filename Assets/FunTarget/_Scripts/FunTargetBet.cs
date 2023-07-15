using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class FunTargetBet : MonoBehaviour
{
    public SpinTheWheel spinTheWheel;
    public FunTargetAPIManager funTargetAPIManager;

    bool isCancelSpecificBet = false;
    public bool isCallWinNumAPI = false;
    public bool isGetWinNum = false;
    public bool isTake = false;
    public bool isDataNull = false;
    public bool isFunCounter = false;
    public bool isDataSendOnClick = false;
    public bool countdownStarted = false;

    public GameObject btnHider;

    public TMP_Text scoreTxt;
    public TMP_Text winText;
    public TMP_Text timerText;
    public TMP_Text bottomPanelMsg;
    [SerializeField] private TMP_Text bet1_text, bet2_text, bet3_text, bet4_text, bet5_text, bet6_text, bet7_text, bet8_text, bet9_text, bet0_text, showAllAmt;
    public TMP_Text[] last10WinText;

    public float timeLeft = 59f;

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
    public int lastTransactionId;

    public List<int> winNumToShow = new();
    public List<Animator> betBtn = new();

    [SerializeField] private float btnCounterValue = 0.2f;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        winNumToShow.Add(PlayerPrefs.GetInt("winNum0"));
        winNumToShow.Add(PlayerPrefs.GetInt("winNum1"));
        winNumToShow.Add(PlayerPrefs.GetInt("winNum2"));
        winNumToShow.Add(PlayerPrefs.GetInt("winNum3"));
        winNumToShow.Add(PlayerPrefs.GetInt("winNum4"));
        winNumToShow.Add(PlayerPrefs.GetInt("winNum5"));
        winNumToShow.Add(PlayerPrefs.GetInt("winNum6"));
        winNumToShow.Add(PlayerPrefs.GetInt("winNum7"));
        winNumToShow.Add(PlayerPrefs.GetInt("winNum8"));
        winNumToShow.Add(PlayerPrefs.GetInt("winNum9"));

        last10WinText[0].text = PlayerPrefs.GetInt("winNum0").ToString();
        last10WinText[1].text = PlayerPrefs.GetInt("winNum1").ToString();
        last10WinText[2].text = PlayerPrefs.GetInt("winNum2").ToString();
        last10WinText[3].text = PlayerPrefs.GetInt("winNum3").ToString();
        last10WinText[4].text = PlayerPrefs.GetInt("winNum4").ToString();
        last10WinText[5].text = PlayerPrefs.GetInt("winNum5").ToString();
        last10WinText[6].text = PlayerPrefs.GetInt("winNum6").ToString();
        last10WinText[7].text = PlayerPrefs.GetInt("winNum7").ToString();
        last10WinText[8].text = PlayerPrefs.GetInt("winNum8").ToString();
        last10WinText[9].text = PlayerPrefs.GetInt("winNum9").ToString();

    }

    public void PrevoiusBetStatus()
    {
        if (isTake)
        {
            bet1_text.text = PlayerPrefs.GetInt("data1").ToString();
            bet2_text.text = PlayerPrefs.GetInt("data2").ToString();
            bet3_text.text = PlayerPrefs.GetInt("data3").ToString();
            bet4_text.text = PlayerPrefs.GetInt("data4").ToString();
            bet5_text.text = PlayerPrefs.GetInt("data5").ToString();
            bet6_text.text = PlayerPrefs.GetInt("data6").ToString();
            bet7_text.text = PlayerPrefs.GetInt("data7").ToString();
            bet8_text.text = PlayerPrefs.GetInt("data7").ToString();
            bet8_text.text = PlayerPrefs.GetInt("data9").ToString();
            bet0_text.text = PlayerPrefs.GetInt("data0").ToString();
            showAllAmt.text = PlayerPrefs.GetInt("SetAllAmt").ToString();
        }
        else
        {
            ResetBetData();
        }
    }

    public void CancelAllBet()
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.btnClick);

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

    public void ShowWinNumInLast10Data()
    {
        if (winNumToShow.Count < last10WinText.Length)
        {
            winNumToShow.Add(spinTheWheel.Winningnumber);
        }
        else
        {
            winNumToShow[currentIndex] = spinTheWheel.Winningnumber;
            currentIndex = (currentIndex + 1) % last10WinText.Length;
        }

        for (int i = 0; i < last10WinText.Length; i++)
        {
            if (i < winNumToShow.Count)
            {
                PlayerPrefs.SetInt("winNum"+ i, winNumToShow[i]);
                last10WinText[i].text = PlayerPrefs.GetInt("winNum" + i).ToString();

                Debug.Log(PlayerPrefs.GetInt("winNum" + i) +  "    saved Nummmmmm");

            }
        }
    }

    #region OnClick Bet Numbers
    bool isPressingData = false;

    public void StopBetData0()
    {
        isPressingData = false;
    }

    private bool isButtonPressed = false;

    public void CommonMsgOnBet()
    {
        if (isTake)
        {
            PlayBottomAnim();
            return;
        }

        if (clickbetData == 0)
        {
            bottomPanelMsg.text = "Please Select Any Bet Amount";
            return;
        }
        else
        {
            bottomPanelMsg.text = "";
        }
    }

    private IEnumerator IncrementBetValue0()
    {
        int limtSet = allAmt + clickbetData;
        while (isButtonPressed)
        {
            if (clickbetData + tempClick_Data0 <= 5000 && clickbetData > 0)
            {
                if (!FT_SoundManager.instance.ft_AudioSorce.isPlaying)
                {
                    FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);
                }

                if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
                {
                    tempClick_Data0 += clickbetData;
                    PlayerPrefs.SetInt("data0", tempClick_Data0);
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
            yield return new WaitForSeconds(btnCounterValue);
        }
    }

    public void SetYourBet_Data0()
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);

        if (isCancelSpecificBet)
        {
            isCancelSpecificBet = false;
            bet0_text.text = "";
            betClickCounter_Data0 = 0;
            tempClick_Data0 = 0;
            clickbetData = 0;
            PlayerPrefs.SetInt("data0", clickbetData);
            AllDataAmount();
            return;
        }

        CommonMsgOnBet();

        StartCoroutine(IncrementBetValue0());

        if (clickbetData > 0)
        {
            betBtn[0].SetTrigger("btnClick");
        }
        else
        {
            betBtn[0].SetTrigger("idle");
        }
    }

    private IEnumerator IncrementBetValue1()
    {
        int limtSet = allAmt + clickbetData;
        while (isButtonPressed)
        {
            if (clickbetData + tempClick_Data1 <= 5000 && clickbetData > 0)
            {
                if (!FT_SoundManager.instance.ft_AudioSorce.isPlaying)
                {
                    FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);
                }

                if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
                {
                    tempClick_Data1 += clickbetData;
                    PlayerPrefs.SetInt("data1", tempClick_Data1);
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

    public void SetYourBet_Data1() // on set bet on numbers 1
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);

        if (isCancelSpecificBet)
        {
            isCancelSpecificBet = false;
            bet1_text.text = "";
            betClickCounter_Data1 = 0;
            tempClick_Data1 = 0;
            clickbetData = 0;
            PlayerPrefs.SetInt("data1", clickbetData);
            AllDataAmount();
            return;
        }

        CommonMsgOnBet();

        StartCoroutine(IncrementBetValue1());

        if (clickbetData > 0)
        {
            betBtn[1].SetTrigger("btnClick");
        }
        else
        {
            betBtn[1].SetTrigger("idle");
        }
    }

    private IEnumerator IncrementBetValue2()
    {
        int limtSet = allAmt + clickbetData;
        while (isButtonPressed)
        {
            if (clickbetData + tempClick_Data2 <= 5000 && clickbetData > 0)
            {
                if (!FT_SoundManager.instance.ft_AudioSorce.isPlaying)
                {
                    FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);
                }

                if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
                {
                    tempClick_Data2 += clickbetData;
                    PlayerPrefs.SetInt("data2", tempClick_Data2);
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

    public void SetYourBet_Data2() // on set bet on numbers 2
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);

        if (isCancelSpecificBet)
        {
            isCancelSpecificBet = false;
            bet2_text.text = "";
            betClickCounter_Data2 = 0;
            tempClick_Data2 = 0;
            clickbetData = 0;
            PlayerPrefs.SetInt("data2", clickbetData);
            AllDataAmount();
            return;
        }

        CommonMsgOnBet();

        StartCoroutine(IncrementBetValue2());

        if (clickbetData > 0)
        {
            betBtn[2].SetTrigger("btnClick");
        }
        else
        {
            betBtn[2].SetTrigger("idle");
        }
    }

    private IEnumerator IncrementBetValue3()
    {
        int limtSet = allAmt + clickbetData;
        while (isButtonPressed)
        {
            if (clickbetData + tempClick_Data3 <= 5000 && clickbetData > 0)
            {
                if (!FT_SoundManager.instance.ft_AudioSorce.isPlaying)
                {
                    FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);
                }

                if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
                {
                    tempClick_Data3 += clickbetData;
                    PlayerPrefs.SetInt("data3", tempClick_Data3);
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

    public void SetYourBet_Data3() // on set bet on numbers 3
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);

        if (isCancelSpecificBet)
        {
            isCancelSpecificBet = false;
            bet3_text.text = "";
            betClickCounter_Data3 = 0;
            tempClick_Data3 = 0;
            clickbetData = 0;
            PlayerPrefs.SetInt("data3", clickbetData);
            AllDataAmount();
            betBtn[3].SetTrigger("idle");
            return;
        }

        CommonMsgOnBet();

        StartCoroutine(IncrementBetValue3());

        if (clickbetData > 0)
        {
            betBtn[3].SetTrigger("btnClick");
        }
        else
        {
            betBtn[3].SetTrigger("idle");
        }
    }

    private IEnumerator IncrementBetValue4()
    {
        int limtSet = allAmt + clickbetData;
        while (isButtonPressed)
        {
            if (clickbetData + tempClick_Data4 <= 5000 && clickbetData > 0)
            {
                if (!FT_SoundManager.instance.ft_AudioSorce.isPlaying)
                {
                    FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);
                }

                if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
                {
                    tempClick_Data4 += clickbetData;
                    PlayerPrefs.SetInt("data4", tempClick_Data4);
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

    public void SetYourBet_Data4() // on set bet on numbers 4
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);

        if (isCancelSpecificBet)
        {
            isCancelSpecificBet = false;
            bet4_text.text = "";
            betClickCounter_Data4 = 0;
            tempClick_Data4 = 0;
            clickbetData = 0;
            PlayerPrefs.SetInt("data4", clickbetData);
            AllDataAmount();
            return;
        }

        CommonMsgOnBet();

        StartCoroutine(IncrementBetValue4());

        if (clickbetData > 0)
        {
            betBtn[4].SetTrigger("btnClick");
        }
        else
        {
            betBtn[4].SetTrigger("idle");
        }
    }

    private IEnumerator IncrementBetValue5()
    {
        int limtSet = allAmt + clickbetData;
        while (isButtonPressed)
        {
            if (clickbetData + tempClick_Data5 <= 5000 && clickbetData > 0)
            {
                if (!FT_SoundManager.instance.ft_AudioSorce.isPlaying)
                {
                    FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);
                }

                if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
                {
                    tempClick_Data5 += clickbetData;
                    PlayerPrefs.SetInt("data5", tempClick_Data5);
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

    public void SetYourBet_Data5() // on set bet on numbers 5
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);

        if (isCancelSpecificBet)
        {
            isCancelSpecificBet = false;
            bet5_text.text = "";
            betClickCounter_Data5 = 0;
            tempClick_Data5 = 0;
            clickbetData = 0;
            PlayerPrefs.SetInt("data5", clickbetData);
            AllDataAmount();
            return;
        }

        CommonMsgOnBet();

        StartCoroutine(IncrementBetValue5());

        if (clickbetData > 0)
        {
            betBtn[5].SetTrigger("btnClick");
        }
        else
        {
            betBtn[5].SetTrigger("idle");
        }
    }

    private IEnumerator IncrementBetValue6()
    {
        int limtSet = allAmt + clickbetData;
        while (isButtonPressed)
        {
            if (clickbetData + tempClick_Data6 <= 5000 && clickbetData > 0)
            {
                if (!FT_SoundManager.instance.ft_AudioSorce.isPlaying)
                {
                    FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);
                }

                if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
                {
                    tempClick_Data6 += clickbetData;
                    PlayerPrefs.SetInt("data6", tempClick_Data6);
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

    public void SetYourBet_Data6() // on set bet on numbers 6
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);

        if (isCancelSpecificBet)
        {
            isCancelSpecificBet = false;
            bet6_text.text = "";
            betClickCounter_Data6 = 0;
            tempClick_Data6 = 0;
            clickbetData = 0;
            PlayerPrefs.SetInt("data6", clickbetData);
            AllDataAmount();
            return;
        }

        CommonMsgOnBet();

        StartCoroutine(IncrementBetValue6());

        if (clickbetData > 0)
        {
            betBtn[6].SetTrigger("btnClick");
        }
        else
        {
            betBtn[6].SetTrigger("idle");
        }
    }

    private IEnumerator IncrementBetValue7()
    {
        int limtSet = allAmt + clickbetData;
        while (isButtonPressed)
        {
            if (clickbetData + tempClick_Data7 <= 5000 && clickbetData > 0)
            {
                if (!FT_SoundManager.instance.ft_AudioSorce.isPlaying)
                {
                    FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);
                }

                if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
                {
                    tempClick_Data7 += clickbetData;
                    PlayerPrefs.SetInt("data7", tempClick_Data7);
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

    public void SetYourBet_Data7() // on set bet on numbers 7
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);

        if (isCancelSpecificBet)
        {
            isCancelSpecificBet = false;
            bet7_text.text = "";
            betClickCounter_Data7 = 0;
            tempClick_Data7 = 0;
            clickbetData = 0;
            PlayerPrefs.SetInt("data7", clickbetData);
            AllDataAmount();
            return;
        }

        CommonMsgOnBet();

        StartCoroutine(IncrementBetValue7());

        if (clickbetData > 0)
        {
            betBtn[7].SetTrigger("btnClick");
        }
        else
        {
            betBtn[7].SetTrigger("idle");
        }
    }

    private IEnumerator IncrementBetValue8()
    {
        int limtSet = allAmt + clickbetData;
        while (isButtonPressed)
        {
            if (clickbetData + tempClick_Data8 <= 5000 && clickbetData > 0)
            {
                if (!FT_SoundManager.instance.ft_AudioSorce.isPlaying)
                {
                    FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);
                }

                if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
                {
                    tempClick_Data8 += clickbetData;
                    PlayerPrefs.SetInt("data8", tempClick_Data8);
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

    public void SetYourBet_Data8() // on set bet on numbers 8
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);

        if (isCancelSpecificBet)
        {
            isCancelSpecificBet = false;
            bet8_text.text = "";
            betClickCounter_Data8 = 0;
            tempClick_Data8 = 0;
            clickbetData = 0;
            PlayerPrefs.SetInt("data8", clickbetData);
            AllDataAmount();
            return;
        }

        CommonMsgOnBet();

        StartCoroutine(IncrementBetValue8());

        if (clickbetData > 0)
        {
            betBtn[8].SetTrigger("btnClick");
        }
        else
        {
            betBtn[8].SetTrigger("idle");
        }
    }

    private IEnumerator IncrementBetValue9()
    {
        int limtSet = allAmt + clickbetData;
        while (isButtonPressed)
        {
            if (clickbetData + tempClick_Data9 <= 5000 && clickbetData > 0)
            {
                if (!FT_SoundManager.instance.ft_AudioSorce.isPlaying)
                {
                    FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);
                }

                if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
                {
                    tempClick_Data9 += clickbetData;
                    PlayerPrefs.SetInt("data9", tempClick_Data9);
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

    public void SetYourBet_Data9() // on set bet on numbers 9
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Bet);

        if (isCancelSpecificBet)
        {
            isCancelSpecificBet = false;
            bet9_text.text = "";
            betClickCounter_Data9 = 0;
            tempClick_Data9 = 0;
            clickbetData = 0;
            PlayerPrefs.SetInt("data9", clickbetData);
            AllDataAmount();
            return;
        }

        CommonMsgOnBet();

        StartCoroutine(IncrementBetValue9());

        if (clickbetData > 0)
        {
            betBtn[9].SetTrigger("btnClick");
        }
        else
        {
            betBtn[9].SetTrigger("idle");
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

    void AllDataAmount()
    {
        allAmt = PlayerPrefs.GetInt("data0") + PlayerPrefs.GetInt("data1") +
                 PlayerPrefs.GetInt("data2") + PlayerPrefs.GetInt("data3") +
                 PlayerPrefs.GetInt("data4") + PlayerPrefs.GetInt("data5") +
                 PlayerPrefs.GetInt("data6") + PlayerPrefs.GetInt("data7") +
                 PlayerPrefs.GetInt("data8") + PlayerPrefs.GetInt("data9");


        showAllAmt.text = allAmt.ToString();

        int tempShowScore = 0;
        tempShowScore = PlayerPrefs.GetInt("ft_Score") - allAmt;
        scoreTxt.text = tempShowScore.ToString();
    }
    #endregion

    #region on click on bet 1,5,10,50,100,500,1000,5000
    public void SelectBetAmt(int betAmt) // on click on bet 1,5,10,50,100,500,1000,5000
    {
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
        if (PlayerPrefs.GetInt("ft_Score") > betAmt_click)
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
        PlayerPrefs.SetInt("data0", 0);
        PlayerPrefs.SetInt("data1", 0);
        PlayerPrefs.SetInt("data2", 0);
        PlayerPrefs.SetInt("data3", 0);
        PlayerPrefs.SetInt("data4", 0);
        PlayerPrefs.SetInt("data5", 0);
        PlayerPrefs.SetInt("data6", 0);
        PlayerPrefs.SetInt("data7", 0);
        PlayerPrefs.SetInt("data8", 0);
        PlayerPrefs.SetInt("data9", 0);

        PlayerPrefs.SetInt("SetAllAmt", 0);

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

    }
    #endregion

    public void LoadToGameSelection()
    {
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
            SceneManager.LoadScene("GameSelection");
        }
    }
}
