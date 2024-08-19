using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTracker : MonoBehaviour
{
    public UDPReceiver udpReceiver;
    public GameObject[] _Point;    // Array to hold the GameObject elements

    void Update()
    {
        ProcessData();
    }

    private void ProcessData()
    {
        string data = udpReceiver.data;

        if (!string.IsNullOrEmpty(data) && data.Length > 2)
        {
            // Remove the square brackets at the beginning and end
            data = data.Trim('[', ']');

            // Split the data into tuples
            string[] tupleStrings = data.Split(new string[] { "), (" }, StringSplitOptions.RemoveEmptyEntries);

            // Process the tuples
            for (int i = 0; i < tupleStrings.Length && i < _Point.Length; i++)
            {
                // Split each tuple into individual coordinates
                string tuple = tupleStrings[i].Trim('(', ')');
                string[] coordinates = tuple.Split(',');

                if (coordinates.Length == 3)
                {
                    // Parse the x, y, z coordinates and scale as needed
                    float x = 7 - float.Parse(coordinates[0]) / 100f;
                    float y =      float.Parse(coordinates[1]) / 100f  - 3;
                    float z =      float.Parse(coordinates[2]) / 100f;

                    Vector3 xyz = new Vector3(x, y, z);

                    // Update the GameObject position
                    if (_Point[i] != null)
                    {
                        _Point[i].transform.position = xyz;
                    }
                }
            }
        }
    }
}