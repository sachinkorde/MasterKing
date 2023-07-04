using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MasterKingGameSelectionScreen : MonoBehaviour
{
    //[SerializeField] private Text userId;

    public bool isenter = false;

    public static string userScore = string.Empty;

    public GameObject Pop_ChangePassword;
    public GameObject blockImg;
    public GameObject gameSelectionPanel;

    public InputField newPassword;
    public InputField oldPassword;

    public static ChagnePassword chngPass;

    public TMP_Text responceText;
    public TMP_Text userIdTxt;
    public TMP_Text userCoinTxt;
    public TMP_Text passwordChangeResponce;
    public static ScoreBoardData scoreBoardData;
    public string scoreBoardURI = "https://godigiinfotech.com/masterking/api/ft_scoreboard";

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        StartCoroutine(SelectionScreenAnim());

        Debug.Log(PlayerPrefs.GetInt("userId"));
        Debug.Log(PlayerPrefs.GetInt("userCoins"));

        userIdTxt.text = PlayerPrefs.GetInt("userId").ToString();
        userCoinTxt.text = PlayerPrefs.GetInt("userCoins").ToString();

        Debug.Log(PlayerPrefs.GetInt("userId"));
        Debug.Log(PlayerPrefs.GetInt("userCoins"));


        GetScoreAndWinScoreDataFunction();
    }

    IEnumerator SelectionScreenAnim()
    {
        gameSelectionPanel.SetActive(true);
        yield return new WaitForSeconds(0.45f);
        gameSelectionPanel.SetActive(false);
    }

    public void LoadFunTargetGame()
    {
        SceneManager.LoadScene("FunTarget");
    }

    public IEnumerator ChangePasswordData()
    {
        WWWForm form = new WWWForm();

        form.AddField("user_id", PlayerPrefs.GetInt("userId"));
        form.AddField("app_token", "temp_token");
        form.AddField("old_password", oldPassword.text);
        form.AddField("new_password", newPassword.text);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.godigiinfotech.com/masterking/api/app-change-password", form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string strjson = www.downloadHandler.text;

                chngPass = JsonUtility.FromJson<ChagnePassword>(strjson);

                switch (chngPass.status)
                {
                    case 500:
                        responceText.gameObject.SetActive(true);
                        responceText.text = chngPass.message;

                        yield return new WaitForSeconds(1.2f);
                        responceText.gameObject.SetActive(false);
                        responceText.text = "";
                        break;

                    case 200:
                        gameSelectionPanel.SetActive(true);
                        responceText.gameObject.SetActive(true);
                        responceText.text = chngPass.message;
                        yield return new WaitForSeconds(1.2f);
                        responceText.gameObject.SetActive(false);
                        responceText.text = "";
                        CloseChangePasswordPOP();

                        passwordChangeResponce.text = chngPass.message;
                        yield return new WaitForSeconds(2.0f);
                        passwordChangeResponce.text = "";

                        SceneManager.LoadScene("Login");
                        PlayerPrefs.DeleteAll();
                        break;
                }
            }
        }
    }

    public void ChangePassWordPOP()
    {
        Pop_ChangePassword.SetActive(true);
    }

    public void CloseChangePasswordPOP()
    {
        Pop_ChangePassword.SetActive(false);
        responceText.text = "";
        oldPassword.text = "";
        newPassword.text = "";
    }

    public void OnClickChangePawword()
    {
        StartCoroutine(ChangePasswordData());
    }

    public void GetScoreAndWinScoreDataFunction()
    {
        StartCoroutine(GetScoreAndWinScoreData());
    }

    public TMP_Text pointText;

    IEnumerator GetScoreAndWinScoreData()
    {
        WWWForm form = new();

        form.AddField("user_id", PlayerPrefs.GetInt("userId"));
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
                       
                        PlayerPrefs.SetInt("ft_Score", scoreBoardData.main_score);

                        pointText.text = PlayerPrefs.GetInt("ft_Score").ToString();
                        break;
                }
            }
        }
    }
}

[System.Serializable]
public class ChagnePassword
{
    public int status;
    public string message;
}
