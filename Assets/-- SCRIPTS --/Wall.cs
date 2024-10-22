using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Wall : AProps
{
    /*[SerializeField] private GameObject _goDemand;
    [SerializeField] private TMP_Text _tDemand;
    [SerializeField] private float _rangeOfAppearition;

    private CustomMidi.MidiKey _chosenKey;
    private List<CustomMidi.MidiKey> _allPossibleKeys = new() { CustomMidi.MidiKey.PAD_1, CustomMidi.MidiKey.PAD_2, CustomMidi.MidiKey.PAD_3, CustomMidi.MidiKey.PAD_4};

    private Dictionary<CustomMidi.MidiKey, string> _textChar = new()
    {
        { CustomMidi.MidiKey.PAD_1, "<color=red>1" },
        { CustomMidi.MidiKey.PAD_2, "<color=red>2" },
        { CustomMidi.MidiKey.PAD_3, "<color=red>3" },
        { CustomMidi.MidiKey.PAD_4, "<color=red>4" },
    };

    private void Start()
    {
        this.transform.position = GameManager.Instance.SpawnPoint.position;
        _chosenKey = _allPossibleKeys[Random.Range(0, _allPossibleKeys.Count - 1)];
        _tDemand.text = _textChar[_chosenKey];
        Debug.Log("chosenKey: " + _chosenKey);
    }

    private void Update()
    {
        base.Update();

        if (CustomMidi.GetKey(_chosenKey))
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
    }*/
}
