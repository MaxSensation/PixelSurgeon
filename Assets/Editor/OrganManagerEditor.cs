using Human;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(BodyPartManager))]
    public class OrganManagerEditor : UnityEditor.Editor 
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var organManager = (BodyPartManager)target;
            if (GUILayout.Button("Test Score")){
                //organManager.GetOrganScore();
            }
        }
    }
}