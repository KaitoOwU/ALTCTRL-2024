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
    public GameData GameData { get; private set; }

    [SerializeField] private TMP_Text _tmp, _scoring;
    [SerializeField] private PlayableDirector _director;
    [SerializeField] private AudioSource _audioSource;

    public Action onBeat;
    [SerializeField] private SignalReceiver _signals;
    private float _beatStatus;
    
    public EInputPrecision InputPrecision
    {
        get
        {
            if (_beatStatus >= GameData.perfectTolerance)
                return EInputPrecision.PERFECT;
            if (_beatStatus >= GameData.niceTolerance)
                return EInputPrecision.NICE;
            return _beatStatus >= GameData.okTolerance ? EInputPrecision.OK : EInputPrecision.MISSED;
        }
    }
    
    private float _score = 0f;
    [SerializeField] private float _timeAfterBeatValid;

    private int _currentCurve = 0;
    private void Update()
    {
        _beatStatus = Mathf.Clamp01(_beatStatus - Time.deltaTime / _timeAfterBeatValid);
        _tmp.text = Math.Round(_beatStatus).ToString(CultureInfo.CurrentCulture);
        if (CustomMidi.GetKeyDown(CustomMidi.MidiKey.NOTE_KEY) || Input.GetKeyDown(KeyCode.Space))
        {
            switch (InputPrecision)
            {
                case EInputPrecision.MISSED:
                    _scoring.text = $"<color=#{Color.red.ToHexString()}><b>MISSED</b></color><br>" + _scoring.text;
                    break;
                case EInputPrecision.OK:
                    _scoring.text = $"<color=#{Color.yellow.ToHexString()}><b>OK</b></color><br>" + _scoring.text;
                    break;
                case EInputPrecision.NICE:
                    _scoring.text = $"<color=#{Color.cyan.ToHexString()}><b>NICE</b></color><br>" + _scoring.text;
                    break;
                case EInputPrecision.PERFECT:
                    _scoring.text = $"<color=#{Color.green.ToHexString()}><b>GREEN</b></color><br>" + _scoring.text;
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
        GameData = Resources.Load<GameData>("GameData");

        Instance = this;
        
        StartCoroutine(StartBeat());
    }

    private IEnumerator StartBeat()
    {
        _director.Play();
        yield return new WaitForSecondsRealtime(1f);
        _director.Pause();
        _director.time = 0;
        yield return new WaitForSecondsRealtime(1f);
        _director.Play();
    }

    public void OnBeat()
    {
        onBeat?.Invoke();

        _beatStatus = 1f;
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
    MISSED,
    OK,
    NICE,
    PERFECT
}
