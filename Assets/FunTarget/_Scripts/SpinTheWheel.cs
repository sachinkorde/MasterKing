using System.Collections;
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

    public void WheelSpinHere()
    {
        FT_SoundManager.instance.PlayAudioClip(FT_GameClips.SpeenWheelRotate);
        spinCenter.SetTrigger("runSpin");
        wheelTheAnimator.SetTrigger("wheelrotation");

        float clipLength = FT_SoundManager.instance.ft_AudioSorce.clip.length;
        Invoke(nameof(SpinTheWheelAnim), clipLength);
    }

    public void SpinTheWheelAnim()
    {
        spinCenter.SetTrigger("idle");

        /*for (int i = 0; i < funTargetBet.betBtn.Count; i++)
        {
            funTargetBet.betBtn[i].SetTrigger("idle");
        }*/

        ShowResult();

        switch (PlayerPrefs.GetInt(Const.winNumber))
        {
            case 0:
                wheelTheAnimator.SetTrigger("0");
                funTargetBet.betBtn[0].SetTrigger("btnAfterWin");
                break;

            case 1:
                wheelTheAnimator.SetTrigger("1");
                funTargetBet.betBtn[1].SetTrigger("btnAfterWin");
                break;

            case 2: 
                wheelTheAnimator.SetTrigger("2");
                funTargetBet.betBtn[2].SetTrigger("btnAfterWin");
                break;

            case 3:
                wheelTheAnimator.SetTrigger("3");
                funTargetBet.betBtn[3].SetTrigger("btnAfterWin");
                break;

            case 4:
                wheelTheAnimator.SetTrigger("4");
                funTargetBet.betBtn[4].SetTrigger("btnAfterWin");
                break;

            case 5:
                wheelTheAnimator.SetTrigger("5");
                funTargetBet.betBtn[5].SetTrigger("btnAfterWin");
                break;

            case 6:
                wheelTheAnimator.SetTrigger("6");
                funTargetBet.betBtn[6].SetTrigger("btnAfterWin");
                break;

            case 7:
                wheelTheAnimator.SetTrigger("7");
                funTargetBet.betBtn[7].SetTrigger("btnAfterWin");
                break;

            case 8:
                wheelTheAnimator.SetTrigger("8");
                funTargetBet.betBtn[8].SetTrigger("btnAfterWin");
                break;

            case 9:
                wheelTheAnimator.SetTrigger("9");
                funTargetBet.betBtn[9].SetTrigger("btnAfterWin");
                break;
        }
        PlayerPrefs.SetInt(Const.isWheelRotate, 1);
        FT_SoundManager.instance.ft_AudioSorce.Stop();
    }

    void ShowResult()
    {
        /*if (!funTargetBet.isDataNull)
        {
            funTargetBet.isDataNull = false;
        }*/
        //funTargetBet.ShowWinNumInLast10Data();

        funTargetAPIManager.ShowResultWithLastTranData();
        Invoke(nameof(StartGameAgain), 0.5f);
    }

    void StartGameAgain()
    {
        funTargetBet.btnHider.SetActive(false);
        //funTargetBet.isDataSendOnClick = false;
        PlayerPrefs.SetInt(Const.isDataSendOnClick, 0);
        funTargetBet.isBetOk = false;
        funTargetBet.allAmtClickCounter = 0;
        funTargetAPIManager.ShowDataOfLast10WinNum();

        Invoke(nameof(WinButtonAnimation), 0.25f);
    }

    void WinButtonAnimation()
    {
        switch (PlayerPrefs.GetInt(Const.winNumber))
        {
            case 0:
                funTargetBet.betBtn[0].SetTrigger("btnAfterWin");
                break;

            case 1:
                funTargetBet.betBtn[1].SetTrigger("btnAfterWin");
                break;

            case 2:
                wheelTheAnimator.SetTrigger("2");
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
