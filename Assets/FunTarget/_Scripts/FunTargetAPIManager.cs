using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class FunTargetAPIManager : MonoBehaviour
{
    public FunTargetBet funTargetBet;
    public SpinTheWheel spinTheWheel;

    public string lastTransactionIdurl = "https://godigiinfotech.com/masterking/api/last_transaction_id_ft"; // Get WinScore click on bet if winscore is greateer than 0
    public string scoreBoardURI = "https://godigiinfotech.com/masterking/api/ft_scoreboard"; // Shows updaed score
    public string betting_data = "https://godigiinfotech.com/masterking/api/betting_data"; // Send all betting data 0 to 9
    public string transfer_main_wallet = "https://godigiinfotech.com/masterking/api/ft_transfer_main_wallet"; //Take API
    public string get_result = "https://godigiinfotech.com/masterking/api/get_result";
    public string last_10_ft = "https://godigiinfotech.com/masterking/api/last_10_ft"; // Get Last 10 Wining Num and Winning score here
    public string get_Timer = "https://godigiinfotech.com/masterking/api/get_timer"; // Timer API
    public string gt_WinningNumFromDb = "https://godigiinfotech.com/masterking/api/get_winning_number"; // Getting wiining Number from Database

    public static ScoreBoardData scoreBoardData;
    public static TimerData timerData;
    public static TakeAPI takeAPI;
    public static Last_10_WinningNum last_10_WinningNum;
    public static TransactionData lasttransactionId;
    public static BettingData bettingData;
    public static GetDbWinNum getDbWinNum;
    public static GetResultData getResultData;
    public static LastTransactionIdData lastTransactionIdData;

    string userID;

    int seconds;
    int preSec;

    private void Awake()
    {
        userID = PlayerPrefs.GetInt(Const.userId).ToString();
        ShowResultWithLastTranData();
    }

    private void Start()
    {
        GetScoreAndWinScoreDataFunction();
        StartTimerAgain();
        funTargetBet.btnHider.SetActive(false);
    }

    #region Last Transaction Id
    public void ShowResultWithLastTranData()
    {
        StartCoroutine(LastTransactionIdData());
    }

    IEnumerator LastTransactionIdData()
    {
        WWWForm form = new();

        form.AddField("user_id", userID);
        form.AddField("app_token", "temp_token");

        using (UnityWebRequest www = UnityWebRequest.Post(lastTransactionIdurl, form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string strjson = www.downloadHandler.text;
                lastTransactionIdData = JsonUtility.FromJson<LastTransactionIdData>(strjson);

                switch (lastTransactionIdData.status)
                {
                    case 500:
                        Debug.Log(www.error);
                        break;

                    case 200:

                        PlayerPrefs.SetInt(Const.last_transaction_id, lastTransactionIdData.last_transaction_id);
                        OnLoadStartGetResultFun();
                        break;
                }
            }
        }
    }

    public void OnLoadStartGetResultFun()
    {
        StartCoroutine(OnLoadStartGetResultData());
    }

    void DataHandling()
    {
        int allamt;
        allamt = getResultData.betting_data[0].data1 + getResultData.betting_data[0].data2
               + getResultData.betting_data[0].data3 + getResultData.betting_data[0].data4
               + getResultData.betting_data[0].data5 + getResultData.betting_data[0].data6
               + getResultData.betting_data[0].data7 + getResultData.betting_data[0].data8
               + getResultData.betting_data[0].data9 + getResultData.betting_data[0].data0;

        funTargetBet.showAllAmt.text = allamt + ".00";

        if (getResultData.betting_data[0].data0 > 0)
        {
            funTargetBet.betBtn[0].SetTrigger("btnClick");
            funTargetBet.bet0_text.text = getResultData.betting_data[0].data0.ToString();
        }
        else
        {
            funTargetBet.betBtn[0].SetTrigger("idle");
        }

        if (getResultData.betting_data[0].data1 > 0)
        {
            funTargetBet.betBtn[1].SetTrigger("btnClick");
            funTargetBet.bet1_text.text = getResultData.betting_data[0].data1.ToString();
        }
        else
        {
            funTargetBet.betBtn[1].SetTrigger("idle");
        }

        if (getResultData.betting_data[0].data2 > 0)
        {
            funTargetBet.betBtn[2].SetTrigger("btnClick");
            funTargetBet.bet2_text.text = getResultData.betting_data[0].data2.ToString();
        }
        else
        {
            funTargetBet.betBtn[2].SetTrigger("idle");
        }

        if (getResultData.betting_data[0].data3 > 0)
        {
            funTargetBet.betBtn[3].SetTrigger("btnClick");
            funTargetBet.bet3_text.text = getResultData.betting_data[0].data3.ToString();
        }
        else
        {
            funTargetBet.betBtn[3].SetTrigger("idle");
        }

        if (getResultData.betting_data[0].data4 > 0)
        {
            funTargetBet.betBtn[4].SetTrigger("btnClick");
            funTargetBet.bet4_text.text = getResultData.betting_data[0].data4.ToString();
        }
        else
        {
            funTargetBet.betBtn[4].SetTrigger("idle");
        }

        if (getResultData.betting_data[0].data5 > 0)
        {
            funTargetBet.betBtn[5].SetTrigger("btnClick");
            funTargetBet.bet5_text.text = getResultData.betting_data[0].data5.ToString();
        }
        else
        {
            funTargetBet.betBtn[5].SetTrigger("idle");
        }

        if (getResultData.betting_data[0].data6 > 0)
        {
            funTargetBet.betBtn[6].SetTrigger("btnClick");
            funTargetBet.bet6_text.text = getResultData.betting_data[0].data6.ToString();
        }
        else
        {
            funTargetBet.betBtn[6].SetTrigger("idle");
        }

        if (getResultData.betting_data[0].data7 > 0)
        {
            funTargetBet.betBtn[7].SetTrigger("btnClick");
            funTargetBet.bet7_text.text = getResultData.betting_data[0].data7.ToString();
        }
        else
        {
            funTargetBet.betBtn[7].SetTrigger("idle");
        }

        if (getResultData.betting_data[0].data8 > 0)
        {
            funTargetBet.betBtn[8].SetTrigger("btnClick");
            funTargetBet.bet8_text.text = getResultData.betting_data[0].data8.ToString();
        }
        else
        {
            funTargetBet.betBtn[8].SetTrigger("idle");
        }

        if (getResultData.betting_data[0].data9 > 0)
        {
            funTargetBet.betBtn[9].SetTrigger("btnClick");
            funTargetBet.bet9_text.text = getResultData.betting_data[0].data9.ToString();
        }
        else
        {
            funTargetBet.betBtn[9].SetTrigger("idle");
        }
    }

    IEnumerator OnLoadStartGetResultData()
    {
        WWWForm form = new();

        form.AddField("user_id", userID);
        form.AddField("app_token", "temp_token");
        form.AddField("id", PlayerPrefs.GetInt(Const.last_transaction_id));

        using (UnityWebRequest www = UnityWebRequest.Post(get_result, form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string strjson = www.downloadHandler.text;
                getResultData = JsonUtility.FromJson<GetResultData>(strjson);

                Debug.Log(strjson);

                switch (getResultData.status)
                {
                    case 500:
                        Debug.Log(www.error);
                        break;

                    case 200:
                        Debug.Log("playerprefs wheel rotate :: " + PlayerPrefs.GetInt(Const.isWheelRotate));


                        if (getResultData.betting_data[0].is_end == 20) // Game Not End
                        {
                            DataHandling();
                            funTargetBet.isTake = false;
                            funTargetBet.takeBtn.enabled = false;
                            funTargetBet.bottomPanelMsg.text = "Bet Ok";
                            funTargetBet.btnHider.SetActive(true);
                        }
                        else if (getResultData.betting_data[0].is_end == 10)
                        {
                            if (getResultData.betting_data[0].winner_score == 0) // No need to Take Here
                            {
                                funTargetBet.isTake = false;
                                funTargetBet.takeBtn.enabled = false;
                                funTargetBet.ResetBetData();
                                funTargetBet.bottomPanelMsg.text = "";
                            }
                            else if (getResultData.betting_data[0].winner_score > 0 &&
                                     getResultData.betting_data[0].status == 0)
                            {
                                funTargetBet.isTake = true;
                                funTargetBet.takeBtn.enabled = true;
                                funTargetBet.winText.text = getResultData.betting_data[0].winner_score.ToString();
                                DataHandling();
                                funTargetBet.bottomPanelMsg.text = "Please Take your Win Amount And Play";
                            }
                            else if (getResultData.betting_data[0].winner_score > 0 &&
                                     getResultData.betting_data[0].status == 10)
                            {
                                funTargetBet.isTake = false;
                                funTargetBet.takeBtn.enabled = false;
                                funTargetBet.ResetBetData();
                                funTargetBet.bottomPanelMsg.text = "";
                            }
                        }

                        break;
                }
            }
        }
    }
    #endregion

    #region GettimerData get_Timer
    public void StartTimerAgain()
    {
        StartCoroutine(GetTimeData());
    }

    IEnumerator GetTimeData()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(get_Timer))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string strjson = www.downloadHandler.text;

                timerData = JsonUtility.FromJson<TimerData>(strjson);

                switch (timerData.status)
                {
                    case 500:
                        Debug.Log(www.error);

                        break;
                    case 200:
                        preSec = seconds;
                        seconds = timerData.timer;

                        if (seconds < preSec)
                        {
                            FT_SoundManager.instance.timerAudio.Play();
                        }

                        if (timerData.timer < 10)
                        {
                            funTargetBet.timerText.text = "00:0" + timerData.timer.ToString();
                            funTargetBet.btnHider.SetActive(true);
                            funTargetBet.bottomPanelMsg.text = "Bet Time Over..!";

                            if (!funTargetBet.isTake && !funTargetBet.isDataSendOnClick)
                            {
                                funTargetBet.isDataSendOnClick = true;
                                UpdateWinNum();
                            }
                        }
                        else
                        {
                            funTargetBet.timerText.text = "00:" + timerData.timer.ToString();
                        }

                        if (timerData.timer <= 1)
                        {
                            isSpin = true;
                        }
                        break;
                }
            }
        }

        yield return new WaitForSeconds(0.71f);
        StartCoroutine(GetTimeData());
    }

    public bool isSpin = false;
    public TMP_Text winTextTempshow;

    private void Update()
    {
        if (isSpin)
        {
            spinTheWheel.WheelSpinHere();
            PlayerPrefs.SetInt(Const.startNewGame, 0);
            PlayerPrefs.SetInt(Const.isWheelRotate, 1);
            isSpin = false;
        }
    }
    #endregion

    #region TakeAPI transfer_main_wallet
    public void Take()
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        StartCoroutine(TransferToMainVallet());
    }

    IEnumerator TransferToMainVallet()
    {
        WWWForm form = new();

        form.AddField("user_id", userID);
        form.AddField("app_token", "temp_token");

        using (UnityWebRequest www = UnityWebRequest.Post(transfer_main_wallet, form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string strjson = www.downloadHandler.text;
                takeAPI = JsonUtility.FromJson<TakeAPI>(strjson);

                switch (scoreBoardData.status)
                {
                    case 500:
                        Debug.Log(www.error);
                        break;

                    case 200:

                        funTargetBet.isTake = false;
                        funTargetBet.isBetOk = false;
                        funTargetBet.takeBtn.enabled = false;
                        funTargetBet.scoreTxt.text = takeAPI.main_score + ".00";
                        funTargetBet.winText.text = "";
                        PlayerPrefs.SetInt(Const.isWheelRotate, 1);
                        funTargetBet.PrevoiusBetStatus();
                        GetScoreAndWinScoreDataFunction();
                        PreviousBtnAnimation();
                        break;
                }
            }
        }
    }

    void PreviousBtnAnimation()
    {
        funTargetBet.previousBtn.gameObject.SetActive(true);
        funTargetBet.betokBtn.gameObject.SetActive(false);
        Invoke(nameof(DisablePreviousBtn), 7.0f);
    }

    void DisablePreviousBtn()
    {
        funTargetBet.previousBtn.gameObject.SetActive(false);
        funTargetBet.betokBtn.gameObject.SetActive(true);
        //funTargetBet.ResetOnNewBet();
    }

    #endregion

    #region ScoreData scoreBoardURI
    public void GetScoreAndWinScoreDataFunction()
    {
        StartCoroutine(GetScoreAndWinScoreData());
    }

    IEnumerator GetScoreAndWinScoreData()
    {
        WWWForm form = new();

        form.AddField("user_id", userID);
        form.AddField("app_token", "temp_token");

        using (UnityWebRequest www = UnityWebRequest.Post(scoreBoardURI, form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string strjson = www.downloadHandler.text;
                scoreBoardData = JsonUtility.FromJson<ScoreBoardData>(strjson);

                switch (scoreBoardData.status)
                {
                    case 500:
                        Debug.Log(www.error);
                        break;

                    case 200:
                        funTargetBet.scoreTxt.text = scoreBoardData.main_score + ".00";
                        PlayerPrefs.SetFloat(Const.ft_score, scoreBoardData.main_score);
                        break;
                }
            }
        }
    }
    #endregion

    #region Sending Betting Data betting_data
    public void OnClickSendBetData()
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);

        if (funTargetBet.isTake)
        {
            funTargetBet.PlayBottomAnim();
            return;
        }
        else if (!funTargetBet.isTake)
        {
            funTargetBet.btnHider.SetActive(true);
            funTargetBet.bottomPanelMsg.text = "Bet Ok..!";
            funTargetBet.isDataSendOnClick = true;
            UpdateWinNum();
        }
    }

    IEnumerator SendBettingData()
    {
        WWWForm form = new();

        form.AddField("user_id", userID);
        form.AddField("app_token", "temp_token");
        form.AddField("data_content[data0]", PlayerPrefs.GetString(Const.data0));
        form.AddField("data_content[data1]", PlayerPrefs.GetString(Const.data1));
        form.AddField("data_content[data2]", PlayerPrefs.GetString(Const.data2));
        form.AddField("data_content[data3]", PlayerPrefs.GetString(Const.data3));
        form.AddField("data_content[data4]", PlayerPrefs.GetString(Const.data4));
        form.AddField("data_content[data5]", PlayerPrefs.GetString(Const.data5));
        form.AddField("data_content[data6]", PlayerPrefs.GetString(Const.data6));
        form.AddField("data_content[data7]", PlayerPrefs.GetString(Const.data7));
        form.AddField("data_content[data8]", PlayerPrefs.GetString(Const.data8));
        form.AddField("data_content[data9]", PlayerPrefs.GetString(Const.data9));
        form.AddField("wining_number", PlayerPrefs.GetInt(Const.winNumber));

        Debug.Log("  spin win Num  : " + PlayerPrefs.GetInt(Const.winNumber));
        using (UnityWebRequest www = UnityWebRequest.Post(betting_data, form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string strjson = www.downloadHandler.text;
                Debug.Log(strjson);

                bettingData = JsonUtility.FromJson<BettingData>(strjson);

                switch (bettingData.status)
                {
                    case 500:

                        funTargetBet.isDataNull = true;
                        break;

                    case 200:

                        funTargetBet.isDataNull = false;
                        funTargetBet.isBetOk = true;
                        PlayerPrefs.SetInt(Const.isWheelRotate, 0);
                        yield return new WaitForSeconds(20);
                        ChangeWinFlag();
                        break;
                }
            }
        }
    }

    void ChangeWinFlag()
    {
        if(PlayerPrefs.GetInt(Const.isWheelRotate) == 0)
        {
            PlayerPrefs.SetInt(Const.isWheelRotate, 1);
        }
    }

    public void SendBetData()
    {
        StartCoroutine(SendBettingData());
    }
    #endregion

    #region Win Num Send to spin Wheel gt_WinningNumFromDb
    public void UpdateWinNum()
    {
        StartCoroutine(GetWinNumFromDb());
    }

    IEnumerator GetWinNumFromDb()
    {
        WWWForm form = new();

        form.AddField("user_id", userID);
        form.AddField("app_token", "temp_token");

        using (UnityWebRequest www = UnityWebRequest.Get(gt_WinningNumFromDb))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string strjson = www.downloadHandler.text;

                getDbWinNum = JsonUtility.FromJson<GetDbWinNum>(strjson);

                switch (getDbWinNum.status)
                {
                    case 500:
                        Debug.Log(www.error);
                        break;

                    case 200:

                         

                        PlayerPrefs.SetInt(Const.winNumber, getDbWinNum.winning_number);

                        //spinTheWheel.Winningnumber = PlayerPrefs.GetInt(Const.winNumber);
                        Debug.Log("funTargetBet.isDataSendOnClick value is : " + funTargetBet.isDataSendOnClick);
                        SendBetData();
                        /*if (!funTargetBet.isDataSendOnClick)
                        {
                            SendBetData();
                            funTargetBet.isDataSendOnClick = true;
                            //PlayerPrefs.SetInt(Const.isTake, 0);
                            Debug.Log("Bet Data is send to DB");
                        }*/
                        break;
                }
            }
        }
    }
    #endregion

    #region Load Previous Bet Data
    public void OnClickLoadPreviousBet()
    {
        StartCoroutine(LastTransactionIdForPreviousData());
    }

    IEnumerator LastTransactionIdForPreviousData()
    {
        WWWForm form = new();

        form.AddField("user_id", userID);
        form.AddField("app_token", "temp_token");

        using (UnityWebRequest www = UnityWebRequest.Post(lastTransactionIdurl, form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string strjson = www.downloadHandler.text;
                lastTransactionIdData = JsonUtility.FromJson<LastTransactionIdData>(strjson);

                switch (lastTransactionIdData.status)
                {
                    case 500:
                        Debug.Log(www.error);
                        break;

                    case 200:

                        PlayerPrefs.SetInt(Const.last_transaction_id, lastTransactionIdData.last_transaction_id);
                        StartCoroutine(LoadPreViousDataFromAPI());
                        break;
                }
            }
        }
    }

    IEnumerator LoadPreViousDataFromAPI()
    {
        WWWForm form = new();

        form.AddField("user_id", userID);
        form.AddField("app_token", "temp_token");
        form.AddField("id", PlayerPrefs.GetInt(Const.last_transaction_id));

        using (UnityWebRequest www = UnityWebRequest.Post(get_result, form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string strjson = www.downloadHandler.text;
                getResultData = JsonUtility.FromJson<GetResultData>(strjson);

                Debug.Log(strjson);

                switch (getResultData.status)
                {
                    case 500:
                        Debug.Log(www.error);
                        break;

                    case 200:
                        
                        int allamt;

                        allamt = getResultData.betting_data[0].data1 + getResultData.betting_data[0].data2 
                               + getResultData.betting_data[0].data3 + getResultData.betting_data[0].data4 
                               + getResultData.betting_data[0].data5 + getResultData.betting_data[0].data6
                               + getResultData.betting_data[0].data7 + getResultData.betting_data[0].data8 
                               + getResultData.betting_data[0].data9 + getResultData.betting_data[0].data0;
                            
                        if(allamt <= PlayerPrefs.GetFloat(Const.ft_score))
                        {
                          /*funTargetBet.bet1_text.text = getResultData.betting_data[0].data1.ToString();
                            funTargetBet.bet2_text.text = getResultData.betting_data[0].data2.ToString();
                            funTargetBet.bet3_text.text = getResultData.betting_data[0].data3.ToString();
                            funTargetBet.bet4_text.text = getResultData.betting_data[0].data4.ToString();
                            funTargetBet.bet5_text.text = getResultData.betting_data[0].data5.ToString();
                            funTargetBet.bet6_text.text = getResultData.betting_data[0].data6.ToString();
                            funTargetBet.bet7_text.text = getResultData.betting_data[0].data7.ToString();
                            funTargetBet.bet8_text.text = getResultData.betting_data[0].data8.ToString();
                            funTargetBet.bet9_text.text = getResultData.betting_data[0].data9.ToString();
                            funTargetBet.bet0_text.text = getResultData.betting_data[0].data0.ToString();
                            funTargetBet.showAllAmt.text = allamt.ToString();
                            */

                            DataHandling();

                            funTargetBet.tempClick_Data0 = getResultData.betting_data[0].data0;
                            funTargetBet.tempClick_Data1 = getResultData.betting_data[0].data1;
                            funTargetBet.tempClick_Data2 = getResultData.betting_data[0].data2;
                            funTargetBet.tempClick_Data3 = getResultData.betting_data[0].data3;
                            funTargetBet.tempClick_Data4 = getResultData.betting_data[0].data4;
                            funTargetBet.tempClick_Data5 = getResultData.betting_data[0].data5;
                            funTargetBet.tempClick_Data6 = getResultData.betting_data[0].data6;
                            funTargetBet.tempClick_Data7 = getResultData.betting_data[0].data7;
                            funTargetBet.tempClick_Data8 = getResultData.betting_data[0].data8;
                            funTargetBet.tempClick_Data9 = getResultData.betting_data[0].data9;

                            PlayerPrefs.SetString(Const.data0, funTargetBet.tempClick_Data0.ToString());
                            PlayerPrefs.SetString(Const.data1, funTargetBet.tempClick_Data1.ToString());
                            PlayerPrefs.SetString(Const.data2, funTargetBet.tempClick_Data2.ToString());
                            PlayerPrefs.SetString(Const.data3, funTargetBet.tempClick_Data3.ToString());
                            PlayerPrefs.SetString(Const.data4, funTargetBet.tempClick_Data4.ToString());
                            PlayerPrefs.SetString(Const.data5, funTargetBet.tempClick_Data5.ToString());
                            PlayerPrefs.SetString(Const.data6, funTargetBet.tempClick_Data6.ToString());
                            PlayerPrefs.SetString(Const.data7, funTargetBet.tempClick_Data7.ToString());
                            PlayerPrefs.SetString(Const.data8, funTargetBet.tempClick_Data8.ToString());
                            PlayerPrefs.SetString(Const.data9, funTargetBet.tempClick_Data9.ToString());
                        }
                        else
                        {
                            funTargetBet.bottomPanelMsg.text = "Insufficient Fund";
                        }
                        
                        break;
                }
            }
        }
    }
    #endregion
}

