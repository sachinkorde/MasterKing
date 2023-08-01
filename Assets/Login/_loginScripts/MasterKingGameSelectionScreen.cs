using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MasterKingGameSelectionScreen : MonoBehaviour
{
    private string scoreBoardURI = "https://godigiinfotech.com/masterking/api/ft_scoreboard";
    private string changePassUrl = "https://www.godigiinfotech.com/masterking/api/app-change-password";

    public GameObject Pop_ChangePassword;
    public GameObject gameSelectionPanel;

    public InputField newPassword;
    public InputField oldPassword;

    public TMP_Text responceText;
    public TMP_Text userIdTxt;
    public TMP_Text userCoinTxt;
    public TMP_Text passwordChangeResponce;

    public static ChagnePassword chngPass;
    public static ScoreBoardData scoreBoardData;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        StartCoroutine(SelectionScreenAnim());

        userIdTxt.text = PlayerPrefs.GetInt(Const.userAcc).ToString();
        //userCoinTxt.text = PlayerPrefs.GetInt(Const.userCoins).ToString();

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
        SceneManager.LoadScene(Const.FunTarget);
    }

    public IEnumerator ChangePasswordData()
    {
        WWWForm form = new();

        form.AddField("user_id", PlayerPrefs.GetInt(Const.userId));
        form.AddField("app_token", "temp_token");
        form.AddField("old_password", oldPassword.text);
        form.AddField("new_password", newPassword.text);

        using (UnityWebRequest www = UnityWebRequest.Post(changePassUrl, form))
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

                        SceneManager.LoadScene(Const.Login);
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

    IEnumerator GetScoreAndWinScoreData()
    {
        WWWForm form = new();

        form.AddField("user_id", PlayerPrefs.GetInt(Const.userId));
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
                        SceneManager.LoadScene(Const.Login);
                        break;

                    case 200:

                        userCoinTxt.text = scoreBoardData.main_score.ToString();
                        break;
                }
            }
        }
    }

    public void LogOutGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Login");
    }
}

[System.Serializable]
public class ChagnePassword
{
    public int status;
    public string message;
}
