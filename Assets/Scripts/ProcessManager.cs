using UnityEngine;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;

public class ProcessManager : MonoBehaviour
{
    private static Process p;
    
    void Start()
    {
        InitializeProcess();
    }

    private void InitializeProcess()
    {
        if (p == null)
        {
            // Define the path to the .exe file
            string exePath = Path.Combine(Application.dataPath, "External/Main.exe");

            // Check if the .exe file exists
            if (File.Exists(exePath))
            {
                try
                {
                    p = new Process(); // Initialize the static Process p
                    p.StartInfo.FileName = exePath;
                    p.Start(); // Start the process
                }
                catch (System.Exception ex)
                {
                    // Handle any exceptions that might occur when starting the process
                    UnityEngine.Debug.Log("Failed to start the process: " + ex.Message);
                }
            }
            else
            {
                // Log an error if the .exe file does not exist
                UnityEngine.Debug.Log("Executable file not found at: " + exePath +
                ". Load file into the system or use your keyboard [Q:P] instead.");

            }
        }
    }

    public static void Quit()
    {
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
            UnityEngine.Debug.Log("Application terminated succesfully");
        }
    }
}