using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Source
{
    [CreateAssetMenu(fileName = "DataSample_SO", menuName = "Data/New Sample")]
    public class DataSampleSO : ScriptableObject
    {
        private Enums _enums;
        [HideInInspector][SerializeField] private List<AudioClass> audioDanger;
        [HideInInspector][SerializeField] private List<AudioClass> audioFriendly;
        [HideInInspector][SerializeField] private List<AudioClass> audioNeutral;
        private int _ID;
        private string _text;
        private float _volum;

        static bool list;
        static bool text;
    
        [CustomEditor(typeof(DataSampleSO))]
        public class DataSampleCustomEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                DataSampleSO dataSampleSo = (DataSampleSO)target;
                DrawButton(dataSampleSo);

                Draw(dataSampleSo);
            }

            private void Draw(DataSampleSO dataSampleSo)
            {
                EditorGUILayout.BeginHorizontal();
                
                EditorGUILayout.LabelField("ID");
                dataSampleSo._ID = EditorGUILayout.IntField(dataSampleSo._ID, GUILayout.MaxWidth(50));
                
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.LabelField("Volum");
                dataSampleSo._volum = EditorGUILayout.Slider(dataSampleSo._volum, 0f, 100f);
            }

            private void DrawList(DataSampleSO dataSampleSo)
            {
                EditorGUILayout.LabelField("Enum audio list", GUILayout.MaxWidth(150));
                dataSampleSo._enums = (Enums)EditorGUILayout.EnumPopup(dataSampleSo._enums);

                EditorGUILayout.LabelField("Audio list", GUILayout.MaxWidth(75));
                serializedObject.Update();

                if (dataSampleSo._enums == Enums.Friendly)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("audioFriendly"), true);
                }
                else if (dataSampleSo._enums == Enums.Danger)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("audioDanger"), true);
                }
                else if (dataSampleSo._enums == Enums.Neutral)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("audioNeutral"), true);
                }
                
                serializedObject.ApplyModifiedProperties();

            }

            private void DrawText(DataSampleSO dataSampleSo)
            {
                EditorGUILayout.LabelField("Text");
                dataSampleSo._text = EditorGUILayout.TextArea(dataSampleSo._text, GUILayout.MaxWidth(1000), GUILayout.Height(50));
            }

            private void DrawButton(DataSampleSO dataSampleSo)
            {
                EditorGUILayout.BeginHorizontal();
                
                if (GUILayout.Button("List"))
                {
                    list = true;
                    text = false;
                }

                if (GUILayout.Button("Text"))
                {
                    list = false;
                    text = true;
                }
            
                if (GUILayout.Button("Clear"))
                {
                    list = false;
                    text = false;
                }
            
                EditorGUILayout.EndHorizontal();
                
                if (list)
                {
                    DrawList(dataSampleSo);
                }
                else if (text)
                {
                    DrawText(dataSampleSo);
                }
            }
        }
    }
}
