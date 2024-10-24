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
            case MidiKey.PAD_DOWN:
                return MidiMaster.GetKeyDown(MidiChannel.Ch10, 36) ||
                       MidiMaster.GetKeyDown(MidiChannel.Ch10, 37) ||
                       MidiMaster.GetKeyDown(MidiChannel.Ch10, 38) ||
                       MidiMaster.GetKeyDown(MidiChannel.Ch10, 39);
            case MidiKey.PAD_UP:
                return MidiMaster.GetKeyDown(MidiChannel.Ch10, 40) ||
                       MidiMaster.GetKeyDown(MidiChannel.Ch10, 41) ||
                       MidiMaster.GetKeyDown(MidiChannel.Ch10, 42) ||
                       MidiMaster.GetKeyDown(MidiChannel.Ch10, 43);
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
        PAD_UP,
        PAD_DOWN
    }
    
}
