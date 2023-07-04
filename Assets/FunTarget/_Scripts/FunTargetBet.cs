using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine.EventSystems;

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

    private void Start()
    {
        //ResetBetData();
        //funTargetAPIManager.GetResultFun();
        //PrevoiusBetStatus();
        /*Debug.Log(PlayerPrefs.GetInt("data0"));
        Debug.Log(PlayerPrefs.GetInt("data1"));
        Debug.Log(PlayerPrefs.GetInt("data2"));
        Debug.Log(PlayerPrefs.GetInt("data3"));
        Debug.Log(PlayerPrefs.GetInt("data4"));
        Debug.Log(PlayerPrefs.GetInt("data5"));
        Debug.Log(PlayerPrefs.GetInt("data6"));
        Debug.Log(PlayerPrefs.GetInt("data7"));
        Debug.Log(PlayerPrefs.GetInt("data8"));
        Debug.Log(PlayerPrefs.GetInt("data9"));*/

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
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
        }
        else
        {
            ResetBetData();
        }
    }

    public void CancelAllBet()
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        ResetBetData();
    }

    public void CancelspecificData()
    {
        isCancelSpecificBet = true;
    }

    void PlayBottomAnim()
    {
        StartCoroutine(BottomPanelAnim());
    }

    IEnumerator BottomPanelAnim()
    {
        bottomPanelMsg.text = "";
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
        //winNumToShow.Add(spinTheWheel.Winningnumber);

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
                last10WinText[i].text = winNumToShow[i].ToString();
            }
            else
            {
                last10WinText[i].text = "";
            }
        }

        Debug.Log("Showing win text data calllllllllllllllll");
    }

    #region OnClick Bet Numbers
    bool isPressingData = false;

    public void StopBetData0()
    {
        isPressingData = false;
    }

    public IEnumerator SetYourBet(int dataIndex, TMP_Text betText, int betClickCounter, int tempClickData)
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        int limitSet = allAmt + clickbetData;

        while (isPressingData)
        {
            if (!isTake)
            {
                if (!isCancelSpecificBet)
                {
                    if (PlayerPrefs.GetInt("ft_Score") >= limitSet)
                    {
                        if (clickbetData > 0)
                        {
                            if (betClickCounter == 0)
                            {
                                betText.text = clickbetData.ToString();
                                tempClickData = clickbetData;
                            }
                            else if (betClickCounter == 1)
                            {
                                tempClickData += clickbetData;
                                betClickCounter++;
                            }
                            else if (betClickCounter > 1)
                            {
                                tempClickData += clickbetData;
                            }
                            betClickCounter++;
                            PlayerPrefs.SetInt("data" + dataIndex, tempClickData);
                            betText.text = tempClickData.ToString();
                        }
                        else
                        {
                            betText.text = "";
                        }
                    }
                    else
                    {
                        bottomPanelMsg.text = "Insufficient Fund";
                    }
                }
                else
                {
                    isCancelSpecificBet = false;
                    betText.text = "";
                    betClickCounter = 0;
                    tempClickData = 0;
                    clickbetData = 0;
                    PlayerPrefs.SetInt("data" + dataIndex, clickbetData);
                }
            }
            else
            {
                PlayBottomAnim();
            }

            AllDataAmount();
            yield return new WaitForSeconds(0.35f);
        }
    }

    public void SetYourBet_Data0() // on set bet on numbers 0
    {
        if (!isPressingData)
        {
            isPressingData = true;
            StartCoroutine(SetYourBet(0, bet0_text, betClickCounter_Data0, tempClick_Data0));
        }
    }

    public void SetYourBet_Data1() // on set bet on numbers 1
    {
        if (!isPressingData)
        {
            isPressingData = true;
            StartCoroutine(SetYourBet(1, bet1_text, betClickCounter_Data1, tempClick_Data1));
        }
    }

    public void SetYourBet_Data2() // on set bet on numbers 2
    {
        if (!isPressingData)
        {
            isPressingData = true;
            StartCoroutine(SetYourBet(2, bet2_text, betClickCounter_Data2, tempClick_Data2));
        }
    }

    public void SetYourBet_Data3() // on set bet on numbers 3
    {
        if (!isPressingData)
        {
            isPressingData = true;
            StartCoroutine(SetYourBet(3, bet3_text, betClickCounter_Data3, tempClick_Data3));
        }
    }

    public void SetYourBet_Data4() // on set bet on numbers 4
    {
        if (!isPressingData)
        {
            isPressingData = true;
            StartCoroutine(SetYourBet(4, bet4_text, betClickCounter_Data4, tempClick_Data4));
        }
    }

    public void SetYourBet_Data5() // on set bet on numbers 5
    {
        if (!isPressingData)
        {
            isPressingData = true;
            StartCoroutine(SetYourBet(5, bet5_text, betClickCounter_Data5, tempClick_Data5));
        }
    }

    public void SetYourBet_Data6() // on set bet on numbers 6
    {
        if (!isPressingData)
        {
            isPressingData = true;
            StartCoroutine(SetYourBet(6, bet6_text, betClickCounter_Data6, tempClick_Data6));
        }
    }

    public void SetYourBet_Data7() // on set bet on numbers 7
    {
        if (!isPressingData)
        {
            isPressingData = true;
            StartCoroutine(SetYourBet(7, bet7_text, betClickCounter_Data7, tempClick_Data7));
        }
    }

    public void SetYourBet_Data8() // on set bet on numbers 8
    {
        if (!isPressingData)
        {
            isPressingData = true;
            StartCoroutine(SetYourBet(8, bet8_text, betClickCounter_Data8, tempClick_Data8));
        }
    }

    public void SetYourBet_Data9() // on set bet on numbers 9
    {
        if (!isPressingData)
        {
            isPressingData = true;
            StartCoroutine(SetYourBet(9, bet9_text, betClickCounter_Data9, tempClick_Data9));
        }
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
        //ResetBetData();
        SceneManager.LoadScene("GameSelection");
    }

    public void OnMouseDownEnter(Button buttonDown)
    {
        if (buttonDown.interactable == true)
        {
            //buttonDown.transform.localScale = new Vector2(1.15f, 1.15f);
            buttonDown.transform.localScale = new Vector3(1.0f, 1.0f);
        }
    }

    public void OnMouseDownExit(Button buttonExit)
    {
        if (buttonExit.interactable == true)
        {
            //buttonExit.transform.localScale = new Vector2(1f, 1f);
            buttonExit.transform.localScale = new Vector3(1.15f, 1.15f);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Handle the back button press here
            // You can perform any necessary actions or navigation logic

            SceneManager.LoadScene("GameSelection");
        }
    }
}
