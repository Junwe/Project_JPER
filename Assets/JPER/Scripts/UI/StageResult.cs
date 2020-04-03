using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageResult : MonoBehaviour
{
    [SerializeField]
    ParticleSystem[] _reulstParticleList;
    [SerializeField]
    ParticleSystem[] _paperParticleList;

    public void PlayReulst()
    {
        foreach (var particle in _reulstParticleList)
        {
            StartCoroutine(PlayFireWork(Random.Range(0f, 3.5f), particle));
        }
        foreach(var particle in _paperParticleList)
        {
            particle.Play();
        }
    }

    private IEnumerator PlayFireWork(float delay, ParticleSystem particle)
    {
        yield return new WaitForSeconds(delay);
        particle.Play();
    }
}
