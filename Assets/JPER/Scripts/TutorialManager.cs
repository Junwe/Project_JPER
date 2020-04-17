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
        CLEAR
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

        SetTutorialMsg("포탈에 가면 클리어입니다!", 2);
        CameraWalking.Target = _trPotal;
        while (_step == TUTORIALSTEP.CLEAR)
        {
            
            yield return new WaitForEndOfFrame();
        }

        CameraWalking.Target = _trPlayer;
        objTutorialButton.gameObject.SetActive(false);
        tsTutorialMsg.gameObject.SetActive(false);
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
