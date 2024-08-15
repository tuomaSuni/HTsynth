using UnityEngine;
using System.Diagnostics;
using System.ComponentModel;

public class Initializer : MonoBehaviour
{
    private static Process p;

    void Start()
    {
        InitializeProcess();
    }

    private static void InitializeProcess()
    {
        if (p == null)
        {
            p = new Process(); // Initialize the static Process p
            p.StartInfo.FileName = System.IO.Path.Combine(Application.dataPath, "External/Main.exe");
            p.Start();
        }
    }

    public static void Quit()
    {
        UnityEngine.Debug.Log("exiting");

        if (p != null && !p.HasExited)
        {
            try
            {
                // Use taskkill to terminate the process tree
                Process taskkill = new Process();
                taskkill.StartInfo.FileName = "taskkill";
                taskkill.StartInfo.Arguments = $"/PID {p.Id} /T /F";
                taskkill.StartInfo.CreateNoWindow = true;
                taskkill.StartInfo.UseShellExecute = false;
                taskkill.StartInfo.RedirectStandardOutput = true;
                taskkill.StartInfo.RedirectStandardError = true;
                taskkill.Start();
                taskkill.WaitForExit();

                string output = taskkill.StandardOutput.ReadToEnd();
                string error = taskkill.StandardError.ReadToEnd();

                UnityEngine.Debug.Log("taskkill output: " + output);
                UnityEngine.Debug.Log("taskkill error: " + error);
            }
            catch (Win32Exception e)
            {
                UnityEngine.Debug.LogError("Failed to execute taskkill: " + e.Message);
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogError("An unexpected error occurred: " + e.Message);
            }
        }
        else
        {
            UnityEngine.Debug.LogWarning("Process is not running.");
        }
    }
}