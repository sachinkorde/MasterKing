using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

[System.Serializable]
public class result
{
    public string message;
    public int status;
    public resultData data;
}

[System.Serializable]
public class resultData
{
    public int id;
    public int account_number;
    public string name;
    public string email;
    public string phone;
    public string type;
    public int point_count;
}

public class MasterKingLogin : MonoBehaviour
{
    //public static string contLink = "https://www.godigiinfotech.com/masterking";
    //public string loginAPI = "api/app-login";
    private string loginUrl = "https://www.godigiinfotech.com/masterking/api/app-login";

    public InputField userLogin;
    public InputField passLogin;

    public static result res;

    public Text loginResponse;
    public GameObject glowEnterBtn;
    public GameObject glowCloseBtn;
    public GameObject loginScreenPanel;

    private void Awake()
    {
        PlayerPrefs.DeleteAll();
    }

    private void Start()
    {
        Debug.Log(PlayerPrefs.GetInt(Const.userId));


        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        StartCoroutine(StartAnim());
        glowEnterBtn.SetActive(false);
        glowCloseBtn.SetActive(false);
    }

    IEnumerator StartAnim()
    {
        loginScreenPanel.SetActive(true);
        yield return new WaitForSeconds(0.11f);
        loginScreenPanel.SetActive(false);
    }

    public void Login()
    {
        StartCoroutine(LoginGame());
        glowEnterBtn.SetActive(true);
    }

    public IEnumerator LoginGame()
    {
        WWWForm form = new();

        form.AddField("account_number", userLogin.text);
        form.AddField("password", passLogin.text);
        form.AddField("app_token", "temp_token");

        using (UnityWebRequest www = UnityWebRequest.Post(loginUrl, form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string strjson = www.downloadHandler.text;
                res = JsonUtility.FromJson<result>(strjson);

                switch (res.status)
                {
                    case 500:
                        loginResponse.text = res.message;
                        yield return new WaitForSeconds(4.5f);
                        loginResponse.text = "";
                        glowEnterBtn.SetActive(false);
                        break;

                    case 200:

                        PlayerPrefs.SetInt("isloggedIn", 1);
                        PlayerPrefs.SetInt(Const.userId, res.data.id);
                        PlayerPrefs.SetInt(Const.userAcc, res.data.account_number);
                        PlayerPrefs.SetInt(Const.userCoins, res.data.point_count);
                        SceneManager.LoadScene(Const.GameSelection);
                        loginResponse.text = res.message + "";

                        Debug.Log(PlayerPrefs.GetInt(Const.userId) + "  saved userId");
                        Debug.Log(PlayerPrefs.GetInt(Const.userAcc) + "  saved userAcc");
                        break;
                }
            }
        }
    }

    public void CloseApp()
    {
        glowCloseBtn.SetActive(true);
        Application.Quit();
    }

    /*public IEnumerator AutoLogin(string username, string pass)
    {
        WWWForm form = new WWWForm();

        form.AddField("username", username);
        form.AddField("password", pass);

        using (UnityWebRequest www = UnityWebRequest.Post("https://btogames.org/btnologin/APIcontroller/Login", form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string strjson = www.downloadHandler.text;
                res = JsonUtility.FromJson<result>(strjson);

                if (res.status == 200)
                {
                    SceneManager.LoadScene("GameScreen");
                }
            }
        }
    }*/
}
