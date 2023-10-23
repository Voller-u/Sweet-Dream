using UnityEditor;
using UnityEngine;

namespace JohnStairs.RCC.Character.Motor {
    [CustomEditor(typeof(RPGController))]
    public class RPGControllerEditor : Editor {
        protected SerializedProperty Script;
        protected SerializedProperty ActivateControl;
        protected SerializedProperty UseLegacyInputSystem;
        protected SerializedProperty LogInputWarnings;
        protected SerializedProperty NormalizeInputValues;

        public virtual void OnEnable() {
            Script = serializedObject.FindProperty("m_Script");
            ActivateControl = serializedObject.FindProperty("ActivateControl");
            UseLegacyInputSystem = serializedObject.FindProperty("UseLegacyInputSystem");
            LogInputWarnings = serializedObject.FindProperty("LogInputWarnings");
            NormalizeInputValues = serializedObject.FindProperty("NormalizeInputValues");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            RPGMotor rpgMotor = ((RPGController)serializedObject.targetObject)?.GetComponent<RPGMotor>();
            
            GUI.enabled = false;
            EditorGUILayout.PropertyField(Script);
            GUI.enabled = true;

            if (rpgMotor == null) {
                EditorGUILayout.LabelField("No RPGMotor component found!");
                EditorGUILayout.LabelField("Please assign one to use this RPGController");
                return;
            }

            EditorGUILayout.PropertyField(ActivateControl);
            if (!ActivateControl.boolValue) {
                EditorGUILayout.LabelField("└> Every player input is ignored");
            }
            EditorGUILayout.PropertyField(UseLegacyInputSystem);
            if (UseLegacyInputSystem.boolValue) {
                EditorGUILayout.PropertyField(LogInputWarnings, new GUIContent("└ Log Input Warnings "));
            }
            EditorGUILayout.PropertyField(NormalizeInputValues);

            serializedObject.ApplyModifiedProperties();
        }
    }
}