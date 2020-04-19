using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageResult : MonoBehaviour
{
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

        //scrollRect.verticalScrollbar.value = 0;//normalizedPosition = new Vector2(0.5f, 0.5f);

        playerRecordDataRows = new List<PlayerRecordDataRow>(dataRowParent.childCount);
        for (int i = 0; i < dataRowParent.childCount; ++i)
            playerRecordDataRows.Add(dataRowParent.GetChild(i).GetComponent<PlayerRecordDataRow>());
    }
}
