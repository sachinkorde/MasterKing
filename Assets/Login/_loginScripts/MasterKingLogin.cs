using System.Collections;
using System.Collections.Generic;
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
    public string user_id;
    public string name;
    public string email;
    public string phone;
    public int point_count;
}
public class MasterKingLogin : MonoBehaviour
{
    public static string contLink = "https://www.godigiinfotech.com/masterking";
    public string loginAPI = "api/app-login";

    public InputField userLogin;
    public InputField passLogin;

    public static result res;

    //public Text messageText, messageText1;

    public Text loginResponse;
    public GameObject glowEmterBtn;
    //public GameObject loginScreenPanel;

    private void Start()
    {
        StartCoroutine(LoginScreenAnim());
        Input.multiTouchEnabled = false;
        //loginScreenPanel.SetActive(false);
        glowEmterBtn.SetActive(false);

        if (PlayerPrefs.GetInt("isloggedIn") == 1)
        {
            SceneManager.LoadScene("GameSelection");
        }
        else
        {
            return;
        }
    }

    IEnumerator LoginScreenAnim()
    {
        //loginScreenPanel.SetActive(true);
        yield return new WaitForSeconds(0.45f);
        //loginScreenPanel.SetActive(false);
    }

    public void Login()
    {
        StartCoroutine(LoginGame());
        glowEmterBtn.SetActive(true);
    }

    public IEnumerator LoginGame()
    {
        WWWForm form = new();

        form.AddField("user_id", userLogin.text);
        form.AddField("password", passLogin.text);
        form.AddField("app_token", "temp_token");

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.godigiinfotech.com/masterking/api/app-login", form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                //loginScreenPanel.SetActive(true);
                string strjson = www.downloadHandler.text;

                Debug.Log(strjson);
                res = JsonUtility.FromJson<result>(strjson);
                Debug.Log(res);

                switch (res.status)
                {
                    case 500:
                        loginResponse.text = res.message;
                        yield return new WaitForSeconds(2f);
                        loginResponse.text = "";
                        glowEmterBtn.SetActive(false);
                        //loginScreenPanel.SetActive(false);
                        break;

                    case 200:

                        PlayerPrefs.SetInt("isloggedIn", 1);
                        PlayerPrefs.SetInt("userId", res.data.id);
                        PlayerPrefs.SetInt("userCoins", res.data.point_count);

                        Debug.Log(PlayerPrefs.GetInt("userId"));
                        Debug.Log(PlayerPrefs.GetInt("userCoins"));

                        SceneManager.LoadScene("GameSelection");

                        //loginScreenPanel.SetActive(false);
                        break;
                }
            }
        }
    }

    public IEnumerator AutoLogin(string username, string pass)
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
                Debug.Log("Post Request Complete!" + www.downloadHandler.text);
                string strjson = www.downloadHandler.text;
                res = JsonUtility.FromJson<result>(strjson);
                Debug.Log(res.status);
                if (res.status == 200)
                {
                    Debug.Log("LoggedIn" + res.message);
                    SceneManager.LoadScene("GameScreen");
                }
            }
        }
    }
}
