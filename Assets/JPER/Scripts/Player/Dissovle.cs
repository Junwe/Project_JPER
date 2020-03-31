using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissovle : MonoBehaviour
{
    private Material _material;

    private float _fade = 1f;
    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(StartDissovle());
        }
    }

    IEnumerator StartDissovle()
    {
        while (_fade >= 0f)
        {
            _fade -= Time.deltaTime;

            _material.SetFloat("_fade", _fade);

            yield return new WaitForEndOfFrame();
        }
        _fade = 1f;

        _material.SetFloat("_fade", _fade);
    }
}
