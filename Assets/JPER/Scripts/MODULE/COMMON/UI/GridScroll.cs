using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScroll : MonoBehaviour
{
    public float StageDistance;
    public Vector3 StageStartPos;
    private int _currentSelectStageIndex = 0;
    private List<RectTransform> _objStageList = new List<RectTransform>();
    private float _scrollSpeed = 0.5f;

    public void AddStageRectTransform(RectTransform rect)
    {
        _objStageList.Add(rect);

        rect.transform.localPosition = new Vector3(StageStartPos.x + (StageDistance * (_objStageList.Count - 1)), StageStartPos.y, 0f);
    }

    public void NextItem()
    {
        if (_objStageList.Count - 1 > _currentSelectStageIndex)
        {
            foreach (var obj in _objStageList)
            {
                Vector3 endPos = new Vector3(obj.localPosition.x - StageDistance, StageStartPos.y, 0f);
                StartCoroutine(Tween.Instance.Move(obj, obj.localPosition, endPos, _scrollSpeed, 0f,
                AnimationCurve.EaseInOut(0f, 0f, 1f, 1f)));
            }
            _currentSelectStageIndex++;
        }
    }
    public void PreItem()
    {
        if (_currentSelectStageIndex > 0)
        {
            foreach (var obj in _objStageList)
            {
                Vector3 endPos = new Vector3(obj.localPosition.x + StageDistance, StageStartPos.y, 0f);
                StartCoroutine(Tween.Instance.Move(obj, obj.localPosition, endPos, _scrollSpeed, 0f,
                AnimationCurve.EaseInOut(0f, 0f, 1f, 1f)));
            }
            _currentSelectStageIndex--;
        }
    }
}
