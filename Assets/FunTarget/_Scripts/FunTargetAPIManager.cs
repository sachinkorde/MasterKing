using System.Collections;
using TMPro;
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

    string userID;

    private void Awake()
    {
        PlayerPrefs.SetInt("userId", 1020);
        userID = PlayerPrefs.GetInt("userId").ToString();

        OnLoadStartGetResultFun();
    }

    private void Start()
    {
        GetScoreAndWinScoreDataFunction();
        //StartCoroutine(GetTimeData());
        //GetResultFun();
        StartTimerAgain();
        funTargetBet.btnHider.SetActive(false);
        //GetLast10WinNumbers();
    }

    #region GettimerData get_Timer
    
    public void StartTimerAgain()
    {
        funTargetBet.countdownStarted = true;
        //funTargetBet.timeLeft = 60;
        //StartCoroutine(StartCountdown());
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

                        if (timerData.timer < 10)
                        {
                            //counter = true;
                            funTargetBet.timerText.text = "00:0" + timerData.timer.ToString();

                            if (!funTargetBet.isFunCounter)
                            {
                                UpdateWinNum();

                                funTargetBet.btnHider.SetActive(true);
                                funTargetBet.bottomPanelMsg.text = "Bet cannot be Accepted";

                                if (!funTargetBet.isTake)
                                {
                                    SendBetData();
                                }
                                
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
                            //spinTheWheel.StartSpinButtonClick();
                            funTargetBet.timerText.text = "00" + ":" + "00";
                            funTargetBet.countdownStarted = false;

                            spinTheWheel.WheelSpinHere();
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

                        Debug.Log(takeAPI.message);
                        funTargetBet.scoreTxt.text = takeAPI.main_score.ToString();
                        funTargetBet.winText.text = "0";

                        funTargetBet.PrevoiusBetStatus();

                        /*if (takeAPI.message != "Data not found.")
                        {
                            
                        }
                        else
                        {
                            Debug.Log(takeAPI.message);
                            yield return null;
                        }*/


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
                        Debug.Log(scoreBoardData.wining_score   +   "     winning score data is available");

                        funTargetBet.scoreTxt.text = scoreBoardData.main_score.ToString();
                        PlayerPrefs.SetInt("ft_Score", scoreBoardData.main_score);
                        break;
                }
            }
        }
    }
    #endregion

    #region Last 10 Win Num last_10_ft
    /*public void GetLast10WinNumbers()
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
    }*/
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

                lasttransactionId = JsonUtility.FromJson<TransactionData>(strjson);

                switch (lasttransactionId.status)
                {
                    case 500:
                        Debug.Log(www.error);
                        break;

                    case 200:
                        Debug.Log(lasttransactionId.message);
                        Debug.Log(lasttransactionId.wining_score);

                        break;
                }
            }
        }
    }
    #endregion

    #region Sending Betting Data betting_data

    public void OnClickSendBetData()
    {
        if (funTargetBet.isTake)
        {
            funTargetBet.PlayBottomAnim();
            return;
        }

        funTargetBet.btnHider.SetActive(true);
        funTargetBet.bottomPanelMsg.text = "Bet Cannot be Accepted..!";
        StartCoroutine(SendBettingData());
        funTargetBet.isDataSendOnClick = true;
    }

    public void SendBetData()
    {
        if (!funTargetBet.isDataSendOnClick)
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

                switch (getDbWinNum.status)
                {
                    case 500:
                        Debug.Log(www.error);
                        funTargetBet.isGetWinNum = false;
                        break;

                    case 200:
                        Debug.Log(getDbWinNum.winning_number + "     ggggggggggggggggggGet Winningngngn Nummmmmmmmmm");
                        funTargetBet.isGetWinNum = true;
                        spinTheWheel.Winningnumber = getDbWinNum.winning_number;
                        break;
                }
            }
        }
    }
    #endregion

    #region GetResult Data
    public void OnLoadStartGetResultFun()
    {
        StartCoroutine(OnLoadStartGetResultData());
    }

    IEnumerator OnLoadStartGetResultData()
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

                        if (getResultData.betting_data[0].status == 10)
                        {
                            Debug.Log("Winning score daaaaaaaaaaaaaaaaataaaaaaaaaaaaa are 0");
                            //funTargetBet.bottomPanelMsg.text = "Sorry.. You are Not Win Pleae Play Again...!";
                            funTargetBet.isTake = false;
                            funTargetBet.ResetBetData();
                        }
                        else if(getResultData.betting_data[0].winner_score > 0)
                        {
                            funTargetBet.isTake = true;

                            Debug.Log("Take winning score it is greater than 0");
                            funTargetBet.PrevoiusBetStatus();
                            funTargetBet.scoreTxt.text = getResultData.betting_data[0].main_score.ToString();
                            funTargetBet.winText.text = getResultData.betting_data[0].winner_score.ToString();
                            funTargetBet.bottomPanelMsg.text = "Please Take your Win Amount And Play";
                        }

                        break;
                }
            }
        }
    }

    public void GetResultFunInGame()
    {
        StartCoroutine(GetResultDataAfterGame());
    }

    IEnumerator GetResultDataAfterGame()
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

                        if(getResultData.betting_data[0].winner_score > 0)
                        {
                            funTargetBet.isTake = true;

                            Debug.Log("Take winning score it is greater than 0");
                            funTargetBet.PrevoiusBetStatus();
                            funTargetBet.scoreTxt.text = getResultData.betting_data[0].main_score.ToString();
                            funTargetBet.winText.text = getResultData.betting_data[0].winner_score.ToString();
                            funTargetBet.bottomPanelMsg.text = "Please Take your Win Amount And Play";
                        }
                        else
                        {
                            Debug.Log("Winning score data are 0");
                            funTargetBet.bottomPanelMsg.text = "Sorry.. You are Not Win Please Play Again...!";
                            funTargetBet.isTake = false;
                            funTargetBet.ResetBetData();
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
    public int main_score;
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
    public string message;
    public int success;
    public int main_score;
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
    public int main_score;
    public int winner_score;
    public int wining_number;
    public int wining_status;
    public int status;
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

#endregion