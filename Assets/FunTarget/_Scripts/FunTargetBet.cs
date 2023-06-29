using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FunTargetBet : MonoBehaviour
{
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

    [SerializeField] private TMP_Text bet1_text, bet2_text, bet3_text,
                                      bet4_text, bet5_text, bet6_text,
                                      bet7_text, bet8_text, bet9_text,
                                      bet0_text, showAllAmt;

    bool isCancelSpecificBet = false;

    private void Start()
    {
        ResetBetData();
    }
  
    public void CancelAllBet()
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        ResetBetData();
    }

    public void LoadToGameSelection()
    {
        ResetBetData();
        SceneManager.LoadScene("GameSelection");
    }

    public void CancelspecificData()
    {
        isCancelSpecificBet = true;
    }

    public void SetYourBet_Data0() // on set bet on numbers 0
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        int limtSet = allAmt + clickbetData;

        if (!isCancelSpecificBet)
        {
            if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
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
                Debug.Log("Insufficient Fund");
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
        
        AllDataAmount();
    }

    public void SetYourBet_Data1() // on set bet on numbers 1
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        int limtSet = allAmt + clickbetData;

        if(!isCancelSpecificBet)
        {
            if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
            {
                if (betClickCounter_Data1 == 0)
                {
                    PlayerPrefs.SetInt("data1", clickbetData);
                    bet1_text.text = clickbetData.ToString();
                    betClickCounter_Data1++;
                    tempClick_Data1 = clickbetData;
                }
                else if (betClickCounter_Data1 == 1)
                {
                    tempClick_Data1 += clickbetData;
                    PlayerPrefs.SetInt("data1", tempClick_Data1);
                    bet1_text.text = tempClick_Data1.ToString();
                    betClickCounter_Data1++;
                    tempClick_Data1 = clickbetData;
                }
                else if (betClickCounter_Data1 > 1)
                {
                    tempClick_Data1 += clickbetData;
                    PlayerPrefs.SetInt("data1", tempClick_Data1);
                    bet1_text.text = tempClick_Data1.ToString();
                }
            }
            else
            {
                Debug.Log("Insufficient Fund");
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
        
        AllDataAmount();
    }

    public void SetYourBet_Data2() // on set bet on numbers 2
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        int limtSet = allAmt + clickbetData;

        if(!isCancelSpecificBet) 
        {
            if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
            {
                if (betClickCounter_Data2 == 0)
                {
                    PlayerPrefs.SetInt("data2", clickbetData);
                    bet2_text.text = clickbetData.ToString();
                    betClickCounter_Data2++;
                    tempClick_Data2 = clickbetData;
                }
                else if (betClickCounter_Data2 == 1)
                {
                    tempClick_Data2 += clickbetData;
                    PlayerPrefs.SetInt("data2", tempClick_Data2);
                    bet2_text.text = tempClick_Data2.ToString();
                    betClickCounter_Data2++;
                    tempClick_Data2 = clickbetData;
                }
                else if (betClickCounter_Data2 > 1)
                {
                    tempClick_Data2 += clickbetData;
                    PlayerPrefs.SetInt("data2", tempClick_Data2);
                    bet2_text.text = tempClick_Data2.ToString();
                }
            }
            else
            {
                Debug.Log("Insufficient Fund");
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
        

        AllDataAmount();
    }

    public void SetYourBet_Data3() // on set bet on numbers 3
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        int limtSet = allAmt + clickbetData;

        if(!isCancelSpecificBet) 
        {
            if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
            {
                if (betClickCounter_Data3 == 0)
                {
                    PlayerPrefs.SetInt("data3", clickbetData);
                    bet3_text.text = clickbetData.ToString();
                    betClickCounter_Data3++;
                    tempClick_Data3 = clickbetData;
                }
                else if (betClickCounter_Data3 == 1)
                {
                    tempClick_Data3 += clickbetData;
                    PlayerPrefs.SetInt("data3", tempClick_Data3);
                    bet3_text.text = tempClick_Data3.ToString();
                    betClickCounter_Data3++;
                    tempClick_Data3 = clickbetData;
                }
                else if (betClickCounter_Data3 > 1)
                {
                    tempClick_Data3 += clickbetData;
                    PlayerPrefs.SetInt("data3", tempClick_Data3);
                    bet3_text.text = tempClick_Data3.ToString();
                }
            }
            else
            {
                Debug.Log("Insufficient Fund");
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
        

        AllDataAmount();
    }

    public void SetYourBet_Data4() // on set bet on numbers 4
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        int limtSet = allAmt + clickbetData;

        if (!isCancelSpecificBet)
        {
            if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
            {
                if (betClickCounter_Data4 == 0)
                {
                    PlayerPrefs.SetInt("data4", clickbetData);
                    bet4_text.text = clickbetData.ToString();
                    betClickCounter_Data4++;
                    tempClick_Data4 = clickbetData;
                }
                else if (betClickCounter_Data4 == 1)
                {
                    tempClick_Data4 += clickbetData;
                    PlayerPrefs.SetInt("data4", tempClick_Data4);
                    bet4_text.text = tempClick_Data4.ToString();
                    betClickCounter_Data4++;
                    tempClick_Data4 = clickbetData;
                }
                else if (betClickCounter_Data4 > 1)
                {
                    tempClick_Data4 += clickbetData;
                    PlayerPrefs.SetInt("data4", tempClick_Data4);
                    bet4_text.text = tempClick_Data4.ToString();
                }
            }
            else
            {
                Debug.Log("Insufficient Fund");
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
        

        AllDataAmount();
    }

    public void SetYourBet_Data5() // on set bet on numbers 5
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        int limtSet = allAmt + clickbetData;

        if (!isCancelSpecificBet)
        {
            if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
            {
                if (betClickCounter_Data5 == 0)
                {
                    PlayerPrefs.SetInt("data5", clickbetData);
                    bet5_text.text = clickbetData.ToString();
                    betClickCounter_Data5++;
                    tempClick_Data5 = clickbetData;
                }
                else if (betClickCounter_Data5 == 1)
                {
                    tempClick_Data5 += clickbetData;
                    PlayerPrefs.SetInt("data5", tempClick_Data5);
                    bet5_text.text = tempClick_Data5.ToString();
                    betClickCounter_Data5++;
                    tempClick_Data5 = clickbetData;
                }
                else if (betClickCounter_Data5 > 1)
                {
                    tempClick_Data5 += clickbetData;
                    PlayerPrefs.SetInt("data5", tempClick_Data5);
                    bet5_text.text = tempClick_Data5.ToString();
                }
            }
            else
            {
                Debug.Log("Insufficient Fund");
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
        

        AllDataAmount();
    }

    public void SetYourBet_Data6() // on set bet on numbers 6
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        int limtSet = allAmt + clickbetData;

        if (!isCancelSpecificBet)
        {
            if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
            {
                if (betClickCounter_Data6 == 0)
                {
                    PlayerPrefs.SetInt("data6", clickbetData);
                    bet6_text.text = clickbetData.ToString();
                    betClickCounter_Data6++;
                    tempClick_Data6 = clickbetData;
                }
                else if (betClickCounter_Data6 == 1)
                {
                    tempClick_Data6 += clickbetData;
                    PlayerPrefs.SetInt("data6", tempClick_Data6);
                    bet6_text.text = tempClick_Data6.ToString();
                    betClickCounter_Data6++;
                    tempClick_Data6 = clickbetData;
                }
                else if (betClickCounter_Data6 > 1)
                {
                    tempClick_Data6 += clickbetData;
                    PlayerPrefs.SetInt("data6", tempClick_Data6);
                    bet6_text.text = tempClick_Data6.ToString();
                }
            }
            else
            {
                Debug.Log("Insufficient Fund");
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
        

        AllDataAmount();
    }

    public void SetYourBet_Data7() // on set bet on numbers 7
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        int limtSet = allAmt + clickbetData;

        if(!isCancelSpecificBet) 
        {
            if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
            {
                if (betClickCounter_Data7 == 0)
                {
                    PlayerPrefs.SetInt("data7", clickbetData);
                    bet7_text.text = clickbetData.ToString();
                    betClickCounter_Data7++;
                    tempClick_Data7 = clickbetData;
                }
                else if (betClickCounter_Data7 == 1)
                {
                    tempClick_Data7 += clickbetData;
                    PlayerPrefs.SetInt("data7", tempClick_Data7);
                    bet7_text.text = tempClick_Data7.ToString();
                    betClickCounter_Data7++;
                    tempClick_Data7 = clickbetData;
                }
                else if (betClickCounter_Data7 > 1)
                {
                    tempClick_Data7 += clickbetData;
                    PlayerPrefs.SetInt("data7", tempClick_Data7);
                    bet7_text.text = tempClick_Data7.ToString();
                }
            }
            else
            {
                Debug.Log("Insufficient Fund");
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
        
        AllDataAmount();
    }

    public void SetYourBet_Data8() // on set bet on numbers 8
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        int limtSet = allAmt + clickbetData;

        if(!isCancelSpecificBet)
        {
            if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
            {
                if (betClickCounter_Data8 == 0)
                {
                    PlayerPrefs.SetInt("data8", clickbetData);
                    bet8_text.text = clickbetData.ToString();
                    betClickCounter_Data8++;
                    tempClick_Data8 = clickbetData;
                }
                else if (betClickCounter_Data8 == 1)
                {
                    tempClick_Data8 += clickbetData;
                    PlayerPrefs.SetInt("data8", tempClick_Data8);
                    bet8_text.text = tempClick_Data8.ToString();
                    betClickCounter_Data8++;
                    tempClick_Data8 = clickbetData;
                }
                else if (betClickCounter_Data8 > 1)
                {
                    tempClick_Data8 += clickbetData;
                    PlayerPrefs.SetInt("data8", tempClick_Data8);
                    bet8_text.text = tempClick_Data8.ToString();
                }
            }
            else
            {
                Debug.Log("Insufficient Fund");
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

        AllDataAmount();
    }

    public void SetYourBet_Data9() // on set bet on numbers 9
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        int limtSet = allAmt + clickbetData;

        if(!isCancelSpecificBet)
        {
            if (PlayerPrefs.GetInt("ft_Score") >= limtSet)
            {
                if (betClickCounter_Data9 == 0)
                {
                    PlayerPrefs.SetInt("data9", clickbetData);
                    bet9_text.text = clickbetData.ToString();
                    betClickCounter_Data9++;
                    tempClick_Data9 = clickbetData;
                }
                else if (betClickCounter_Data9 == 1)
                {
                    tempClick_Data9 += clickbetData;
                    PlayerPrefs.SetInt("data9", tempClick_Data9);
                    bet9_text.text = tempClick_Data9.ToString();
                    betClickCounter_Data9++;
                    tempClick_Data9 = clickbetData;
                }
                else if (betClickCounter_Data9 > 1)
                {
                    tempClick_Data9 += clickbetData;
                    PlayerPrefs.SetInt("data9", tempClick_Data9);
                    bet9_text.text = tempClick_Data9.ToString();
                }
            }
            else
            {
                Debug.Log("Insufficient Fund");
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
        
        AllDataAmount();
    }

    void AllDataAmount()
    {
        allAmt = PlayerPrefs.GetInt("data0") + PlayerPrefs.GetInt("data1") +
                 PlayerPrefs.GetInt("data2") + PlayerPrefs.GetInt("data3") +
                 PlayerPrefs.GetInt("data4") + PlayerPrefs.GetInt("data5") +
                 PlayerPrefs.GetInt("data6") + PlayerPrefs.GetInt("data7") +
                 PlayerPrefs.GetInt("data8") + PlayerPrefs.GetInt("data9");

        //PlayerPrefs.SetInt("SetAllAmt", allAmt);
        //Debug.Log(PlayerPrefs.GetInt("SetAllAmt") + "    saveddd alll amtttttt");
        Debug.Log(allAmt + "       temp amt hereeeeeeee");

        //showAllAmt.text = PlayerPrefs.GetInt("SetAllAmt").ToString();
        showAllAmt.text = allAmt.ToString();
    }

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
        Debug.Log(clickbetData);
        Debug.Log(betAmt);
    }

    public void OnClickBetAmt(int betAmt_click)
    {
        Debug.Log(PlayerPrefs.GetInt("ft_Score"));

        if (PlayerPrefs.GetInt("ft_Score") > betAmt_click)
        {
            tempBetData = betAmt_click;

            Debug.Log(tempBetData);

            Debug.Log(betAmt_click);
        }
        else
        {
            Debug.Log("Insufficient fund");
        }
    }

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
}
