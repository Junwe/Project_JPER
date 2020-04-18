using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialManager : MonoBehaviour
{
    private enum TUTORIALSTEP
    {
        MOVE,
        JUIMP,
        CLEAR,
        GAMESTART,
    }
    public Text txtTutorial;
    public TweenScale tsTutorialMsg;
    //public TweenMovePingPong tmTutorialMsgRoot;

    public Transform[] trMsgPositionList;
    public GameObject objTutorialButton;
    public CameraWalking CameraWalking;
    private TUTORIALSTEP _step = TUTORIALSTEP.MOVE;


    private Transform _trPlayer;
    private Transform _trPotal;

    public void StartTutorial()
    {
        _trPotal = GameObject.Find("G_Portal").GetComponent<Transform>();
        _trPlayer = GameObject.Find("G_Player").GetComponent<Transform>();

        tsTutorialMsg.gameObject.SetActive(true);
        objTutorialButton.gameObject.SetActive(true);
        //tmTutorialMsgRoot.StartTween();
        StartCoroutine(TutorialRutin());
    }
    IEnumerator TutorialRutin()
    {
        SetTutorialMsg("화살표로 움직일 수 있어요!", 0);
        while (_step == TUTORIALSTEP.MOVE)
        {

            yield return new WaitForEndOfFrame();
        }

        SetTutorialMsg("점프를 할 수 있어요!", 1);
        while (_step == TUTORIALSTEP.JUIMP)
        {

            yield return new WaitForEndOfFrame();
        }
        SetCameraTarget(false);
        SetTutorialMsg("포탈에 가면 클리어입니다!", 2);
        while (_step == TUTORIALSTEP.CLEAR)
        {

            yield return new WaitForEndOfFrame();
        }
        SetCameraTarget(true);
        SetTutorialMsg("화이팅 :)", 2);
        while (_step == TUTORIALSTEP.GAMESTART)
        {
            yield return new WaitForEndOfFrame();
        }
        objTutorialButton.gameObject.SetActive(false);
        tsTutorialMsg.gameObject.SetActive(false);
    }

    private void SetCameraTarget(bool player)
    {
        if (player)
        {
            Invoke("EnablePlayerAnimation", 1f);
            CameraWalking.Target = _trPlayer;
            CameraWalking.SetCameraSize(10.8f, 1f);
        }
        else
        {
            CameraWalking.Target = _trPotal;
            CameraWalking.SetCameraSize(5f, 1f);
            _trPlayer.GetComponentInParent<Animator>().enabled = false;
        }
    }
    private void EnablePlayerAnimation()
    {
        _trPlayer.GetComponentInParent<Animator>().enabled = true;
    }
    private void SetTutorialMsg(string msg, int pos)
    {
        txtTutorial.text = msg;
        txtTutorial.transform.position = trMsgPositionList[pos].position;
        tsTutorialMsg.StartTween();
    }

    public void ClickNextStep()
    {
        _step = (TUTORIALSTEP)((int)_step + 1);
    }

}
