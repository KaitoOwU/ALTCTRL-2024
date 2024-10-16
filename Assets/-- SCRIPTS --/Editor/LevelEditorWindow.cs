using System;
using UnityEditor;
using UnityEngine;

[EditorWindowTitle(title = "Scenario Manager", icon = "Assets/-- SCRIPTS --/Editor/levelEditorLogo.png")]
public class LevelEditorWindow : EditorWindow
{
    private AudioClip _audioClip;
    [Min(1)] private uint _bpm;

    [MenuItem("Window/Level Editor")]
    public static void Init()
    {
        LevelEditorWindow window = GetWindowWithRect<LevelEditorWindow>(new Rect(0, 0, 800, 600), false, "Level Editor");
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
                GUILayout.Label("Level Editor", new GUIStyle(GUI.skin.label) {alignment = TextAnchor.MiddleCenter, fontSize = 30, fontStyle = FontStyle.Bold, fixedHeight = 64});
                GUILayout.FlexibleSpace();
            }
            EditorGUILayout.EndHorizontal();
        
            EditorGUILayout.Space(30);
            _audioClip = EditorGUILayout.ObjectField("Music", _audioClip, typeof(AudioClip), false) as AudioClip;
            _bpm = (uint) EditorGUILayout.IntField("BPM", (int) _bpm);

            if (_audioClip)
            {
                GUILayout.Space(10);
                
                float duration = _audioClip.length;
                GUILayout.Label("Duration : " + TimeSpan.FromSeconds(duration).ToString(@"mm\:ss"));
                GUILayout.Label("BPM : " + _bpm + " bpm");
                GUILayout.Space(10);

                int beats = (int) (_bpm / 60f * duration);
                GUILayout.Label("Beats : " + beats + " beat(s)");

                GUILayout.Space(20);
                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    GUI.backgroundColor = Color.green;
                    if (GUILayout.Button("Create Soundtracker", new GUIStyle(GUI.skin.button) {alignment = TextAnchor.MiddleCenter, fixedWidth = 250, fixedHeight = 50, fontSize = 20, fontStyle = FontStyle.Bold}))
                    {
                    
                    }
                    GUILayout.FlexibleSpace();
                }
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.EndVertical();
    }
}
