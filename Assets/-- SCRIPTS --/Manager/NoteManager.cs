using System;
using System.Collections;
using System.Collections.Generic;
using MidiJack;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    
}

public static class CustomMidi
{

    public static bool GetKeyDown(MidiKey midiKey)
    {
        switch (midiKey)
        {
            case MidiKey.NOTE_KEY:
                        //WHITE TOUCHES
                return MidiMaster.GetKeyDown(0) ||
                       MidiMaster.GetKeyDown(2) ||
                       MidiMaster.GetKeyDown(4) ||
                       MidiMaster.GetKeyDown(5) ||
                       MidiMaster.GetKeyDown(7) ||
                       MidiMaster.GetKeyDown(9) ||
                       MidiMaster.GetKeyDown(11) ||
                       MidiMaster.GetKeyDown(12) ||
                       MidiMaster.GetKeyDown(14) ||
                       MidiMaster.GetKeyDown(16) ||
                       MidiMaster.GetKeyDown(17) ||
                       MidiMaster.GetKeyDown(19) ||
                       MidiMaster.GetKeyDown(21) ||
                       MidiMaster.GetKeyDown(23) ||
                       MidiMaster.GetKeyDown(24) ||
                       //BLACK TOUCHES
                       MidiMaster.GetKeyDown(1) ||
                       MidiMaster.GetKeyDown(3) ||
                       MidiMaster.GetKeyDown(6) ||
                       MidiMaster.GetKeyDown(8) ||
                       MidiMaster.GetKeyDown(10) ||
                       MidiMaster.GetKeyDown(13) ||
                       MidiMaster.GetKeyDown(15) ||
                       MidiMaster.GetKeyDown(18) ||
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
    
    public static float GetKey(int noteId)
    {
        return MidiMaster.GetKey(noteId);
    }

    public enum MidiKey
    {
        NOTE_KEY,
        PAD_1,
        PAD_2,
        PAD_3,
        PAD_4
    }
    
}
