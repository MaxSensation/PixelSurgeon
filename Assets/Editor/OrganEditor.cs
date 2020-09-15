using Human;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Organ))]
    public class OrganEditor : UnityEditor.Editor 
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var organ = (Organ)target;
            if (GUILayout.Button("Set Position")){
                organ.SetCorrectPosition();
                EditorUtility.SetDirty(organ);
            }
        }
    }
}