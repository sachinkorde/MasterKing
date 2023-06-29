using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SpinTheWheel : MonoBehaviour
{
    public static SpinTheWheel instance;
    public FunTargetAPIManager funTargetAPIManager;

    public GameObject SpinWheel;

    public TMP_Text winText;

    public string[] NumberValue, NumberAngle;

    public int totalNumbers;
    public int Winningnumber;
    int NumberOfRotation = 4;
    public int[] result, resultarr;

    public Animator spinCenter;

    private void Start()
    {
        instance = this;
        resultarr = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        result = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        winText.text = "";
    }

    public void SendData(int a, int result)
    {
        resultarr[a] = result;
    }

    public void StartSpinButtonClick()
    {
        SpinWheel.SetActive(true);
        SpinWheel.transform.localEulerAngles = Vector3.zero;
        NumberOfRotation = Random.Range(4, 10);
        Winningnumber = Random.Range(0, totalNumbers);
        float AngleOfRotation;
        AngleOfRotation = (NumberOfRotation * -360) + float.Parse(NumberAngle[Winningnumber]);
        StartCoroutine(Spin(0, AngleOfRotation, 7));
    }

    private IEnumerator Spin(float fromAngle, float toAngle, float withinSeconds)
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.SpeenWheelRotate);
        var passedTime = 0f;

        while (passedTime < withinSeconds)
        {
            if (!spinCenter.GetCurrentAnimatorStateInfo(0).IsName("run_spin"))
            {
                spinCenter.SetTrigger("run_spin");
            }

            var lerpFactor = Mathf.SmoothStep(0, 1, Mathf.SmoothStep(0, 1, passedTime / withinSeconds));
            SpinWheel.transform.localEulerAngles = new Vector3(0.0f, 0.0f, Mathf.Lerp(fromAngle, toAngle, lerpFactor));
            passedTime += Time.deltaTime;
            yield return null;

            if (spinCenter.GetCurrentAnimatorStateInfo(0).IsName("run_spin"))
            {
                spinCenter.SetTrigger("idle");
            }
        }

        FT_SoundManager.instance.ft_AudioSorce.Stop();

        yield return new WaitForSeconds(2.0f);

        winText.text = NumberValue[Winningnumber];
        funTargetAPIManager.SendBetData();
        funTargetAPIManager.StartTimerAgain();
        funTargetAPIManager.OnLoadScoreDataFun();
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Loading);

        yield return new WaitForSeconds(3.0f);

        SpinWheel.transform.localEulerAngles = Vector3.zero;
    }
}