#region Json Parser code
[System.Serializable]
public class ScoreBoardData
{
    public int status;
    public string message;
    public float main_score;
    public int wining_score;
}

[System.Serializable]
public class TimerData
{
    public int status;
    public string message;
    public int timer;
}

[System.Serializable]
public class Last_Transaction_id_FT
{
    public int status;
    public string message;
    public int last_transaction_id;
    public int collection_status;
    public int transaction_status;
    public int wining_score;
}

[System.Serializable]
public class TakeAPI
{
    public int status;
    public string message;
    public float main_score;
    public int wining_score;
}

[System.Serializable]
public class Last_10_WinningFt
{
    public int status;
    public string message;
}

[System.Serializable]
public class Last_10_WinningNum
{
    public int status;
    public string message;
    public DataItem_WinNum[] last_10_data;
    public int main_score;
    public int wining_score;
}

[System.Serializable]
public class DataItem_WinNum
{
    public int id;
    public string random_number;
    public string random_symbol;
    public string created_at;
    public string created_by;
}

[System.Serializable]
public class GetResultData
{
    public int status;
    public string message;
    public betting_data[] betting_data;
}

[System.Serializable]
public class betting_data
{
    public int id;
    public int user_id;
    public int data0;
    public int data1;
    public int data2;
    public int data3;
    public int data4;
    public int data5;
    public int data6;
    public int data7;
    public int data8;
    public int data9;
    public float main_score;
    public float winner_score;
    public int wining_number;
    public int wining_status;
    public int status;
    public int is_end;
}

public class BettingData
{
    public int status;
    public string message;
    public int transaction_id;
}

[System.Serializable]
public class TransactionData
{
    public int status;
    public string message;
    public string last_transaction_id;
    public int collection_status;
    public string transaction_status;
    public string wining_score;
}

[System.Serializable]
public class GetDbWinNum
{
    public int status;
    public string message;
    public int winning_number;
}

[System.Serializable]
public class LastTransactionIdData
{
    public int status;
    public string message;
    public int last_transaction_id;
}
#endregion