using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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

    [SerializeField] private TMP_Text _tmp, _scoring;
    [SerializeField] private PlayableDirector _directorMelody, _directorDrums;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameData _gameData;

    public Action onRealBeat, onPlayerBeat;
    [SerializeField] private SignalReceiver _signals;
    private float _beatStatus = 0f;
    
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

    private int _currentCurve = 0;
    private void Update()
    {
        _beatStatus = Mathf.Clamp01(_beatStatus - Time.deltaTime / _timeAfterBeatValid);
        _audioSource.volume = Mathf.Clamp(_audioSource.volume - Time.deltaTime / _timeAfterBeatValid * 5f, 0.3f, 1f);
        _tmp.text = Math.Round(_beatStatus).ToString(CultureInfo.CurrentCulture);
        if (CustomMidi.GetKeyDown(CustomMidi.MidiKey.NOTE_KEY) || Input.GetKeyDown(KeyCode.Space))
        {
            OnPlayerBeat();
            switch (InputPrecision)
            {
                case EInputPrecision.PERFECT:
                    _scoring.text = $"<color=#{Color.green.ToHexString()}>PERFECT</color><br>" + _scoring.text;
                    _audioSource.volume = 1f;
                    break;
                case EInputPrecision.NICE:
                    _scoring.text = $"<color=#{Color.cyan.ToHexString()}>NICE</color><br>" + _scoring.text;
                    _audioSource.volume = .9f;
                    break;
                case EInputPrecision.OK:
                    _scoring.text = $"<color=#{Color.yellow.ToHexString()}>OK</color><br>" + _scoring.text;
                    _audioSource.volume = .8f;
                    break;
                case EInputPrecision.MISSED:
                    _scoring.text = $"<color=#{Color.red.ToHexString()}>MISSED</color><br>" + _scoring.text;
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
    }

    private IEnumerator StartBeat()
    {
        _directorMelody.Play();
        _directorDrums.Play();
        yield return new WaitForSecondsRealtime(1f);
        _directorMelody.Pause();
        _directorDrums.Pause();
        _directorMelody.time = 0;
        _directorDrums.time = 0;
        yield return new WaitForSecondsRealtime(1f);
        _directorMelody.Play();
        _directorDrums.Play();
    }

    public void OnRealBeat()
    {
        onRealBeat?.Invoke();

        _beatStatus = 1f;
    }

    public void OnPlayerBeat()
    {
        onPlayerBeat?.Invoke();
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
