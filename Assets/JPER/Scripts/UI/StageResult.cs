using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageResult : MonoBehaviour
{
    [SerializeField]
    ParticleSystem[] _reulstParticleList;

    public void PlayReulst()
    {
        foreach (var particle in _reulstParticleList)
        {
            StartCoroutine(PlayFireWork(Random.Range(0f, 1.5f), particle));
        }
    }

    private IEnumerator PlayFireWork(float delay, ParticleSystem particle)
    {
        yield return new WaitForSeconds(delay);
        particle.Play();
    }
}
