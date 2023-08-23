using UnityEngine;

public class SpinTheWheel : MonoBehaviour
{
    public static SpinTheWheel instance;
    public FunTargetAPIManager funTargetAPIManager;
    public FunTargetBet funTargetBet;

    public GameObject SpinWheel;
    public Animator spinCenter;

    public int Winningnumber;

    public Animator wheelTheAnimator;

    private void Awake()
    {
        instance = this;
    }

    public void WheelSpinHere()
    {
        funTargetBet.ResetBtnAnims();
        funTargetAPIManager.ShowTempDataOfLast10WinNum();
        spinCenter.SetTrigger("runSpin");
        wheelTheAnimator.SetTrigger("wheelrotation");

        if (!FT_SoundManager.instance.ft_spin.isPlaying)
        {
            FT_SoundManager.instance.ft_spin.Play();
        }

        float clipLength = FT_SoundManager.instance.ft_spin.clip.length;
        Invoke(nameof(SpinTheWheelAnim), clipLength);
    }

    public int showresTime = 0;

    public void SpinTheWheelAnim()
    {
        spinCenter.SetTrigger("idle");

        switch (PlayerPrefs.GetInt(Const.tempAnimForWinBtn))
        {
            case 0:
                wheelTheAnimator.SetTrigger("0");
                //funTargetBet.betBtn[0].SetTrigger("btnAfterWin");
                break;

            case 1:
                wheelTheAnimator.SetTrigger("1");
                //funTargetBet.betBtn[1].SetTrigger("btnAfterWin");
                break;

            case 2:
                wheelTheAnimator.SetTrigger("2");
                //funTargetBet.betBtn[2].SetTrigger("btnAfterWin");
                break;

            case 3:
                wheelTheAnimator.SetTrigger("3");
                //funTargetBet.betBtn[3].SetTrigger("btnAfterWin");
                break;

            case 4:
                wheelTheAnimator.SetTrigger("4");
                //funTargetBet.betBtn[4].SetTrigger("btnAfterWin");
                break;

            case 5:
                wheelTheAnimator.SetTrigger("5");
                //funTargetBet.betBtn[5].SetTrigger("btnAfterWin");
                break;

            case 6:
                wheelTheAnimator.SetTrigger("6");
                //funTargetBet.betBtn[6].SetTrigger("btnAfterWin");
                break;

            case 7:
                wheelTheAnimator.SetTrigger("7");
                //funTargetBet.betBtn[7].SetTrigger("btnAfterWin");
                break;

            case 8:
                wheelTheAnimator.SetTrigger("8");
                //funTargetBet.betBtn[8].SetTrigger("btnAfterWin");
                break;

            case 9:
                wheelTheAnimator.SetTrigger("9");
                //funTargetBet.betBtn[9].SetTrigger("btnAfterWin");
                break;
        }

        FT_SoundManager.instance.ft_AudioSorce.Stop();
        FT_SoundManager.instance.ft_spin.Stop();

        showresTime = 1;
        if(showresTime == 1)
        {
            ShowResult();
            showresTime = 0;
        }
    }

    void ShowResult()
    {
        Debug.Log("   show result");
        funTargetAPIManager.ShowResultWithLastTranIdAt_GameEnd();
        Invoke(nameof(StartGameAgain), 0.5f);
        WinButtonAnimation();
    }

    void StartGameAgain()
    {
        Debug.Log("   start game again");
        PlayerPrefs.SetInt(Const.isDataSendOnClick, 0);
        funTargetBet.isBetOk = false;
        funTargetAPIManager.isTimerSound = true;
        funTargetAPIManager.hasFunctionBeenCalled = false;
        funTargetBet.btnHider.SetActive(false);
        funTargetBet.allAmtClickCounter = 0;
        funTargetAPIManager.ShowDataOfLast10WinNum();
    }

    public void WinButtonAnimation()
    {
        funTargetBet.ResetBtnAnims();

        switch (PlayerPrefs.GetInt(Const.animForWinBtn))
        {
            case 0:
                funTargetBet.betBtn[0].SetTrigger("btnAfterWin");
                break;

            case 1:
                funTargetBet.betBtn[1].SetTrigger("btnAfterWin");
                break;

            case 2:
                funTargetBet.betBtn[2].SetTrigger("btnAfterWin");
                break;

            case 3:
                funTargetBet.betBtn[3].SetTrigger("btnAfterWin");
                break;

            case 4:
                funTargetBet.betBtn[4].SetTrigger("btnAfterWin");
                break;

            case 5:
                funTargetBet.betBtn[5].SetTrigger("btnAfterWin");
                break;

            case 6:
                funTargetBet.betBtn[6].SetTrigger("btnAfterWin");
                break;

            case 7:
                funTargetBet.betBtn[7].SetTrigger("btnAfterWin");
                break;

            case 8:
                funTargetBet.betBtn[8].SetTrigger("btnAfterWin");
                break;

            case 9:
                funTargetBet.betBtn[9].SetTrigger("btnAfterWin");
                break;
        }
    }
}
