using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class EditorTools
{
    static EditorTools()
    {
        EditorApplication.playModeStateChanged += ModeChanged;
    }

    static void ModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            Initializer.Quit();
        }
    }
}