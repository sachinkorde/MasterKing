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

    /*public void StartSpinButtonClick()
    {
        SpinWheel.SetActive(true);
        SpinWheel.transform.localEulerAngles = Vector3.zero;
        NumberOfRotation = Random.Range(7, 10);
        //Winningnumber = Random.Range(0, totalNumbers);
        float AngleOfRotation;
        AngleOfRotation = (NumberOfRotation * -360) + float.Parse(NumberAngle[Winningnumber]);
        StartCoroutine(Spin(0, AngleOfRotation, 7));
    }*/

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
            funTargetAPIManager.GetResultFunInGame();
        }

        yield return new WaitForSeconds(0.5f);

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

    public Animator wheelTheAnimator;

    public void WheelSpinHere()
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.SpeenWheelRotate);
        spinCenter.SetTrigger("runSpin");
        wheelTheAnimator.SetTrigger("wheelrotation");

        float clipLength = FT_SoundManager.instance.ft_AudioSorce.clip.length;
        Invoke(nameof(SpinTheWheelTest), clipLength);
    }

    public void SpinTheWheelTest()
    {
        spinCenter.SetTrigger("idle");

        for (int i = 0; i < funTargetBet.betBtn.Count; i++)
        {
            funTargetBet.betBtn[i].SetTrigger("idle");
        }

        switch (Winningnumber)
        {
            case 0:
                wheelTheAnimator.SetTrigger("0");
                funTargetBet.betBtn[0].SetTrigger("btnClick");
                break;

            case 1:
                wheelTheAnimator.SetTrigger("1");
                funTargetBet.betBtn[1].SetTrigger("btnClick");
                break;

            case 2: 
                wheelTheAnimator.SetTrigger("2");
                funTargetBet.betBtn[2].SetTrigger("btnClick");
                break;

            case 3:
                wheelTheAnimator.SetTrigger("3");
                funTargetBet.betBtn[3].SetTrigger("btnClick");
                break;

            case 4:
                wheelTheAnimator.SetTrigger("4");
                funTargetBet.betBtn[4].SetTrigger("btnClick");
                break;

            case 5:
                wheelTheAnimator.SetTrigger("5");
                funTargetBet.betBtn[5].SetTrigger("btnClick");
                break;

            case 6:
                wheelTheAnimator.SetTrigger("6");
                funTargetBet.betBtn[6].SetTrigger("btnClick");
                break;

            case 7:
                wheelTheAnimator.SetTrigger("7");
                funTargetBet.betBtn[7].SetTrigger("btnClick");
                break;

            case 8:
                wheelTheAnimator.SetTrigger("8");
                funTargetBet.betBtn[8].SetTrigger("btnClick");
                break;

            case 9:
                wheelTheAnimator.SetTrigger("9");
                funTargetBet.betBtn[9].SetTrigger("btnClick");
                break;
        }

        FT_SoundManager.instance.ft_AudioSorce.Stop();

        Invoke(nameof(ShowResult), 0.5f);
    }

    void ShowResult()
    {
        if (!funTargetBet.isDataNull)
        {
            funTargetAPIManager.GetResultFunInGame();
        }

        if (funTargetBet.isTake)
        {
            FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Win);
        }
        else
        {
            FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Loose);
        }

        funTargetBet.ShowWinNumInLast10Data();
        //if Here is winner play win sound or play loose soundclip
        //FT_SoundManager.instance.PlayAudioClip(FT_GameClips.Loading);
        Invoke(nameof(StartGameAgain), 0.5f);
    }

    void StartGameAgain()
    {
        funTargetAPIManager.StartTimerAgain();

        funTargetBet.btnHider.SetActive(false);
        funTargetBet.isDataSendOnClick = false;
        funTargetBet.isFunCounter = false;
        funTargetBet.isCallWinNumAPI = false;
    }
}
