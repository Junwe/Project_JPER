using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageResult : MonoBehaviour
{
    [Range(0f, 1f)]
    public float scrollPos = 0;

    [SerializeField]
    ParticleSystem[] _reulstParticleList;
    [SerializeField]
    private Transform dataRowParent;
    [SerializeField]
    ParticleSystem[] _paperParticleList;
    [SerializeField]
    GameObject _objDataUI;
    [SerializeField]
    Transform _trDataParent;
    [SerializeField]
    Button _btnAction;
    [SerializeField]
    private ScrollRect scrollRect;

    private List<PlayerRecordDataRow> playerRecordDataRows = null;

    public void PlayReulst()
    {
        _btnAction.enabled = false;
        foreach (var particle in _reulstParticleList)
        {
            StartCoroutine(PlayFireWork(Random.Range(0f, 3.5f), particle));
        }
        foreach (var particle in _paperParticleList)
        {
            particle.Play();
        }

        for (int i = 0; i < playerRecordDataRows.Count; ++i)
            playerRecordDataRows[i].UpdateDataValue();

        StartCoroutine(SetScrollRectVerticalPosition());
    }

    private IEnumerator SetScrollRectVerticalPosition()
    {
        var transformRef = transform;
        //var temp = scrollRect.velocity;
        scrollRect.verticalNormalizedPosition = 0;

        while (transform.localScale.x < 0.975f)
        {
            scrollRect.verticalNormalizedPosition = transform.localScale.x;
            yield return null;
        }

        scrollRect.verticalNormalizedPosition = 1;
        //scrollRect.velocity = new Vector2(0, 1); //temp;
    }

    private IEnumerator PlayFireWork(float delay, ParticleSystem particle)
    {
        yield return new WaitForSeconds(delay);
        particle.Play();
        Sound.Instance.PlayEffSound(SOUND.S_FIREWORK);
    }

    private void Start()
    {
        foreach (PlayerActionCounter.RecordDataType data in System.Enum.GetValues(typeof(PlayerActionCounter.RecordDataType)))
        {
            if (data != PlayerActionCounter.RecordDataType.None)
            {
                GameObject dataobj = Instantiate(_objDataUI, _trDataParent);
                dataobj.GetComponent<PlayerRecordDataRow>().SetType(data);
            }
        }

        playerRecordDataRows = new List<PlayerRecordDataRow>(dataRowParent.childCount);
        for (int i = 0; i < dataRowParent.childCount; ++i)
            playerRecordDataRows.Add(dataRowParent.GetChild(i).GetComponent<PlayerRecordDataRow>());
    }

#if UNITY_EDITOR
    private void Update()
    {
        //scrollRect.verticalNormalizedPosition = scrollPos;
    }
#endif
}
