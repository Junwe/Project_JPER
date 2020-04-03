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

    private PlayerActionCounter playerActionCounter = null;
    private List<PlayerRecordDataRow> playerRecordDataRows = null;

    public void PlayReulst()
    {
        foreach (var particle in _reulstParticleList)
        {
            StartCoroutine(PlayFireWork(Random.Range(0f, 1.5f), particle));
        }

        for (int i = 0; i < playerRecordDataRows.Count; ++i)
            playerRecordDataRows[i].UpdateDataValue();
    }

    private IEnumerator PlayFireWork(float delay, ParticleSystem particle)
    {
        yield return new WaitForSeconds(delay);
        particle.Play();
    }

    private void Start()
    {
        playerActionCounter = GameManager.Instance.playerActionCounter;
        playerRecordDataRows = new List<PlayerRecordDataRow>(dataRowParent.childCount);
        for (int i = 0; i < dataRowParent.childCount; ++i)
            playerRecordDataRows.Add(dataRowParent.GetChild(i).GetComponent<PlayerRecordDataRow>());
    }
}
