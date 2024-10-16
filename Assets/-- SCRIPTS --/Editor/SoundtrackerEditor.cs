using System;
using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;
using UnityEditor;
using UnityEngine;

[EditorWindowTitle(title = "Soundtracker", icon = "Assets/-- SCRIPTS --/Editor/soundtracker.png")]
public class SoundtrackerEditor : EditorWindow
{
    private MidiFile _midiFile;
    private CurveHolder[] _curves;
    
    public static void Init(MidiFile file)
    {
        var window = GetWindowWithRect<SoundtrackerEditor>(new Rect(0, 0, 500, 600), false);
        window.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical(new GUIStyle(){margin = new RectOffset(50, 50, 20, 20)});
        {
            EditorGUILayout.Space(10);
        
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                GUILayout.Label(AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/-- SCRIPTS --/Editor/soundtracker.png"), new GUIStyle(GUI.skin.label) {fixedWidth = 64, fixedHeight = 64, alignment = TextAnchor.MiddleCenter});
                EditorGUILayout.Space(20);
                GUILayout.Label("Soundtracker", new GUIStyle(GUI.skin.label) {alignment = TextAnchor.MiddleCenter, fontSize = 30, fontStyle = FontStyle.Bold, fixedHeight = 64});
                GUILayout.FlexibleSpace();
            }
            EditorGUILayout.EndHorizontal();
            
            GUILayout.Space(20);
            
            EditorGUILayout.BeginVertical(new GUIStyle(GUI.skin.box));
            {
                GUILayout.Label("Test");
            }
            EditorGUILayout.EndVertical();

        }
        EditorGUILayout.EndVertical();
    }
}

[Serializable]
public struct CurveHolder
{
    public AnimationCurve curve;
}
