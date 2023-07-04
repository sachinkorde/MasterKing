using System.Collections;
using UnityEngine;

public class SpinTheWheel : MonoBehaviour
{
    public static SpinTheWheel instance;
    public FunTargetAPIManager funTargetAPIManager;
    public FunTargetBet funTargetBet;

    public GameObject SpinWheel;
    public Animator spinCenter;

    public int totalNumbers;
    public int Winningnumber;
    int NumberOfRotation = 4;
    public int[] result, resultarr;

    public string[] NumberValue, NumberAngle;

    private void Start()
    {
        instance = this;
        resultarr = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        result = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    }

    public void SendData(int a, int result)
    {
        resultarr[a] = result;
    }

    public void StartSpinButtonClick()
    {
        SpinWheel.SetActive(true);
        SpinWheel.transform.localEulerAngles = Vector3.zero;
        NumberOfRotation = Random.Range(7, 10);
        //Winningnumber = Random.Range(0, totalNumbers);
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
        }

        spinCenter.gameObject.SetActive(false);
        spinCenter.gameObject.SetActive(true);

        FT_SoundManager.instance.ft_AudioSorce.Stop();
        yield return new WaitForSeconds(0.75f);

        if (!funTargetBet.isDataNull)
        {
            funTargetAPIManager.GetResultFun();
            //funTargetBet.ResetBetData();
            //funTargetBet.PrevoiusBetStatus();
        }

        yield return new WaitForSeconds(0.5f);

        //funTargetAPIManager.GetLast10WinNumbers();
        funTargetBet.ShowWinNumInLast10Data();
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Loading);

        yield return new WaitForSeconds(1.0f);

        funTargetAPIManager.StartTimerAgain();
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Loading);
        funTargetBet.isCallWinNumAPI = false;

        yield return new WaitForSeconds(2.0f);

        funTargetBet.btnHider.SetActive(false);
        funTargetBet.isDataSendOnClick = false;
        funTargetBet.isFunCounter = false;
        SpinWheel.transform.localEulerAngles = Vector3.zero;
    }
}
