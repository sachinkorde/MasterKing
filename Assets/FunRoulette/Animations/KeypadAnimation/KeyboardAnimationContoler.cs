using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardAnimationContoler : MonoBehaviour
{
    public static bool iskeyboarAnim=false;

    public GameObject Betok,cancelBet,cancelSpecific,keybord_test,KeyboardMain,
        BettingChiops,Take,zoombtn,wheel;

    public static GameObject _Betok, _cancelBet, _cancelSpecific, _keybord_test, _KeyboardMain,
        _BettingChiops, _Take, _zoombtn,_wheel;

    public static KeyboardAnimationContoler _inst;
    private void Awake()
    {
        _inst = this;

        iskeyboarAnim = false;
     
        _Betok = Betok;
        _cancelBet = cancelBet;
        _cancelSpecific = cancelSpecific;
        _keybord_test = keybord_test;
        _KeyboardMain = KeyboardMain;
        _BettingChiops = BettingChiops;
        _Take = Take;
        _zoombtn = zoombtn;
        _wheel = wheel;
      //  _Betok.GetComponent<Animator>().enabled = false;
      //  _cancelBet.GetComponent<Animator>().enabled = false;
      //  _cancelSpecific.GetComponent<Animator>().enabled = false;
      //  _keybord_test.GetComponent<Animator>().enabled = false;
      //  _KeyboardMain.GetComponent<Animator>().enabled = false;
      //   _BettingChiops.GetComponent<Animator>().enabled = false;
      //  _zoombtn.GetComponent<Animator>().enabled = false;

    }

  /*  public static void StartAnimaton()
    {
        _Take.SetActive(false);
        _wheel.SetActive(false);
        _Betok.GetComponent<Animator>().enabled = true;
        _cancelBet.GetComponent<Animator>().enabled = true;
        _cancelSpecific.GetComponent<Animator>().enabled = true;
        _keybord_test.GetComponent<Animator>().enabled = true;
        _KeyboardMain.GetComponent<Animator>().enabled = true;
        _BettingChiops.GetComponent<Animator>().enabled = true;
        _zoombtn.GetComponent<Animator>().enabled = true;

    }
  */

    public static void AnimationIn()
    {
        _Take.SetActive(false);
        _wheel.SetActive(false);
        // Debug.Log("hi");
        _BettingChiops.GetComponent<Animator>().SetBool("in", true);
        _Betok.GetComponent<Animator>().SetBool("in", true);
        _cancelBet.GetComponent<Animator>().SetBool("in", true);
        _cancelSpecific.GetComponent<Animator>().SetBool("in", true);
        _keybord_test.GetComponent<Animator>().SetBool("in", true);
        _KeyboardMain.GetComponent<Animator>().SetBool("in", true);
        _zoombtn.GetComponent<Animator>().SetBool("in", true);
    }

    public static void AnimationOut()
    {
        
        // Debug.Log("hi2");
        _BettingChiops.GetComponent<Animator>().SetBool("in", false);
        _Betok.GetComponent<Animator>().SetBool("in", false);
        _cancelBet.GetComponent<Animator>().SetBool("in", false);
        _cancelSpecific.GetComponent<Animator>().SetBool("in", false);
        _keybord_test.GetComponent<Animator>().SetBool("in", false);
        _KeyboardMain.GetComponent<Animator>().SetBool("in", false);
        _zoombtn.GetComponent<Animator>().SetBool("in", false);
        _inst.xyz();
    }

    public void xyz()
    {
        Invoke("Delay",0.5f);
    }

    void Delay()
    {
        _Take.SetActive(true);
        _wheel.SetActive(true);
    }
}
