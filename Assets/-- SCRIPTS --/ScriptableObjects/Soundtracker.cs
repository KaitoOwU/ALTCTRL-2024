using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;
using Unity.Collections;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class Soundtracker : ScriptableObject
{

    [ReadOnly] public string path;
    public AudioClip audioClip;
    public List<CurveHolder> curves = new List<CurveHolder>();
    
    public MidiFile midiFile { get; private set; }

    public void Setup()
    {
        midiFile = MidiFile.Read(path);
    }
    
    #if UNITY_EDITOR
    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        var obj = EditorUtility.InstanceIDToObject(instanceID);
        if (obj is not Soundtracker asset)
            return false;
        
        
        var window = EditorWindow.GetWindow<SoundtrackerEditor>();
        asset.Setup();
        window.linkedSoundTracker = asset;
        window.Show();
        return true;
    }
    #endif
    
}

[Serializable]
public class CurveHolder
{
    public string layerName;
    public int channel;
    public float toleranceTreshold;
    public AnimationCurve curve = new();
}
