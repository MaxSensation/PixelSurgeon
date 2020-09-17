using Human;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(OrganManager))]
    public class OrganManagerEditor : UnityEditor.Editor 
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var organManager = (OrganManager)target;
            if (GUILayout.Button("Test Score")){
                organManager.GetOrganScore();
            }
        }
    }
}