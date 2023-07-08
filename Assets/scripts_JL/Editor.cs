using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AI_Duck))]
public class editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AI_Duck lg = (AI_Duck)target;
        if (GUILayout.Button("Regenerate"))
        {
            lg.Start();
        }
    }
}
