using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CustomMidi;

public abstract class AProps : MonoBehaviour
{
    [SerializeField] List<MidiKey> _keys = new List<MidiKey>();
    [SerializeField] bool _isNPC = false;

    public abstract void AskInputs();
}
