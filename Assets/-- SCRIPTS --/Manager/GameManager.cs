using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { get; private set; }
    [field:SerializeField] public Player Player { get; private set; }
    [field:SerializeField] public float PosY { get; private set; }
    [field:SerializeField] public float PropsSpeed { get; private set; }
    [field:SerializeField] public Transform SpawnPoint { get; private set; }

    [SerializeField] private PlayableDirector[] _directors;
    [SerializeField] private AudioSource[] _audioSources;
    [SerializeField] private int[] _beatsNeeded;
    [SerializeField] private GameData _gameData;

    public Action onRealBeat, onPlayerBeat;
    private float _beatStatus = 0f;
    private int _currentLayer = 0;
    [SerializeField] private float _timeReset;
    [SerializeField] private Material _colorAura;

    private int _succesfulBeats = 0;
    
    public EInputPrecision InputPrecision
    {
        get
        {
            if (_beatStatus >= _gameData.perfectTolerance)
                return EInputPrecision.PERFECT;
            if (_beatStatus >= _gameData.niceTolerance)
                return EInputPrecision.NICE;
            return _beatStatus >= _gameData.okTolerance ? EInputPrecision.OK : EInputPrecision.MISSED;
        }
    }
    
    private float _score = 0f;
    [SerializeField] private float _timeAfterBeatValid;
    private void Update()
    {
        _beatStatus = Mathf.Clamp01(_beatStatus - Time.deltaTime / _timeAfterBeatValid);
        _audioSources[_currentLayer].volume = Mathf.Clamp(_audioSources[_currentLayer].volume - Time.deltaTime / _timeAfterBeatValid * 5f, 0.5f, 1f);
        if (CustomMidi.GetKeyDown(CustomMidi.MidiKey.NOTE_KEY) || Input.GetKeyDown(KeyCode.Space))
        {
            OnPlayerBeat();
            switch (InputPrecision)
            {
                case EInputPrecision.PERFECT:
                    _audioSources[_currentLayer].volume = 1f;
                    _colorAura.SetFloat("_Radius", _colorAura.GetFloat("_Radius") * 1.05f);
                    _succesfulBeats++;
                    break;
                case EInputPrecision.NICE:
                    _audioSources[_currentLayer].volume = .9f;
                    _colorAura.SetFloat("_Radius", _colorAura.GetFloat("_Radius") * 1.03f);
                    _succesfulBeats++;
                    break;
                case EInputPrecision.OK:
                    _audioSources[_currentLayer].volume = .8f;
                    _colorAura.SetFloat("_Radius", _colorAura.GetFloat("_Radius") * 1.01f);
                    _succesfulBeats++;
                    break;
                case EInputPrecision.MISSED:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void Awake()
    {
        if (Instance != null)
            Destroy(this.gameObject);

        Instance = this;
        
        StartCoroutine(StartBeat());
        _colorAura.SetFloat("_Radius", 0.15f);
    }

    private IEnumerator StartBeat()
    {
        foreach (var director in _directors)
        {
            director.Play();
        }
        yield return new WaitForSecondsRealtime(1f);
        foreach (var director in _directors)
        {
            director.Pause();
        }
        foreach (var director in _directors)
        {
            director.time = 0;
        }
        yield return new WaitForSecondsRealtime(1f);
        foreach (var director in _directors)
        {
            director.Play();
        }

        for (int i = 1; i < _audioSources.Length; i++)
        {
            _audioSources[i].volume = 0f;
        }
    }

    public void OnRealBeat()
    {
        onRealBeat?.Invoke();

        _beatStatus = 1f;
    }

    public void OnPlayerBeat()
    {
        onPlayerBeat?.Invoke();
        Debug.Log(InputPrecision);
    }

    public void ResetBeat()
    {
        foreach (var director in _directors)
        {
            director.Pause();
            director.time = _timeReset / 60f;
            director.Play();
        }

        if (_succesfulBeats > _beatsNeeded[_currentLayer])
        {
            _audioSources[_currentLayer].DOFade(0.5f, 1f);
            if (_audioSources.Length > _currentLayer + 1)
            {
                _currentLayer++;
                _audioSources[_currentLayer].DOFade(0.1f, 1f);
            }
            else
            {
                Debug.Log("WIN");
                DOTween.To(() => _colorAura.GetFloat("_Radius"), x => _colorAura.SetFloat("_Radius", x), 10f, 3f);
            }
        }
        Debug.Log(_succesfulBeats);
        _succesfulBeats = 0;
    }

    #if UNITY_EDITOR
    private void OnValidate()
    {
        foreach (var fixedPose in Editor.FindObjectsByType<GameObject>(FindObjectsSortMode.InstanceID)) if(fixedPose.GetComponent<IFixedPos>() != null)
        {
            fixedPose.transform.position = new Vector3(fixedPose.transform.position.x, PosY, fixedPose.transform.position.z);
        }
    }
    #endif
}

public interface IFixedPos{}

public enum EInputPrecision
{
    PERFECT,
    NICE,
    OK,
    MISSED
}
