using System;
using System.Linq;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using UnityEditor;
using UnityEngine;

[EditorWindowTitle(title = "Soundtracker Creator", icon = "Assets/-- SCRIPTS --/Editor/levelEditorLogo.png")]
public class LevelEditorWindow : EditorWindow
{
    private DefaultAsset _audioClip;

    [MenuItem("Assets/Create/Soundtracker", false, 1)]
    public static void Init()
    {
        LevelEditorWindow window = GetWindowWithRect<LevelEditorWindow>(new Rect(0, 0, 400, 400), false);
        window.Show();
    }
    
    [MenuItem("Tools/Soundtracker Creator")]
    public static void Init2()
    {
        LevelEditorWindow window = GetWindowWithRect<LevelEditorWindow>(new Rect(0, 0, 400, 400), false);
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
                GUILayout.Label(AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/-- SCRIPTS --/Editor/levelEditorLogo.png"), new GUIStyle(GUI.skin.label) {fixedWidth = 64, fixedHeight = 64, alignment = TextAnchor.MiddleCenter});
                EditorGUILayout.Space(20);
                GUILayout.Label("Level Creator", new GUIStyle(GUI.skin.label) {alignment = TextAnchor.MiddleCenter, fontSize = 30, fontStyle = FontStyle.Bold, fixedHeight = 64});
                GUILayout.FlexibleSpace();
            }
            EditorGUILayout.EndHorizontal();
        
            EditorGUILayout.Space(30);
            _audioClip = EditorGUILayout.ObjectField(".MID File", _audioClip, typeof(DefaultAsset), false) as DefaultAsset;
            if (_audioClip)
            {
                if (AssetDatabase.GetAssetPath(_audioClip).EndsWith(".mid") == false)
                {
                    _audioClip = null;
                }
            }
            
            if (_audioClip)
            {
                MidiFile midi = MidiFile.Read(AssetDatabase.GetAssetPath(_audioClip));
                var time = midi.GetDuration<MetricTimeSpan>();
                
                GUILayout.Space(10);
                GUILayout.Label("Duration : " + time.Minutes + ":" + time.Seconds);
                GUILayout.Space(10);
                
                GUILayout.Label("Layers detected : " + midi.GetChannels().Count());
                string channels = string.Empty;
                foreach(var channel in midi.GetChannels().OrderBy(x => x))
                {
                    channels += channel + "   ";
                }
                GUILayout.Label(channels);
                
                GUILayout.FlexibleSpace();
                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    GUI.backgroundColor = Color.green;
                    if (GUILayout.Button("Create Soundtracker", new GUIStyle(GUI.skin.button) {alignment = TextAnchor.MiddleCenter, fixedWidth = 250, fixedHeight = 50, fontSize = 20, fontStyle = FontStyle.Bold}))
                    {
                        var soundTracker = ScriptableObject.CreateInstance<Soundtracker>();
                        soundTracker.path = AssetDatabase.GetAssetPath(_audioClip);
                        AssetDatabase.CreateAsset(soundTracker, "Assets/Resources/Soundtrackers/" + _audioClip.name + ".asset");

                        soundTracker.Setup();

                        Selection.activeObject = soundTracker;
                        EditorGUIUtility.PingObject(soundTracker);
                        
                        SoundtrackerEditor.Init(soundTracker);
                        EditorUtility.SetDirty(soundTracker);
                        this.Close();
                    }
                    GUILayout.FlexibleSpace();
                }
                EditorGUILayout.EndHorizontal();
                GUILayout.FlexibleSpace();
            }
        }
        EditorGUILayout.EndVertical();
    }
}
