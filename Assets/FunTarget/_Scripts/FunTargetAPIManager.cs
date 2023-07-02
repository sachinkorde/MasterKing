using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

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
    public static LasttransactionId lasttransactionId;
    public static BettingData bettingData;
    public static GetDbWinNum getDbWinNum;
    public static GetResultData getResultData;

    string userID;

    private void Start()
    {
        PlayerPrefs.SetInt("userId", 1020);
        userID = PlayerPrefs.GetInt("userId").ToString();
        GetScoreAndWinScoreDataFunction();
        StartCoroutine(GetTimeData());
        //GetResultFun();
        funTargetBet.btnHider.SetActive(false);
        GetLast10WinNumbers();
    }

    #region GettimerData get_Timer
    public void StartTimerAgain()
    {
        StartCoroutine(GetTimeData());
        //GetLast10WinNumbers();
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

                        if (timerData.timer < 10)
                        {
                            //counter = true;
                            funTargetBet.timerText.text = "00:0" + timerData.timer.ToString();

                            if(!funTargetBet.isFunCounter)
                            {
                                UpdateWinNum();

                                Debug.Log(timerData.timer + "    time values senddddd");
                                funTargetBet.btnHider.SetActive(true);
                                funTargetBet.bottomPanelMsg.text = "Bet cannot be Accepted";
                                SendBetData();
                                funTargetBet.isFunCounter = true;
                                Debug.Log(funTargetBet.isFunCounter + "    timer data send Valuessssssssssssssssss");
                            }
                            else
                            {
                                yield return null;
                            }
                        }
                        else
                        {
                            funTargetBet.timerText.text = "00:" + timerData.timer.ToString();
                        }


                        if (timerData.timer == 0)
                        {
                            spinTheWheel.StartSpinButtonClick();
                            StopCoroutine(GetTimeData());
                            FT_SoundManager.instance.timerAudio.Stop();
                        }
                        else
                        {
                            if (!FT_SoundManager.instance.timerAudio.isPlaying)
                            {
                                FT_SoundManager.instance.timerAudio.Play();
                            }

                            StartCoroutine(GetTimeData());
                        }
                        break;
                }
            }
        }
    }
    #endregion

    #region TakeAPI transfer_main_wallet
    public void Take()
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        StartCoroutine(TransferToMainVallet());
        //GetScoreAndWinScoreDataFunction();
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
                        Debug.Log(takeAPI.message);
                        Debug.Log(takeAPI.main_score);
                        Debug.Log(takeAPI.wining_score);
                        Debug.Log(takeAPI.message);
                        funTargetBet.isTake = false;
                        if(takeAPI.message != "Data not found.")
                        {
                            Debug.Log(takeAPI.message);
                            funTargetBet.scoreTxt.text = takeAPI.main_score.ToString();
                            funTargetBet.winText.text = "0";
                        }
                        else
                        {
                            Debug.Log(takeAPI.message);
                            yield return null;
                        }
                        
                        
                        //funTargetBet.isTake = false;
                        break;
                }
            }
        }
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
                        Debug.Log("Netwrok 500 Error");
                        break;

                    case 200:
                         Debug.Log(scoreBoardData.main_score);
                        Debug.Log(scoreBoardData.wining_score);

                        funTargetBet.scoreTxt.text = scoreBoardData.main_score.ToString();
                        PlayerPrefs.SetInt("ft_Score", scoreBoardData.main_score);
                        break;
                }
            }
        }
    }
    #endregion

    #region Last 10 Win Num last_10_ft
    public void GetLast10WinNumbers()
    {
        StartCoroutine(GettingLast_10_WinnigNumbers());
    }

    IEnumerator GettingLast_10_WinnigNumbers()
    {
        WWWForm form = new();

        form.AddField("user_id", userID);
        form.AddField("app_token", "temp_token");
        //Debug.Log(userID + "   in last 10 Dta");
        using (UnityWebRequest www = UnityWebRequest.Post(last_10_ft, form))
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

                last_10_WinningNum = JsonUtility.FromJson<Last_10_WinningNum>(strjson);
                
                switch (last_10_WinningNum.status)
                {
                    case 500:
                        Debug.Log(www.error);
                        break;

                    case 200:
                        
                        int minLength = Mathf.Min(funTargetBet.last10WinText.Length, last_10_WinningNum.last_10_data.Length);

                        for (int i = 0; i < minLength; i++)
                        {
                            funTargetBet.last10WinText[i].text = last_10_WinningNum.last_10_data[i].random_number;


                            Debug.Log(last_10_WinningNum.last_10_data[i].random_number + "       random Numbersssssss");
                        }
                        break;
                }
            }
        }
    }
    #endregion

    #region lastTransactionIdurl unused this API
    public void GetWinScoreFunction()
    {
        StartCoroutine(GetWinScore());
    }

    IEnumerator GetWinScore()
    {
        WWWForm form = new();

        form.AddField("user_id", PlayerPrefs.GetInt("userId").ToString());
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
                Debug.Log(strjson);

                lasttransactionId = JsonUtility.FromJson<LasttransactionId>(strjson);

                switch (lasttransactionId.status)
                {
                    case 500:
                        Debug.Log(www.error);
                        break;

                    case 200:
                        Debug.Log(lasttransactionId.message);
                        Debug.Log(lasttransactionId.wining_score);

                        /*if (lasttransactionId.wining_score > 0)
                        {
                            //funTargetBet.isTake = true;
                        }
                        else
                        {
                            //funTargetBet.isTake = false;
                        }*/
                        break;
                }
            }
        }
    }
    #endregion

    #region Sending Betting Data betting_data

    public void OnClickSendBetData()
    {
        funTargetBet.btnHider.SetActive(true);
        funTargetBet.bottomPanelMsg.text = "Bet Cannot be Accepted..!";
        StartCoroutine(SendBettingData());
        funTargetBet.isDataSendOnClick = true;
    }

    public void SendBetData()
    {
        if(!funTargetBet.isDataSendOnClick)
        {
            StartCoroutine(SendBettingData());
        }
    }

    IEnumerator SendBettingData()
    {
        int sendWinNum = 0;

        sendWinNum = spinTheWheel.Winningnumber;

        Debug.Log(sendWinNum);
        WWWForm form = new();

        form.AddField("user_id", PlayerPrefs.GetInt("userId").ToString());
        form.AddField("app_token", "temp_token");
        form.AddField("data_content[data0]", PlayerPrefs.GetInt("data0"));
        form.AddField("data_content[data1]", PlayerPrefs.GetInt("data1"));
        form.AddField("data_content[data2]", PlayerPrefs.GetInt("data2"));
        form.AddField("data_content[data3]", PlayerPrefs.GetInt("data3"));
        form.AddField("data_content[data4]", PlayerPrefs.GetInt("data4"));
        form.AddField("data_content[data5]", PlayerPrefs.GetInt("data5"));
        form.AddField("data_content[data6]", PlayerPrefs.GetInt("data6"));
        form.AddField("data_content[data7]", PlayerPrefs.GetInt("data7"));
        form.AddField("data_content[data8]", PlayerPrefs.GetInt("data8"));
        form.AddField("data_content[data9]", PlayerPrefs.GetInt("data9"));
        form.AddField("wining_number", sendWinNum);


        Debug.Log(PlayerPrefs.GetInt("data0"));
        Debug.Log(PlayerPrefs.GetInt("data1"));
        Debug.Log(PlayerPrefs.GetInt("data2"));
        Debug.Log(PlayerPrefs.GetInt("data3"));
        Debug.Log(PlayerPrefs.GetInt("data4"));
        Debug.Log(PlayerPrefs.GetInt("data5"));
        Debug.Log(PlayerPrefs.GetInt("data6"));
        Debug.Log(PlayerPrefs.GetInt("data7"));
        Debug.Log(PlayerPrefs.GetInt("data8"));
        Debug.Log(PlayerPrefs.GetInt("data9"));
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
                        Debug.Log(www.error);

                        funTargetBet.isDataNull = true;
                        break;

                    case 200:
                        Debug.Log(bettingData.message);
                        Debug.Log(bettingData.transaction_id);
                        funTargetBet.isDataNull = false;
                        funTargetBet.lastTransactionId = bettingData.transaction_id;

                        PlayerPrefs.SetInt("last_transaction_id", bettingData.transaction_id);
                        //funTargetBet.ResetBetData();
                        break;
                }
            }
        }
    }
    #endregion

    #region Win Nun Send to pin Wheel gt_WinningNumFromDb
    public void UpdateWinNum()
    {
        funTargetBet.isCallWinNumAPI = true;
        StartCoroutine(GetWinNumFromDb());
    }

    IEnumerator GetWinNumFromDb()
    {
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

                switch (timerData.status)
                {
                    case 500:
                        Debug.Log(www.error);
                        funTargetBet.isGetWinNum = false;
                        break;

                    case 200:
                        Debug.Log(getDbWinNum.winning_number +  "     Get Winningngngn Nummmmmmmmmm");
                        funTargetBet.isGetWinNum = true;
                        spinTheWheel.Winningnumber = getDbWinNum.winning_number;
                        break;
                }
            }
        }
    }
    #endregion

    #region GetResult Data
    public void GetResultFun()
    {
        StartCoroutine(GetResultData());
    }

    IEnumerator GetResultData()
    {
        Debug.Log(PlayerPrefs.GetInt("last_transaction_id") + "    last trrnasaction idddddddddddddddddddddd");
        WWWForm form = new();

        form.AddField("user_id", userID);
        form.AddField("app_token", "temp_token");
        form.AddField("id", PlayerPrefs.GetInt("last_transaction_id"));

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
                        Debug.Log(getResultData.betting_data[0].main_score);
                        Debug.Log(getResultData.betting_data[0].winner_score);

                        funTargetBet.scoreTxt.text = getResultData.betting_data[0].main_score.ToString();
                        funTargetBet.winText.text = getResultData.betting_data[0].winner_score.ToString();

                        if (getResultData.betting_data[0].winner_score > 0)
                        {
                            funTargetBet.isTake = true;

                            Debug.Log("Take winning score it is greater than 0");

                            funTargetBet.bottomPanelMsg.text = "Please Take your Win Amount And Play";
                        }
                        else
                        {
                            Debug.Log("Winning score data are 0");
                            funTargetBet.bottomPanelMsg.text = "Sorry.. You are Not Win Pleae Play Again...!";
                            funTargetBet.isTake = false;
                        }

                        break;
                }
            }
        }
    }
    #endregion
}