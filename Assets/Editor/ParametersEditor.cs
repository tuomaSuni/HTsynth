using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SetModulator))]
public class SetModulatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SetModulator setmodulator = (SetModulator)target;

        // Draw the 'envelope' field
        setmodulator.envelope = (SetModulator.Envelopes)EditorGUILayout.EnumPopup("Select Envelope", setmodulator.envelope);

        // Conditionally make 'sustainMode' read-only
        bool isSustainModeEditable = setmodulator.envelope == SetModulator.Envelopes.ADSR;

        GUI.enabled = isSustainModeEditable;
        setmodulator.sustainMode = (SetModulator.Modes)EditorGUILayout.EnumPopup("Sustain Mode", setmodulator.sustainMode);
        GUI.enabled = true;  // Reset GUI.enabled to true for other fields
        
        // Apply changes to the serialized object
        serializedObject.ApplyModifiedProperties();
    }
}