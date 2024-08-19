using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class OnEditorQuit
{
    static OnEditorQuit()
    {
        EditorApplication.playModeStateChanged += ModeChanged;
    }

    static void ModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            ProcessManager.Quit();
        }
    }
}