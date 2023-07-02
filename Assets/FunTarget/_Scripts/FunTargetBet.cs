using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class FunTargetBet : MonoBehaviour
{
    bool isCancelSpecificBet = false;
    public bool isCallWinNumAPI = false;
    public bool isGetWinNum = false;
    public bool isTake = false;
    public bool isDataNull = false;
    public bool isFunCounter = false;
    public bool isDataSendOnClick = false;

    public GameObject btnHider;

    public TMP_Text scoreTxt;
    public TMP_Text winText;
    public TMP_Text timerText;
    public TMP_Text bottomPanelMsg;
    [SerializeField] private TMP_Text bet1_text, bet2_text, bet3_text, bet4_text, bet5_text, bet6_text, bet7_text, bet8_text, bet9_text, bet0_text, showAllAmt;
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
    public int lastTransactionId;

    private void Start()
    {
        ResetBetData();

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

    #region OnClick Bet Numbers
    public void SetYourBet_Data0() // on set bet on numbers 0
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        int limtSet = allAmt + clickbetData;

        if (!isTake)
        {
            if (!isCancelSpecificBet)
            {
                if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
                {
                    if (clickbetData > 0)
                    {
                        if (betClickCounter_Data0 == 0)
                        {
                            PlayerPrefs.SetInt("data0", clickbetData);
                            bet0_text.text = clickbetData.ToString();
                            betClickCounter_Data0++;
                            tempClick_Data0 = clickbetData;
                        }
                        else if (betClickCounter_Data0 == 1)
                        {
                            tempClick_Data0 += clickbetData;
                            PlayerPrefs.SetInt("data0", tempClick_Data0);
                            bet0_text.text = tempClick_Data0.ToString();
                            betClickCounter_Data0++;
                            tempClick_Data0 = clickbetData;
                        }
                        else if (betClickCounter_Data0 > 1)
                        {
                            tempClick_Data0 += clickbetData;
                            PlayerPrefs.SetInt("data0", tempClick_Data0);
                            bet0_text.text = tempClick_Data0.ToString();
                        }
                    }
                    else
                    {
                        bet0_text.text = "";
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
                bet0_text.text = "";
                betClickCounter_Data0 = 0;
                tempClick_Data0 = 0;
                clickbetData = 0;
                PlayerPrefs.SetInt("data0", clickbetData);
            }
        }
        else
        {
            PlayBottomAnim();
        }


        AllDataAmount();
    }

    public void SetYourBet_Data1() // on set bet on numbers 1
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        int limtSet = allAmt + clickbetData;
        if (!isTake)
        {
            if (!isCancelSpecificBet)
            {
                if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
                {
                    if (clickbetData > 0)
                    {
                        if (betClickCounter_Data1 == 0)
                        {
                            bet1_text.text = clickbetData.ToString();
                            tempClick_Data1 = clickbetData;
                        }
                        else if (betClickCounter_Data1 == 1)
                        {
                            tempClick_Data1 += clickbetData;
                            betClickCounter_Data1++;
                        }
                        else if (betClickCounter_Data1 > 1)
                        {
                            tempClick_Data1 += clickbetData;
                        }
                        betClickCounter_Data1++;
                        PlayerPrefs.SetInt("data1", tempClick_Data1);
                        bet1_text.text = tempClick_Data1.ToString();
                    }
                    else
                    {
                        bet1_text.text = "";
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
                bet1_text.text = "";
                betClickCounter_Data1 = 0;
                tempClick_Data1 = 0;
                clickbetData = 0;
                PlayerPrefs.SetInt("data1", clickbetData);
            }
        }
        else
        {
            PlayBottomAnim();
        }

        AllDataAmount();
    }

    public void SetYourBet_Data2() // on set bet on numbers 2
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        int limtSet = allAmt + clickbetData;

        if (!isTake)
        {
            if (!isCancelSpecificBet)
            {
                if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
                {
                    if (clickbetData > 0)
                    {
                        if (betClickCounter_Data2 == 0)
                        {
                            bet2_text.text = clickbetData.ToString();
                            tempClick_Data2 = clickbetData;
                        }
                        else if (betClickCounter_Data2 == 1)
                        {
                            tempClick_Data2 += clickbetData;
                            betClickCounter_Data2++;
                        }
                        else if (betClickCounter_Data2 > 1)
                        {
                            tempClick_Data2 += clickbetData;
                        }
                        betClickCounter_Data2++;
                        PlayerPrefs.SetInt("data2", tempClick_Data2);
                        bet2_text.text = tempClick_Data2.ToString();
                    }
                    else
                    {
                        bet2_text.text = "";
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
                bet2_text.text = "";
                betClickCounter_Data2 = 0;
                tempClick_Data2 = 0;
                clickbetData = 0;
                PlayerPrefs.SetInt("data2", clickbetData);
            }
        }
        else
        {
            PlayBottomAnim();
        }


        AllDataAmount();
    }

    public void SetYourBet_Data3() // on set bet on numbers 3
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        int limtSet = allAmt + clickbetData;

        if (!isTake)
        {
            if (!isCancelSpecificBet)
            {
                if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
                {
                    if (clickbetData > 0)
                    {
                        if (betClickCounter_Data3 == 0)
                        {
                            bet3_text.text = clickbetData.ToString();
                            tempClick_Data3 = clickbetData;
                        }
                        else if (betClickCounter_Data3 == 1)
                        {
                            tempClick_Data3 += clickbetData;
                            betClickCounter_Data3++;
                        }
                        else if (betClickCounter_Data3 > 1)
                        {
                            tempClick_Data3 += clickbetData;
                        }
                        betClickCounter_Data3++;
                        PlayerPrefs.SetInt("data3", tempClick_Data3);
                        bet3_text.text = tempClick_Data3.ToString();
                    }
                    else
                    {
                        bet3_text.text = "";
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
                bet3_text.text = "";
                betClickCounter_Data3 = 0;
                tempClick_Data3 = 0;
                clickbetData = 0;
                PlayerPrefs.SetInt("data3", clickbetData);
            }
        }
        else
        {
            PlayBottomAnim();
        }


        AllDataAmount();
    }

    public void SetYourBet_Data4() // on set bet on numbers 4
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        int limtSet = allAmt + clickbetData;

        if (!isTake)
        {
            if (!isCancelSpecificBet)
            {
                if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
                {
                    if (clickbetData > 0)
                    {
                        if (betClickCounter_Data4 == 0)
                        {
                            bet4_text.text = clickbetData.ToString();
                            tempClick_Data4 = clickbetData;
                        }
                        else if (betClickCounter_Data4 == 1)
                        {
                            tempClick_Data4 += clickbetData;
                            betClickCounter_Data4++;
                        }
                        else if (betClickCounter_Data4 > 1)
                        {
                            tempClick_Data4 += clickbetData;
                        }
                        betClickCounter_Data4++;
                        PlayerPrefs.SetInt("data4", tempClick_Data4);
                        bet4_text.text = tempClick_Data4.ToString();
                    }
                    else
                    {
                        bet4_text.text = "";
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
                bet4_text.text = "";
                betClickCounter_Data4 = 0;
                tempClick_Data4 = 0;
                clickbetData = 0;
                PlayerPrefs.SetInt("data4", clickbetData);
            }
        }
        else
        {
            PlayBottomAnim();
        }


        AllDataAmount();
    }

    public void SetYourBet_Data5() // on set bet on numbers 5
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        int limtSet = allAmt + clickbetData;

        if (!isTake)
        {
            if (!isCancelSpecificBet)
            {
                if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
                {
                    if (clickbetData > 0)
                    {
                        if (betClickCounter_Data5 == 0)
                        {
                            bet5_text.text = clickbetData.ToString();
                            tempClick_Data5 = clickbetData;
                        }
                        else if (betClickCounter_Data5 == 1)
                        {
                            tempClick_Data5 += clickbetData;
                            betClickCounter_Data5++;
                        }
                        else if (betClickCounter_Data5 > 1)
                        {
                            tempClick_Data5 += clickbetData;
                        }
                        betClickCounter_Data5++;
                        PlayerPrefs.SetInt("data5", tempClick_Data5);
                        bet5_text.text = tempClick_Data5.ToString();
                    }
                    else
                    {
                        bet5_text.text = "";
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
                bet5_text.text = "";
                betClickCounter_Data5 = 0;
                tempClick_Data5 = 0;
                clickbetData = 0;
                PlayerPrefs.SetInt("data5", clickbetData);
            }
        }
        else
        {
            PlayBottomAnim();
        }

        AllDataAmount();
    }

    public void SetYourBet_Data6() // on set bet on numbers 6
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        int limtSet = allAmt + clickbetData;

        if (!isTake)
        {
            if (!isCancelSpecificBet)
            {
                if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
                {
                    if (clickbetData > 0)
                    {
                        if (betClickCounter_Data6 == 0)
                        {
                            bet6_text.text = clickbetData.ToString();
                            tempClick_Data6 = clickbetData;
                        }
                        else if (betClickCounter_Data6 == 1)
                        {
                            tempClick_Data6 += clickbetData;
                            betClickCounter_Data6++;
                        }
                        else if (betClickCounter_Data6 > 1)
                        {
                            tempClick_Data6 += clickbetData;
                        }
                        betClickCounter_Data6++;
                        PlayerPrefs.SetInt("data6", tempClick_Data6);
                        bet6_text.text = tempClick_Data6.ToString();
                    }
                    else
                    {
                        bet6_text.text = "";
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
                bet6_text.text = "";
                betClickCounter_Data6 = 0;
                tempClick_Data6 = 0;
                clickbetData = 0;
                PlayerPrefs.SetInt("data6", clickbetData);
            }
        }
        else
        {
            PlayBottomAnim();
        }

        AllDataAmount();
    }

    public void SetYourBet_Data7() // on set bet on numbers 7
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        int limtSet = allAmt + clickbetData;

        if (!isTake)
        {
            if (!isCancelSpecificBet)
            {
                if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
                {
                    if (clickbetData > 0)
                    {
                        if (betClickCounter_Data7 == 0)
                        {
                            bet7_text.text = clickbetData.ToString();
                            tempClick_Data7 = clickbetData;
                        }
                        else if (betClickCounter_Data7 == 1)
                        {
                            tempClick_Data7 += clickbetData;
                            betClickCounter_Data7++;
                        }
                        else if (betClickCounter_Data7 > 1)
                        {
                            tempClick_Data7 += clickbetData;
                        }
                        betClickCounter_Data7++;
                        PlayerPrefs.SetInt("data7", tempClick_Data7);
                        bet7_text.text = tempClick_Data7.ToString();
                    }
                    else
                    {
                        bet7_text.text = "";
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
                bet7_text.text = "";
                betClickCounter_Data7 = 0;
                tempClick_Data7 = 0;
                clickbetData = 0;
                PlayerPrefs.SetInt("data7", clickbetData);
            }
        }
        else
        {
            PlayBottomAnim();
        }

        AllDataAmount();
    }

    public void SetYourBet_Data8() // on set bet on numbers 8
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        int limtSet = allAmt + clickbetData;

        if (!isTake)
        {
            if (!isCancelSpecificBet)
            {
                if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
                {
                    if (clickbetData > 0)
                    {
                        if (betClickCounter_Data8 == 0)
                        {
                            bet8_text.text = clickbetData.ToString();
                            tempClick_Data8 = clickbetData;
                        }
                        else if (betClickCounter_Data8 == 1)
                        {
                            tempClick_Data8 += clickbetData;
                            betClickCounter_Data8++;
                        }
                        else if (betClickCounter_Data8 > 1)
                        {
                            tempClick_Data8 += clickbetData;
                        }
                        betClickCounter_Data8++;
                        PlayerPrefs.SetInt("data8", tempClick_Data8);
                        bet8_text.text = tempClick_Data8.ToString();
                    }
                    else
                    {
                        bet8_text.text = "";
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
                bet8_text.text = "";
                betClickCounter_Data8 = 0;
                tempClick_Data8 = 0;
                clickbetData = 0;
                PlayerPrefs.SetInt("data8", clickbetData);
            }
        }
        else
        {
            PlayBottomAnim();
        }

        AllDataAmount();
    }

    public void SetYourBet_Data9() // on set bet on numbers 9
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        int limtSet = allAmt + clickbetData;

        if (!isTake)
        {
            if (!isCancelSpecificBet)
            {
                if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
                {
                    if (clickbetData > 0)
                    {
                        if (betClickCounter_Data9 == 0)
                        {
                            bet9_text.text = clickbetData.ToString();
                            tempClick_Data9 = clickbetData;
                        }
                        else if (betClickCounter_Data9 == 1)
                        {
                            tempClick_Data9 += clickbetData;
                            betClickCounter_Data9++;
                        }
                        else if (betClickCounter_Data9 > 1)
                        {
                            tempClick_Data9 += clickbetData;
                        }
                        betClickCounter_Data9++;
                        PlayerPrefs.SetInt("data9", tempClick_Data9);
                        bet9_text.text = tempClick_Data9.ToString();
                    }
                    else
                    {
                        bet9_text.text = "";
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
                bet9_text.text = "";
                betClickCounter_Data9 = 0;
                tempClick_Data9 = 0;
                clickbetData = 0;
                PlayerPrefs.SetInt("data9", clickbetData);
            }
        }
        else
        {
            PlayBottomAnim();
        }

        AllDataAmount();
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
        ResetBetData();
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
}
