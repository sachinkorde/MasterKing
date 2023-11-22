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
        //funTargetBet.ResetBtnAnims();
        spinCenter.SetTrigger("runSpin");
        wheelTheAnimator.SetTrigger("wheelrotation");

        if (!FT_SoundManager.instance.ft_spin.isPlaying)
        {
            FT_SoundManager.instance.ft_spin.Play();
        }
        //float clipLength = FT_SoundManager.instance.ft_spin.clip.length;
        Invoke(nameof(SpinTheWheelAnim), 5.5f);
    }

    public int showresTime = 0;

    public void SpinTheWheelAnim()
    {
        showresTime = 1;
        if (showresTime == 1)
        {
            ShowResult();
            showresTime = 0;
        }
    }

    void ShowResult()
    {
        funTargetAPIManager.ShowResultWithLastTranIdAt_GameEnd();
        //funTargetAPIManager.ShowDataOfLast10WinNum();
        //Invoke(nameof(WinButtonAnimation), 0.35f);
        Invoke(nameof(StartGameAgain), 0.5f);
    }

    void StartGameAgain()
    {
        PlayerPrefs.SetInt(Const.isDataSendOnClick, 0);
        funTargetBet.isBetOk = false;
        funTargetAPIManager.isTimerSound = true;
        funTargetAPIManager.hasFunctionBeenCalled = false;
        funTargetBet.btnHider.SetActive(false);
        funTargetBet.allAmtClickCounter = 0;
        //WinButtonAnimation();
    }

    /*public void StopWheelAtWinNum()
    {
        switch (PlayerPrefs.GetInt(Const.animForWinBtn))
        {
            case 0:
                wheelTheAnimator.SetTrigger("0");
                break;

            case 1:
                wheelTheAnimator.SetTrigger("1");

                Debug.Log("reached");
                break;

            case 2:
                wheelTheAnimator.SetTrigger("2");
                Debug.Log("reached");
                break;

            case 3:
                wheelTheAnimator.SetTrigger("3");
                Debug.Log("reached");
                break;

            case 4:
                wheelTheAnimator.SetTrigger("4");
                Debug.Log("reached");
                break;

            case 5:
                wheelTheAnimator.SetTrigger("5");
                Debug.Log("reached");
                break;

            case 6:
                wheelTheAnimator.SetTrigger("6");
                Debug.Log("reached");
                break;

            case 7:
                wheelTheAnimator.SetTrigger("7");
                Debug.Log("reached");
                break;

            case 8:
                wheelTheAnimator.SetTrigger("8");
                Debug.Log("reached");
                break;

            case 9:
                wheelTheAnimator.SetTrigger("9");
                Debug.Log("reached");
                break;
        }

        FT_SoundManager.instance.ft_AudioSorce.Stop();
        FT_SoundManager.instance.ft_spin.Stop();
        //WinButtonAnimation();
    }*/

    /*public void WinButtonAnimation()
    {
        funTargetBet.ResetBtnAnims();
        Debug.Log(PlayerPrefs.GetInt(Const.animForWinBtn) + "  is playerprefs values is corretc");
        switch (PlayerPrefs.GetInt(Const.animForWinBtn))
        {
            case 0:
                funTargetBet.betBtn[0].SetTrigger("btnAfterWin");
                Debug.Log("reached");
                break;

            case 1:
                funTargetBet.betBtn[1].SetTrigger("btnAfterWin");
                Debug.Log("reached");
                break;

            case 2:
                funTargetBet.betBtn[2].SetTrigger("btnAfterWin");
                Debug.Log("reached");
                break;

            case 3:
                funTargetBet.betBtn[3].SetTrigger("btnAfterWin");
                Debug.Log("reached");
                break;

            case 4:
                funTargetBet.betBtn[4].SetTrigger("btnAfterWin");
                Debug.Log("reached");
                break;

            case 5:
                funTargetBet.betBtn[5].SetTrigger("btnAfterWin");
                Debug.Log("reached");
                break;

            case 6:
                funTargetBet.betBtn[6].SetTrigger("btnAfterWin");
                Debug.Log("reached");
                break;

            case 7:
                funTargetBet.betBtn[7].SetTrigger("btnAfterWin");
                Debug.Log("reached");
                break;

            case 8:
                funTargetBet.betBtn[8].SetTrigger("btnAfterWin");
                Debug.Log("reached");
                break;

            case 9:
                funTargetBet.betBtn[9].SetTrigger("btnAfterWin");
                Debug.Log("reached");
                break;
        }
    }*/
}
