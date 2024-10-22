using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[EditorWindowTitle(title = "Soundtracker", icon = "Assets/-- SCRIPTS --/Editor/soundtracker.png")]
public class SoundtrackerEditor : EditorWindow
{
    public Soundtracker linkedSoundTracker;
    
    public static void Init(Soundtracker soundTracker)
    {
        var window = GetWindowWithRect<SoundtrackerEditor>(new Rect(0, 0, 500, 600), false);
        window.linkedSoundTracker = soundTracker;
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
            
            
            GUILayout.Space(40);
            linkedSoundTracker.audioClip = (AudioClip) EditorGUILayout.ObjectField("Audio Clip", linkedSoundTracker.audioClip, typeof(AudioClip), false);
            GUILayout.Space(20);


            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Add Layer", new GUIStyle(GUI.skin.button) { fixedWidth = 85 }))
            {
                linkedSoundTracker.curves.Add(new CurveHolder());
            }

            GUI.backgroundColor = Color.white;
            
            EditorGUILayout.BeginVertical(new GUIStyle(GUI.skin.box));
            {
                if (linkedSoundTracker.curves.Count == 0)
                {
                    GUI.color = Color.red;
                    GUILayout.Label("No layer to display", new GUIStyle(GUI.skin.label) {alignment = TextAnchor.MiddleCenter, fontSize = 20});
                    GUI.color = Color.white;
                }
                else
                {
                    for (int i = 0; i < linkedSoundTracker.curves.Count; i++)
                    {
                        GUILayout.Space(10);
                        
                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                GUILayout.Label("Layer n°" + (i+1), new GUIStyle(GUI.skin.label) {fontSize = 20, fontStyle = FontStyle.Bold});
                                GUILayout.FlexibleSpace();
                                GUI.backgroundColor = Color.magenta;
                                if (GUILayout.Button("Generate Timings", new GUIStyle(GUI.skin.button) {fontSize = 15, alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold}))
                                {
                                    GenerateTimings(i, linkedSoundTracker.curves[i].channel);
                                }
                                GUI.backgroundColor = Color.white;
                            }
                            EditorGUILayout.EndHorizontal();
                            GUI.backgroundColor = Color.red;
                            if (GUILayout.Button("x", new GUIStyle(GUI.skin.button) {fixedWidth = 20, fixedHeight = 20, fontSize = 15, alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold}))
                            {
                                linkedSoundTracker.curves.RemoveAt(i);
                                return;
                            }
                            GUI.backgroundColor = Color.white;
                        }
                        EditorGUILayout.EndHorizontal();
                        var holder = linkedSoundTracker.curves[i];
                        
                        EditorGUILayout.BeginHorizontal();
                        {
                            holder.layerName = EditorGUILayout.TextField(holder.layerName);
                            holder.channel = Math.Clamp(EditorGUILayout.IntField(holder.channel), 0, int.MaxValue);
                        }
                        EditorGUILayout.EndHorizontal();
                        
                        holder.toleranceTreshold = EditorGUILayout.Slider("Tolerance Treshold (sec)", holder.toleranceTreshold, 0f, 0.5f);
                        holder.curve = EditorGUILayout.CurveField(holder.curve);
                    }
                }
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndVertical();
    }

    private void GenerateTimings(int layer, int channel)
    {
        var tempo = linkedSoundTracker.midiFile.GetTempoMap();
        var curve = linkedSoundTracker.curves[layer].curve;
        curve.ClearKeys();
        
        foreach (var trackChunk in linkedSoundTracker.midiFile.GetTrackChunks())
        {
            using var notesManager = trackChunk.ManageNotes();
            foreach (var note in notesManager.Objects)
            {
                if (note.Channel == channel)
                {
                    int i = int.Parse(linkedSoundTracker.midiFile.TimeDivision.ToString().Replace(" ticks/qnote", ""));
                    var timing = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time + i/3, tempo);

                    if ((float)timing.TotalSeconds - linkedSoundTracker.curves[layer].toleranceTreshold > 0)
                    {
                        curve.AddKey(new Keyframe((float)timing.TotalSeconds - linkedSoundTracker.curves[layer].toleranceTreshold, 0));
                    }
                    curve.AddKey(new Keyframe((float)timing.TotalSeconds, 1));
                    if ((float)timing.TotalSeconds - linkedSoundTracker.curves[layer].toleranceTreshold < linkedSoundTracker.midiFile.GetDuration<MetricTimeSpan>().TotalSeconds)
                    {
                        curve.AddKey(new Keyframe((float)timing.TotalSeconds + linkedSoundTracker.curves[layer].toleranceTreshold, 0));   
                    }
                }
            }
        }
    }
}
