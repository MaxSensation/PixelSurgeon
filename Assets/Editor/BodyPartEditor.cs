using UnityEditor;
using UnityEngine;
using BodyPart = Human.BodyPart;

namespace Editor
{
    [CustomEditor(typeof(BodyPart))]
    public class BodyPartEditor : UnityEditor.Editor 
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var organ = (BodyPart)target;
            if (!GUILayout.Button("Set Position")) return;
            organ.SetCorrectPosition();
            EditorUtility.SetDirty(organ);
        }
    }
}