using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    [SerializeField] private Image _cloud, _ok, _nice, _perfect, _transition;
    public static WinManager Instance { get; private set; }

    private void Awake()
    {
        _cloud.transform.localScale = Vector3.zero;
        
        if(Instance != null)
            Destroy(this.gameObject);

        Instance = this;
    }

    public IEnumerator SetWin(float score)
    {
        _cloud.transform.DOScale(1f, 1.5f).SetEase(Ease.OutExpo).WaitForCompletion();
        EInputPrecision inputPrecision;
        if (score >= 0.8f)
            inputPrecision = EInputPrecision.PERFECT;
        else if (score >= 0.6f)
            inputPrecision = EInputPrecision.NICE;
        else
            inputPrecision = EInputPrecision.OK;

        switch (inputPrecision)
        {
            case EInputPrecision.PERFECT:
                _perfect.transform.DOScale(1f, 1.5f).SetEase(Ease.OutExpo);
                break;
            case EInputPrecision.NICE:
                _nice.transform.DOScale(1f, 1.5f).SetEase(Ease.OutExpo);
                break;
            case EInputPrecision.OK:
                _ok.transform.DOScale(1f, 1.5f).SetEase(Ease.OutExpo);
                break;
            case EInputPrecision.MISSED:
            default:
                throw new ArgumentOutOfRangeException(nameof(inputPrecision), inputPrecision, null);
        }

        yield return new WaitForSecondsRealtime(3f);
        yield return _transition.DOFade(1f, 1f).WaitForCompletion();
        SceneManager.LoadScene("MainMenu");
    }
}
