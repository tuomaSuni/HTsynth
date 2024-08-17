using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetControls : MonoBehaviour
{
    [SerializeField] private bool debugMode = false;
    // Start is called before the first frame update
    [SerializeField] private GameObject System;
    [SerializeField] private GameObject Controls;
    void Awake()
    {
        if (debugMode == true)
        {
            System.SetActive(true);
        }
        if (debugMode == false)
        {
            Controls.SetActive(true);
        }
    }
    
}
