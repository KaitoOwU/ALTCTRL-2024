using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPC : AProps
{
    private List<CustomMidi.MidiKey> _firstCombinaison = new() { CustomMidi.MidiKey.LEFT_WHITE, CustomMidi.MidiKey.RIGHT_WHITE };
    private List<CustomMidi.MidiKey> _secondCombinaison = new() { CustomMidi.MidiKey.FIRST_BLACK, CustomMidi.MidiKey.SECOND_BLACK, CustomMidi.MidiKey.THIRD_BLACK, CustomMidi.MidiKey.FOURTH_BLACK };
    private Dictionary<CustomMidi.MidiKey, string> _textChar = new()
    {
        { CustomMidi.MidiKey.LEFT_WHITE, "<color=blue>D" },
        { CustomMidi.MidiKey.RIGHT_WHITE, "<color=red>L" },
        { CustomMidi.MidiKey.FIRST_BLACK, "<color=black>E" },
        { CustomMidi.MidiKey.SECOND_BLACK, "<color=black>A" },
        { CustomMidi.MidiKey.THIRD_BLACK, "<color=black>I" },
        { CustomMidi.MidiKey.FOURTH_BLACK, "<color=black>O" },
    };
    
    [SerializeField] private GameObject _goDemand;
    [SerializeField] private TMP_Text _tDemand;
    [SerializeField] private float _rangeOfAppearition;
    
    private (CustomMidi.MidiKey first, CustomMidi.MidiKey second) _combinaison;

    private void Start()
    {
        this.transform.position = GameManager.Instance.SpawnPoint.position;
        _combinaison = (_firstCombinaison[Random.Range(0, _firstCombinaison.Count - 1)], _secondCombinaison[Random.Range(0, _secondCombinaison.Count - 1)]);
        _tDemand.text = _textChar[_combinaison.first] + _textChar[_combinaison.second];
    }

    protected override void Update()
    {
        base.Update();

        if (CustomMidi.GetKey(_combinaison.first) && CustomMidi.GetKey(_combinaison.second))
        {
            Destroy(gameObject);
        }

        if (_goDemand.activeInHierarchy == false)
        {
            //Debug.Log(Vector3.Distance(GameManager.Instance.Player.transform.position, this.transform.position));
            if (Vector3.Distance(GameManager.Instance.Player.transform.position, this.transform.position) <
                _rangeOfAppearition)
            {
                _goDemand.SetActive(true);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _rangeOfAppearition);
    }
}
