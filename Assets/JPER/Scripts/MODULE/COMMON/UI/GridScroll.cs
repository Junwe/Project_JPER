using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScroll : MonoBehaviour
{
    public float StageDistance;
    public Vector3 StageStartPos;
    private int _currentSelectStageIndex = 0;
    private List<Transform> _objStageList = new List<Transform>();
    private float _scrollSpeed = 0.5f;

    public void AddStageRectTransform(Transform rect)
    {
        _objStageList.Add(rect);

        rect.transform.parent.localPosition = new Vector3(StageStartPos.x + (StageDistance * (_objStageList.Count - 1)),
        StageStartPos.y - 150f, 0f);
    }

    public void NextItem()
    {
        if (_objStageList.Count - 1 > _currentSelectStageIndex)
        {
            StopAllCoroutines();
            SetObjectPosition();
            foreach (var obj in _objStageList)
            {
                Vector3 endPos = new Vector3(obj.transform.parent.localPosition.x - StageDistance, StageStartPos.y - 150f, 0f);
                StartCoroutine(Tween.Instance.Move(obj.transform.parent.gameObject, obj.transform.parent.localPosition, endPos, _scrollSpeed, 0f,
                AnimationCurve.EaseInOut(0f, 0f, 1f, 1f)));
            }
            _currentSelectStageIndex++;
        }
    }
    public void PreItem()
    {
        if (_currentSelectStageIndex > 0)
        {
            StopAllCoroutines();
            SetObjectPosition();
            foreach (var obj in _objStageList)
            {
                Vector3 endPos = new Vector3(obj.transform.parent.localPosition.x + StageDistance, StageStartPos.y - 150f, 0f);
                StartCoroutine(Tween.Instance.Move(obj.transform.parent.gameObject, obj.transform.parent.localPosition, endPos, _scrollSpeed, 0f,
                AnimationCurve.EaseInOut(0f, 0f, 1f, 1f)));
            }
            _currentSelectStageIndex--;
        }
    }

    private void SetObjectPosition()
    {
        int objIndex = 0;
        for (int i = -_currentSelectStageIndex; i < _objStageList.Count - _currentSelectStageIndex; ++i)
        {
            Vector3 endPos = new Vector3(StageStartPos.x + (i * StageDistance), StageStartPos.y - 150f, 0f);
            _objStageList[objIndex].parent.localPosition = endPos;
            objIndex++;
        }
    }
}
