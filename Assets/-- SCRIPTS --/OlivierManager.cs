using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlivierManager : MonoBehaviour
{

    [SerializeField] private AudioSource[] _olivier;
    [SerializeField] private int _bpm;

    private void Start()
    {
        StartCoroutine(OlivierStart());
    }

    private IEnumerator OlivierStart()
    {
        float wait = 60f / _bpm;
        yield return new WaitForSecondsRealtime(1f);
        _olivier[0].Play();
        yield return new WaitForSecondsRealtime(_olivier[0].clip.length);
        _olivier[1].Play();
        yield return new WaitForSecondsRealtime(_olivier[1].clip.length);
        _olivier[0].Play();
        yield return new WaitForSecondsRealtime(_olivier[0].clip.length/2);
        _olivier[1].Play();
        yield return new WaitForSecondsRealtime(_olivier[1].clip.length/2);
        _olivier[2].Play();
        yield return new WaitForSecondsRealtime(_olivier[2].clip.length/2);
        _olivier[3].Play();
    }
}
