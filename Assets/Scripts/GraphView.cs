using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (JumpGraph))]

public class GraphView : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        JumpGraph myJumpGraph = (JumpGraph)target;

        myJumpGraph.gravityForce = EditorGUILayout.Slider("Gravity", myJumpGraph.gravityForce, -9.8f, -39.2f);
        myJumpGraph.upwardForce = EditorGUILayout.Slider("Jump Force", myJumpGraph.upwardForce, 5, 100);
        myJumpGraph.timerLimit = EditorGUILayout.Slider("Jump Limit", myJumpGraph.timerLimit, 5, 60);
        myJumpGraph.isAirborne = EditorGUILayout.Toggle("Is Airborne", myJumpGraph.isAirborne);
        myJumpGraph.isGrounded = EditorGUILayout.Toggle("Is Grounded", myJumpGraph.isGrounded);
    }
}
