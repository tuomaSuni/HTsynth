using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Parameters))]
public class ParametersEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Parameters parameters = (Parameters)target;

        // Draw the 'envelope' field
        parameters.envelope = (Parameters.Envelopes)EditorGUILayout.EnumPopup("Select Envelope", parameters.envelope);

        // Conditionally make 'sustainMode' read-only
        bool isSustainModeEditable = parameters.envelope == Parameters.Envelopes.ADSR;

        GUI.enabled = isSustainModeEditable;
        parameters.sustainMode = (Parameters.Modes)EditorGUILayout.EnumPopup("Sustain Mode", parameters.sustainMode);
        GUI.enabled = true;  // Reset GUI.enabled to true for other fields

        // Draw other fields
        parameters.attackTime = EditorGUILayout.Slider("Attack Time", parameters.attackTime, 0, 1);
        parameters.decayTime = EditorGUILayout.Slider("Decay Time", parameters.decayTime, 0, 1);
        parameters.sustainTime = EditorGUILayout.Slider("Sustain Time", parameters.sustainTime, 0, 1);
        parameters.releaseTime = EditorGUILayout.Slider("Release Time", parameters.releaseTime, 0, 1);

        // Apply changes to the serialized object
        serializedObject.ApplyModifiedProperties();
    }
}