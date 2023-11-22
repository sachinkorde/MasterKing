using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class FunTargetAPIManager : MonoBehaviour
{
    public FunTargetBet funTargetBet;
    public SpinTheWheel spinTheWheel;

    public string lastTransactionIdurl = "https://godigiinfotech.com/masterking/api/last_transaction_id_ft"; // Get WinScore click on bet if winscore is greateer than 0
    public string scoreBoardURI = "https://godigiinfotech.com/masterking/api/ft_scoreboard"; // Shows updaed score
    public string betting_data = "https://godigiinfotech.com/masterking/api/betting_data"; // Send all betting data 0 to 9
    public string transfer_main_wallet = "https://godigiinfotech.com/masterking/api/ft_transfer_main_wallet"; //Take API
    public string get_result = "https://godigiinfotech.com/masterking/api/get_result";
    public string last_10_WinNumber = "https://godigiinfotech.com/masterking/api/last_10_win_number"; // Get Last 10 Wining Num and Winning score here
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
    public static Last10WinNum last10WinNum;

    public Animator timerAnimator;

    string userID;

    int seconds;
    int preSec;

    public bool isSpin = false;
    public bool isTimerAnimStart = false;
    public TMP_Text winTextTempshow;
    private float currentTime = 0;

    private void Awake()
    {
        StartTimer();
        userID = PlayerPrefs.GetInt(Const.userId).ToString();
    }

    private void Start()
    {
        ShowResultWithLastTranIdAt_Start();
        GetScoreAndWinScoreDataFunction();
        ShowDataOfLast10WinNum();
        funTargetBet.btnHider.SetActive(false);

        isTimeStart = false;
        isTimerSound = true;
        funTargetBet.timerText.text = "00:00";
    }

    #region Last Transaction Id At Start
    public void ShowResultWithLastTranIdAt_Start()
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

                ShowResultWithLastTranIdAt_Start();
            }
            else
            {
                string strjson = www.downloadHandler.text;
                lastTransactionIdData = JsonUtility.FromJson<LastTransactionIdData>(strjson);

                switch (lastTransactionIdData.status)
                {
                    case 500:
                        Debug.Log(www.error);

                        if (www.error != "Null")
                        {
                            ShowResultWithLastTranIdAt_Start();

                            Debug.Log(www.error +  "   called here how many times");
                        }
                        else
                        {
                            SceneManager.LoadScene(Const.Login);
                        }

                        break;

                    case 200:

                        PlayerPrefs.SetInt(Const.last_transaction_id, lastTransactionIdData.last_transaction_id);
                        OnLoadStartGetResult_AtStart();
                        break;
                }
            }
        }
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

    public void OnLoadStartGetResult_AtStart()
    {
        StartCoroutine(OnLoadStartGetResultData_AtStart());
    }

    IEnumerator OnLoadStartGetResultData_AtStart()
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
                OnLoadStartGetResult_AtStart();
                Debug.Log(www.error);
            }
            else
            {
                string strjson = www.downloadHandler.text;
                getResultData = JsonUtility.FromJson<GetResultData>(strjson);

                switch (getResultData.status)
                {
                    case 500:
                        Debug.Log(www.error);
                        OnLoadStartGetResult_AtStart();
                        break;

                    case 200:
                        
                        if (getResultData.betting_data[0].is_end == 20) // Game Not End
                        {
                            DataHandling();
                            funTargetBet.TakeBtnDisabledState();
                            funTargetBet.bottomPanelMsg.text = "Bet Ok";
                            funTargetBet.btnHider.SetActive(true);

                            Debug.Log("  Game is not End Here");

                        }
                        else if (getResultData.betting_data[0].is_end == 10) // Old Game is End Here
                        {
                            if (getResultData.betting_data[0].winner_score > 0 
                                && getResultData.betting_data[0].status == 0)  // here we need to take
                            {
                                funTargetBet.TakeBtnEnbledState();
                                funTargetBet.winText.text = getResultData.betting_data[0].winner_score.ToString();
                                DataHandling();

                                Debug.Log(getResultData.betting_data[0].winner_score);
                                funTargetBet.bottomPanelMsg.text = "Please Take your Win Amount And Play";

                                Debug.Log("Old Game is End Here and Player need to take win Amt");
                            }
                            else if(getResultData.betting_data[0].winner_score == 0 
                                && getResultData.betting_data[0].status == 0) // Here not win But need to take
                            {
                                Take();
                                funTargetBet.TakeBtnDisabledState();
                                FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Loose);
                                funTargetBet.ResetBetData();
                                funTargetBet.bottomPanelMsg.text = "";

                                Debug.Log("Player is not win but take API called for setup calculations in game");
                            }
                            else if(getResultData.betting_data[0].status == 10) // No need to take 
                            {
                                funTargetBet.ResetBetData();
                                funTargetBet.TakeBtnDisabledState();

                                Debug.Log("Old Game is End Here and Player already to take win Amt");

                            }
                            //spinTheWheel.showresTime = false;
                            StopCoroutine(OnLoadStartGetResultData_AtStart());
                        }

                        break;
                }
            }
        }
    }
    #endregion

    #region Show Restlt At Game End
    public void ShowResultWithLastTranIdAt_GameEnd()
    {
        StartCoroutine(LastTransactionIdDataAt_GameEnd());
    }

    IEnumerator LastTransactionIdDataAt_GameEnd()
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

                ShowResultWithLastTranIdAt_GameEnd();
            }
            else
            {
                string strjson = www.downloadHandler.text;
                lastTransactionIdData = JsonUtility.FromJson<LastTransactionIdData>(strjson);

                switch (lastTransactionIdData.status)
                {
                    case 500:
                        Debug.Log(www.error);

                        if (www.error != "Null")
                        {
                            //ShowResultWithLastTranIdAt_GameEnd();

                            Debug.Log(www.error + "   called here how many times");
                        }
                        else
                        {
                            SceneManager.LoadScene(Const.Login);
                        }

                        break;

                    case 200:

                        PlayerPrefs.SetInt(Const.last_transaction_id, lastTransactionIdData.last_transaction_id);
                        LoadGetResult_AtGameEnd();
                        break;
                }
            }
        }
    }

    public void LoadGetResult_AtGameEnd()
    {
        StartCoroutine(OnLoadStartGetResultData_AtGameEnd());
    }

    IEnumerator OnLoadStartGetResultData_AtGameEnd()
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

                LoadGetResult_AtGameEnd();
            }
            else
            {
                string strjson = www.downloadHandler.text;
                getResultData = JsonUtility.FromJson<GetResultData>(strjson);

                switch (getResultData.status)
                {
                    case 500:
                        Debug.Log(www.error);
                        //LoadGetResult_AtGameEnd();
                        break;

                    case 200:
                        if (getResultData.betting_data[0].is_end == 10) // Old Game is End Here
                        {
                            if (getResultData.betting_data[0].winner_score > 0
                                && getResultData.betting_data[0].status == 0)  // here we need to take
                            {
                                funTargetBet.TakeBtnEnbledState();
                                funTargetBet.winText.text = getResultData.betting_data[0].winner_score.ToString();
                                DataHandling();

                                funTargetBet.bottomPanelMsg.text = "Please Take your Win Amount And Play";

                                Debug.Log("Old Game is End Here and Player need to take win Amt");
                            }
                            else if (getResultData.betting_data[0].winner_score == 0
                                && getResultData.betting_data[0].status == 0) // Here not win But need to take
                            {
                                Take();
                                funTargetBet.TakeBtnDisabledState();
                                FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Loose);
                                funTargetBet.ResetBetData();
                                funTargetBet.bottomPanelMsg.text = "You Loose..Play Again!";
                                Debug.Log("Player is not win but take API called for setup calculations in game");
                            }

                            
                            Debug.Log(PlayerPrefs.GetInt(Const.winNumber) + "    Wining Num ");
                        }

                        break;
                }
            }
        }

        ShowDataOfLast10WinNum();
    }
    #endregion

    #region GettimerData get_Timer
    public void StartTimer()
    {
        StartCoroutine(GetTimeData());
    }

    IEnumerator GetTimeData()
    {
        currentTime = 0;

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

                        currentTime = timerData.timer;
                        isTimeStart = true;
                        break;
                }
            }
        }
    }

    bool isTimeStart = false;
    public bool isTimerSound = true;

    private void FixedUpdate()
    {
        if(isTimeStart)
        {
            UpdateTimerDisplay();
        }
    }

    public bool hasFunctionBeenCalled = false;

    void UpdateTimerDisplay()
    {
        preSec = seconds;
        seconds = Mathf.FloorToInt(currentTime % 60);
        currentTime -= Time.deltaTime;

        if (seconds < preSec && isTimerSound)
        {
            if (!FT_SoundManager.instance.timerAudio.isPlaying)
            {
                FT_SoundManager.instance.timerAudio.Play();
            }
        }

        if(seconds < 10)
        {
            funTargetBet.timerText.text = "00:0" + seconds;
        }
        else
        {
            funTargetBet.timerText.text = "00:" + seconds;
        }

        if (seconds == 1)
        {
            if (!hasFunctionBeenCalled)
            {
                hasFunctionBeenCalled = true;
                CustomAnimation();
                //Debug.Log(seconds + "     This is secondssss");
            }
        }

        if (seconds == 10)
        {
            funTargetBet.btnHider.SetActive(true);
            funTargetBet.bottomPanelMsg.text = "Bet Time Over..!";
            funTargetBet.BetOkIdleAnim();

            if (!funTargetBet.isTake && PlayerPrefs.GetInt(Const.isDataSendOnClick) == 0)
            {
                PlayerPrefs.SetInt(Const.isDataSendOnClick, 1);
                UpdateWinNum();
            }
        }

        if (seconds == 15)
        {
            timerAnimator.SetTrigger("timerAnim");
        }
    }

    void CustomAnimation()
    {
        funTargetBet.timerText.text = "00:01";
        Invoke(nameof(WheelRotate), 0.65f);
    }

    void WheelRotate()
    {
        funTargetBet.timerText.text = "00:00";
        isTimerSound = false;
        spinTheWheel.WheelSpinHere();
        PlayerPrefs.SetInt(Const.startNewGame, 0);
        timerAnimator.SetTrigger("idle");
        isSpin = false;
        currentTime = 60;
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
                Take();
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

                        funTargetBet.isBetOk = false;
                        funTargetBet.btnHider.SetActive(false);
                        funTargetBet.TakeBtnDisabledState();
                        funTargetBet.scoreTxt.text = takeAPI.main_score + ".00";
                        funTargetBet.winText.text = "";
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
        Invoke(nameof(DisablePreviousBtn), 5.0f);
    }

    void DisablePreviousBtn()
    {
        funTargetBet.previousBtn.gameObject.SetActive(false);
        funTargetBet.betokBtn.gameObject.SetActive(true);
        funTargetBet.BetOkIdleAnim();
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

                        if(www.error != null)
                        {
                            GetScoreAndWinScoreDataFunction();
                        }
                        else
                        {
                            SceneManager.LoadScene(Const.Login);
                        }
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
            PlayerPrefs.SetInt(Const.isDataSendOnClick, 1);
            UpdateWinNum();
        }
        funTargetBet.betokBtn.gameObject.SetActive(false);
        funTargetBet.betokBtn.gameObject.SetActive(true);
        funTargetBet.BetOkIdleAnim();
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

                        /*Debug.Log("------This Data sended To server-------");

                        Debug.Log(PlayerPrefs.GetString(Const.data0));
                        Debug.Log(PlayerPrefs.GetString(Const.data1));
                        Debug.Log(PlayerPrefs.GetString(Const.data2));
                        Debug.Log(PlayerPrefs.GetString(Const.data3));
                        Debug.Log(PlayerPrefs.GetString(Const.data4));
                        Debug.Log(PlayerPrefs.GetString(Const.data5));
                        Debug.Log(PlayerPrefs.GetString(Const.data6));
                        Debug.Log(PlayerPrefs.GetString(Const.data7));
                        Debug.Log(PlayerPrefs.GetString(Const.data8));
                        Debug.Log(PlayerPrefs.GetString(Const.data9));

                        Debug.Log("------This Data sended To server-------");*/
                        break;
                }
            }
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
        if(funTargetBet.isBetOk || funTargetBet.isTake)
        {
            return;
        }
        else
        {
            StartCoroutine(GetWinNumFromDb());
        }
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
                        SendBetData();
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

                OnClickLoadPreviousBet();
            }
            else
            {
                string strjson = www.downloadHandler.text;
                lastTransactionIdData = JsonUtility.FromJson<LastTransactionIdData>(strjson);

                switch (lastTransactionIdData.status)
                {
                    case 500:
                        Debug.Log(www.error);

                        OnClickLoadPreviousBet();

                        break;

                    case 200:

                        PlayerPrefs.SetInt(Const.last_transaction_id, lastTransactionIdData.last_transaction_id);
                        StartCoroutine(LoadPreViousDataFromAPI());

                        funTargetBet.allAmtClickCounter = 1;
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

                            float updatedScore;
                            updatedScore = PlayerPrefs.GetFloat(Const.ft_score) - allamt;
                            funTargetBet.scoreTxt.text = updatedScore+ ".00";
                            funTargetBet.previousBtn.gameObject.SetActive(false);
                            funTargetBet.betokBtn.gameObject.SetActive(true);
                            funTargetBet.betokBtn.enabled = true;
                            funTargetBet.cancelBtn.enabled = true;
                            funTargetBet.cancelSpecificBetBtn.enabled = true;
                            //funTargetBet.BetBtnAnimation();


                            /*Debug.Log("------This Data Get From server-------");

                            Debug.Log(PlayerPrefs.GetString(Const.data0));
                            Debug.Log(PlayerPrefs.GetString(Const.data1));
                            Debug.Log(PlayerPrefs.GetString(Const.data2));
                            Debug.Log(PlayerPrefs.GetString(Const.data3));
                            Debug.Log(PlayerPrefs.GetString(Const.data4));
                            Debug.Log(PlayerPrefs.GetString(Const.data5));
                            Debug.Log(PlayerPrefs.GetString(Const.data6));
                            Debug.Log(PlayerPrefs.GetString(Const.data7));
                            Debug.Log(PlayerPrefs.GetString(Const.data8));
                            Debug.Log(PlayerPrefs.GetString(Const.data9));

                            Debug.Log("------This Data Get From server-------");*/

                            funTargetBet.AllDataAmount();

                            funTargetBet.allAmtClickCounter++;
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

    #region show Last 10 Win Number Btn animation is also here after winning
    public void ShowDataOfLast10WinNum()
    {
        StartCoroutine(ShowLast10WinNumbers());
    }

    private IEnumerator ShowLast10WinNumbers()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(last_10_WinNumber))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonString = webRequest.downloadHandler.text;
                last10WinNum = JsonUtility.FromJson<Last10WinNum>(jsonString);

                if (last10WinNum.last_data != null)
                {
                    for (int i = 0; i < last10WinNum.last_data.Length; i++)
                    {
                        funTargetBet.last10WinText[i].text = last10WinNum.last_data[i];

                        Debug.Log(i + " == " + last10WinNum.last_data[i] + " last 10 data");
                    }

                    int x = int.Parse(last10WinNum.last_data[9]);
                    Debug.Log(x + " Is data correct");
                    PlayerPrefs.SetInt(Const.animForWinBtn, x);

                    Debug.Log(PlayerPrefs.GetInt(Const.animForWinBtn) + "  is playerprefs values is corretc");
                    Animator animator = spinTheWheel.wheelTheAnimator;
                    funTargetBet.AllDataAmount();
                    
                    switch (x)
                    {
                        case 0:
                            animator.SetTrigger("0");
                            funTargetBet.betBtn[0].SetTrigger("btnAfterWin");
                            Debug.Log("reached");
                            break;

                        case 1:
                            animator.SetTrigger("1");
                            funTargetBet.betBtn[1].SetTrigger("btnAfterWin");
                            Debug.Log("reached");
                            break;

                        case 2:
                            animator.SetTrigger("2");
                            funTargetBet.betBtn[2].SetTrigger("btnAfterWin");
                            Debug.Log("reached");
                            break;

                        case 3:
                            animator.SetTrigger("3");
                            funTargetBet.betBtn[3].SetTrigger("btnAfterWin");
                            Debug.Log("reached");
                            break;

                        case 4:
                            animator.SetTrigger("4");
                            funTargetBet.betBtn[4].SetTrigger("btnAfterWin");
                            Debug.Log("reached");
                            break;

                        case 5:
                            animator.SetTrigger("5");
                            funTargetBet.betBtn[5].SetTrigger("btnAfterWin");
                            Debug.Log("reached");
                            break;

                        case 6:
                            animator.SetTrigger("6");
                            funTargetBet.betBtn[6].SetTrigger("btnAfterWin");
                            Debug.Log("reached");
                            break;

                        case 7:
                            animator.SetTrigger("7");
                            funTargetBet.betBtn[7].SetTrigger("btnAfterWin");
                            Debug.Log("reached");
                            break;

                        case 8:
                            animator.SetTrigger("8");
                            funTargetBet.betBtn[8].SetTrigger("btnAfterWin");
                            Debug.Log("reached");
                            break;

                        case 9:
                            animator.SetTrigger("9");
                            funTargetBet.betBtn[9].SetTrigger("btnAfterWin");
                            Debug.Log("reached");
                            break;
                    }

                    spinTheWheel.spinCenter.SetTrigger("idle");
                }
                else
                {
                    ShowDataOfLast10WinNum();
                }
            }
            else
            {
                ShowDataOfLast10WinNum();
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

[System.Serializable]
public class Last10WinNum
{
    public int status;
    public string message;
    public string[] last_data;
}
#endregion