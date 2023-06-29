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
    public string last_10_ft = "https://godigiinfotech.com/masterking/api/last_10_ft";
    public string get_Timer = "https://godigiinfotech.com/masterking/api/get_timer";

    string userID;
    
    public static ScoreBoardData scoreBoardData;
    //public static SendWheelDataForLast10Data sendWheelDataForLast10Data;
    public static TimerData timerData;
    public static TakeAPI takeAPI;
    public static Last_10_WinningNum last_10_WinningNum;
    public static LasttransactionId lasttransactionId;
    public static BettingData bettingData;

    public TMP_Text scoreTxt;
    public TMP_Text timerText;
    public TMP_Text bottomPanelMsg;
    public TMP_Text[] last10WinText;

    public GameObject btnHider;

    public bool iscancelSpecificBet = false;
    public bool isTake = false;

    private void Start()
    {
        PlayerPrefs.SetInt("userId", 1020);
        userID = PlayerPrefs.GetInt("userId").ToString();
        StartCoroutine(OnLoadScoreData());
        StartCoroutine(GetTimeData());
        btnHider.SetActive(false);
        GetLast10WinNumbers();
    }

    public void OnLoadScoreDataFun()
    {
        StartCoroutine(OnLoadScoreData());
    }

    IEnumerator OnLoadScoreData()
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
               //Debug.Log(strjson);

                scoreBoardData = JsonUtility.FromJson<ScoreBoardData>(strjson);
                //Debug.Log(scoreBoardData);

                //Debug.Log(scoreBoardData.main_score);
                //Debug.Log(scoreBoardData.wining_score);

                switch (scoreBoardData.status)
                {
                    case 500:
                        Debug.Log("Netwrok 500 Error");
                        break;

                    case 200:
                        //Debug.Log(scoreBoardData.main_score);
                        //Debug.Log(scoreBoardData.wining_score);

                        scoreTxt.text = scoreBoardData.main_score.ToString();

                        PlayerPrefs.SetInt("ft_Score", scoreBoardData.main_score);
                        break;
                }
            }
        }
    }

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
                        //Debug.Log(last_10_WinningNum.main_score);
                        //Debug.Log(last_10_WinningNum.message);
                        //Debug.Log(last_10_WinningNum.wining_score);


                        //foreach (DataItem_WinNum item in last_10_WinningNum.last_10_data)
                        //{
                        //    Debug.Log("ID: " + item.id);
                        //    Debug.Log("Random Number: " + item.random_number);
                        //    //last10WinText[]
                        //    Debug.Log("Created At: " + item.created_at);
                        //    Debug.Log("Created By: " + item.created_by);
                        //}
                        int minLength = Mathf.Min(last10WinText.Length, last_10_WinningNum.last_10_data.Length);

                        for (int i = 0; i < minLength; i++)
                        {
                            last10WinText[i].text = last_10_WinningNum.last_10_data[i].random_number;
                        }
                        break;
                }
            }
        }
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
                //Debug.Log(strjson);

                timerData = JsonUtility.FromJson<TimerData>(strjson);

                switch (timerData.status)
                {
                    case 500:
                        Debug.Log(www.error);

                        break;

                    case 200:
                        //Debug.Log(timerData.message);
                        //Debug.Log(timerData.status);

                        if(timerData.timer < 10)
                        {
                            timerText.text = "00:0" + timerData.timer.ToString();
                        }
                        else
                        {
                            timerText.text = "00:" + timerData.timer.ToString();
                        }

                        
                        if(timerData.timer == 0)
                        {
                            spinTheWheel.StartSpinButtonClick();
                            StopCoroutine(GetTimeData());
                            FT_SoundManager.instance.timerAudio.Stop();
                            //send wining data to the server
                            //send allAmt to server and update scoreBoard value
                            //send specific amt to the server and reset playerprefs to that perticular function
                            //Update last 10 data
                        }
                        else
                        {
                            if (!FT_SoundManager.instance.timerAudio.isPlaying)
                            {
                                FT_SoundManager.instance.timerAudio.Play();
                            }
                            
                            StartCoroutine(GetTimeData());
                        }

                        if (timerData.timer > 50)
                        {
                            btnHider.SetActive(true);
                            bottomPanelMsg.text = "Bet cannot be Accepted";
                        }
                        break;
                }
            }
        }
    }

    public void Take()
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.ClickSound);
        StartCoroutine(TransferToMainVallet());
        OnLoadScoreDataFun();
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
                //Debug.Log(strjson);

                takeAPI = JsonUtility.FromJson<TakeAPI>(strjson);

                //Debug.Log(takeAPI);
                switch (scoreBoardData.status)
                {
                    case 500:
                        Debug.Log(www.error);
                        break;

                    case 200:
                        Debug.Log(takeAPI.message);
                        break;
                }
            }
        }
    }

    IEnumerator Last_10_Data()
    {
        WWWForm form = new();

        form.AddField("user_id", userID);
        form.AddField("app_token", "temp_token");

        using (UnityWebRequest www = UnityWebRequest.Post(last_10_ft, form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                //Debug.Log(www.error);
            }
            else
            {
                string strjson = www.downloadHandler.text;
               // Debug.Log(strjson);

                takeAPI = JsonUtility.FromJson<TakeAPI>(strjson);

               // Debug.Log(takeAPI);
                switch (scoreBoardData.status)
                {
                    case 500:
                        Debug.Log(www.error);
                        break;

                    case 200:
                        Debug.Log(takeAPI.message);
                        break;
                }
            }
        }
    }

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

                        if (lasttransactionId.wining_score > 0)
                        {
                            isTake = true;
                        }
                        break;
                }
            }
        }
    }

    public void SendBetData()
    {
        StartCoroutine(SendBettingData());
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
                        break;

                    case 200:
                        Debug.Log(bettingData.message);
                        Debug.Log(bettingData.transaction_id);
                        funTargetBet.ResetBetData();
                        break;
                }
            }
        }
    }

    public void StartTimerAgain()
    {
        StartCoroutine(GetTimeData());
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
public class LasttransactionId
{
    public int status;
    public string message;
    public int last_transaction_id;
    public int collection_status;
    public int transaction_status;
    public int wining_score;
}

[System.Serializable]
public class BettingData
{
    public int status;
    public string message;
    public int transaction_id;
}