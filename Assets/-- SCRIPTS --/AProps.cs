using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CustomMidi;

public abstract class AProps : MonoBehaviour, IFixedPos
{
    
    [SerializeField] List<MidiKey> _keys = new List<MidiKey>();
    [SerializeField] bool _isNPC = false;

    protected virtual void Update()
    {
        transform.position -= transform.right * (Time.deltaTime * GameManager.Instance.PropsSpeed);
    }
}
