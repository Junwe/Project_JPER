using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrap : MonoBehaviour
{
    public enum MovingType
    {
        Loop,
        OneDicrection
    }

    public List<Vector3> points;

    public bool playOneShot = false;
    public MovingType movingType;

    [SerializeField]
    private float startTime = 0;
    [SerializeField]
    private float movingTime;
    [SerializeField]
    private float waitTime;
    [SerializeField]
    private bool isMoveWithPlayer;

    public void ValueCopy(MovingTrap moving)
    {
        points = moving.points;
        playOneShot = moving.playOneShot;
        movingType = moving.movingType;
        movingTime = moving.movingTime;
        waitTime = moving.waitTime;
        isMoveWithPlayer = moving.isMoveWithPlayer;
    }

    public void StartMoving()
    {
        if (points.Count > 1 && movingTime > 0)
            StartCoroutine(Moving());
    }

    public void StopMoving()
    {
        StopCoroutine(Moving());
    }

    private void Start()
    {
        StartMoving();
    }

    private IEnumerator Moving()
    {
        float elapsedTime = 0;
        int direction = 1; // 1 또는 -1 만 사용.
        var trapTransform = transform;
        var waitSec = new WaitForSeconds(waitTime);

        if(startTime > 0)
            yield return new WaitForSeconds(startTime);

        do
        {
            elapsedTime += Time.deltaTime * direction;
            if (elapsedTime >= movingTime || elapsedTime <= 0)
            {
                switch (movingType)
                {
                    case MovingType.Loop:
                        elapsedTime = elapsedTime >= movingTime ? movingTime : 0;
                        int index = direction < 0 ? 0 : points.Count - 1;
                        trapTransform.position = points[index];
                        direction *= -1;
                        break;
                    case MovingType.OneDicrection:
                        elapsedTime = 0;
                        direction = 1;
                        trapTransform.position = points[points.Count - 1];
                        break;
                }
                yield return waitSec;
            }

            float percent = elapsedTime / movingTime;
            float temp = percent * (points.Count - 1);
            int fromIndex = Mathf.FloorToInt(temp);
            int toIndex = Mathf.CeilToInt(temp);
            float alpha = fromIndex == 0 ? temp : temp % fromIndex;
            trapTransform.position = Vector3.Lerp(points[fromIndex], points[toIndex], alpha);

            yield return null;
        }
        while (playOneShot == false);
    }
}
