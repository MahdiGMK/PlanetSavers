using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Enemy))]
public class EnemyInsp : Editor
{
    Enemy targ;
    private void OnEnable()
    {
        targ = (Enemy)target;
    }
    private void OnSceneGUI()
    {

        Handles.color = Color.blue;
        Handles.SphereHandleCap(1, targ.startPos, Quaternion.identity, 1, EventType.Repaint);
        targ.startPos = Handles.DoPositionHandle(targ.startPos, Quaternion.identity);
        Handles.color = Color.red;
        Handles.SphereHandleCap(1, targ.endPos, Quaternion.identity, 1, EventType.Repaint);
        targ.endPos = Handles.DoPositionHandle(targ.endPos, Quaternion.identity);

    }
    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Reset Start Pos"))
            targ.startPos = targ.transform.position + Vector3.left;
        if (GUILayout.Button("Reset End Pos"))
            targ.endPos = targ.transform.position + Vector3.right;
        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Snap Start Pos"))
        {
            targ.startPos = new Vector2(Mathf.Round(targ.startPos.x), Mathf.Round(targ.startPos.y));
        }
        if (GUILayout.Button("Snap End Pos"))
        {
            targ.endPos = new Vector2(Mathf.Round(targ.endPos.x), Mathf.Round(targ.endPos.y));
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(20);

        GUILayout.Label("Going");
        GUILayout.BeginHorizontal();
        targ.TimeToGo = EditorGUILayout.FloatField("Duration", targ.TimeToGo);
        targ.GoingFlow = EditorGUILayout.CurveField("Flow", targ.GoingFlow);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        targ.RestTime0 = EditorGUILayout.FloatField("Duration of first stop", targ.RestTime0);

        GUILayout.Space(10);

        GUILayout.Label("Comming");
        GUILayout.BeginHorizontal();
        targ.TimeToComeBack = EditorGUILayout.FloatField("Duration", targ.TimeToComeBack);
        targ.CommingFlow = EditorGUILayout.CurveField("Flow", targ.CommingFlow);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        targ.RestTime1 = EditorGUILayout.FloatField("Duration of secound stop", targ.RestTime1);
    }
}
