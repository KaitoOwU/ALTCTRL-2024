using System;
using System.Collections;
using System.Collections.Generic;
using MidiJack;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    private void Update()
    {
        if (CustomMidi.GetKeyDown(CustomMidi.MidiKey.LEFT_WHITE))
        {
            Debug.Log("LEFT_WHITE");
        } else if (CustomMidi.GetKeyDown(CustomMidi.MidiKey.RIGHT_WHITE))
        {
            Debug.Log("RIGHT_WHITE");
        } else if (CustomMidi.GetKeyDown(CustomMidi.MidiKey.FIRST_BLACK))
        {
            Debug.Log("FIRST_BLACK");
        } else if (CustomMidi.GetKeyDown(CustomMidi.MidiKey.SECOND_BLACK))
        {
            Debug.Log("SECOND_BLACK");
        } else if (CustomMidi.GetKeyDown(CustomMidi.MidiKey.THIRD_BLACK))
        {
            Debug.Log("THIRD_BLACK");
        } else if (CustomMidi.GetKeyDown(CustomMidi.MidiKey.FOURTH_BLACK))
        {
            Debug.Log("FOURTH_BLACK");
        } else if (CustomMidi.GetKeyDown(CustomMidi.MidiKey.PAD_1))
        {
            Debug.Log("PAD_1");
        } else if (CustomMidi.GetKeyDown(CustomMidi.MidiKey.PAD_2))
        {
            Debug.Log("PAD_2");
        } else if (CustomMidi.GetKeyDown(CustomMidi.MidiKey.PAD_3))
        {
            Debug.Log("PAD_3");
        } else if (CustomMidi.GetKeyDown(CustomMidi.MidiKey.PAD_4))
        {
            Debug.Log("PAD_4");
        }
    }
}

public static class CustomMidi
{

    public static bool GetKeyDown(MidiKey midiKey)
    {
        switch (midiKey)
        {
            case MidiKey.LEFT_WHITE:
                return MidiMaster.GetKeyDown(0) ||
                       MidiMaster.GetKeyDown(2) ||
                       MidiMaster.GetKeyDown(4) ||
                       MidiMaster.GetKeyDown(5) ||
                       MidiMaster.GetKeyDown(7) ||
                       MidiMaster.GetKeyDown(9) ||
                       MidiMaster.GetKeyDown(11);
            case MidiKey.RIGHT_WHITE:
                return MidiMaster.GetKeyDown(12) ||
                       MidiMaster.GetKeyDown(14) ||
                       MidiMaster.GetKeyDown(16) ||
                       MidiMaster.GetKeyDown(17) ||
                       MidiMaster.GetKeyDown(19) ||
                       MidiMaster.GetKeyDown(21) ||
                       MidiMaster.GetKeyDown(23) ||
                       MidiMaster.GetKeyDown(24);
            case MidiKey.FIRST_BLACK:
                return MidiMaster.GetKeyDown(1) ||
                       MidiMaster.GetKeyDown(3);
            case MidiKey.SECOND_BLACK:
                return MidiMaster.GetKeyDown(6) ||
                       MidiMaster.GetKeyDown(8) ||
                       MidiMaster.GetKeyDown(10);
            case MidiKey.THIRD_BLACK:
                return MidiMaster.GetKeyDown(13) ||
                       MidiMaster.GetKeyDown(15);
            case MidiKey.FOURTH_BLACK:
                return MidiMaster.GetKeyDown(18) ||
                       MidiMaster.GetKeyDown(20) ||
                       MidiMaster.GetKeyDown(22);
            case MidiKey.PAD_1:
                return MidiMaster.GetKeyDown(36) ||
                       MidiMaster.GetKeyDown(40);
            case MidiKey.PAD_2:
                return MidiMaster.GetKeyDown(37) ||
                       MidiMaster.GetKeyDown(41);
            case MidiKey.PAD_3:
                return MidiMaster.GetKeyDown(38) ||
                       MidiMaster.GetKeyDown(42);
            case MidiKey.PAD_4:
                return MidiMaster.GetKeyDown(39) ||
                       MidiMaster.GetKeyDown(43);
            default:
                throw new ArgumentOutOfRangeException(nameof(midiKey), midiKey, null);
        }
    }

    public enum MidiKey
    {
        LEFT_WHITE,
        RIGHT_WHITE,
        FIRST_BLACK,
        SECOND_BLACK,
        THIRD_BLACK,
        FOURTH_BLACK,
        PAD_1,
        PAD_2,
        PAD_3,
        PAD_4
    }
    
}
