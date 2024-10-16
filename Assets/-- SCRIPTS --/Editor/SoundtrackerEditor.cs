using System;
using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;
using UnityEditor;
using UnityEngine;

[EditorWindowTitle(title = "Soundtracker", icon = "Assets/-- SCRIPTS --/Editor/soundtracker.png")]
public class SoundtrackerEditor : EditorWindow
{
    public int bpm;
    public MidiFile midiFile;
    private List<CurveHolder> _curves = new();
    
    public static void Init(MidiFile file, int bpm)
    {
        var window = GetWindowWithRect<SoundtrackerEditor>(new Rect(0, 0, 500, 600), false);
        window.midiFile = file;
        window.bpm = bpm;
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
                if (_curves.Count == 0)
                {
                    GUI.color = Color.red;
                    GUILayout.Label("No layer to display", new GUIStyle(GUI.skin.label) {alignment = TextAnchor.MiddleCenter, fontSize = 20});
                    GUI.color = Color.white;
                }
                else
                {
                    for (int i = 0; i < _curves.Count; i++)
                    {
                        GUILayout.Space(10);
                        
                        EditorGUILayout.BeginHorizontal();
                        {
                            GUILayout.Label("Layer n°" + (i+1), new GUIStyle(GUI.skin.label) {fontSize = 20, fontStyle = FontStyle.Bold});
                            GUI.backgroundColor = Color.red;
                            if (GUILayout.Button("x", new GUIStyle(GUI.skin.button) {fixedWidth = 20, fixedHeight = 20, fontSize = 15, alignment = TextAnchor.MiddleCenter}))
                            {
                                _curves.RemoveAt(i);
                                return;
                            }
                            GUI.backgroundColor = Color.white;
                        }
                        EditorGUILayout.EndHorizontal();
                        var holder = _curves[i];
                        
                        EditorGUILayout.BeginHorizontal();
                        {
                            holder.layerName = EditorGUILayout.TextField(holder.layerName);
                            holder.channel = Math.Clamp(EditorGUILayout.IntField(holder.channel), 1, int.MaxValue);
                        }
                        EditorGUILayout.EndHorizontal();
                        holder.curve = EditorGUILayout.CurveField("Timings", holder.curve);
                        
                        GUILayout.Space(10);
                    }
                }
            }
            EditorGUILayout.EndVertical();
            
            GUILayout.Space(20);
            
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                GUI.backgroundColor = Color.green;
                if (GUILayout.Button("Add Layer", new GUIStyle(GUI.skin.button) {alignment = TextAnchor.MiddleCenter, fixedHeight = 50, fontSize = 20, fontStyle = FontStyle.Bold}))
                {
                    _curves.Add(new());
                }
                GUILayout.Space(40);
                GUI.backgroundColor = Color.magenta;
                if (GUILayout.Button("Auto Complete curves", new GUIStyle(GUI.skin.button) {alignment = TextAnchor.MiddleCenter, fixedHeight = 50, fontSize = 20, fontStyle = FontStyle.Bold}))
                {
                        
                }
                GUI.backgroundColor = Color.white;
                GUILayout.FlexibleSpace();
            }
            EditorGUILayout.EndHorizontal();

        }
        EditorGUILayout.EndVertical();
    }
}

[Serializable]
public struct CurveHolder
{
    public string layerName;
    public int channel;
    public AnimationCurve curve;
}
