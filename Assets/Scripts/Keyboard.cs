using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    private char[] keys = { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p' };
    [SerializeField] private GameObject instrument;
    private ADSR adsr;
    private KeyLogic keylogic;
    void Update()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKeyDown(keys[i].ToString()))
            {
                adsr = instrument.transform.GetChild(i).GetComponent<ADSR>();
                StartCoroutine(adsr.Attack());
                keylogic = instrument.transform.GetChild(i).GetComponent<KeyLogic>();
                keylogic.SetAlpha(0.5f);
            }
            if (Input.GetKeyUp(keys[i].ToString()))
            {
                keylogic = instrument.transform.GetChild(i).GetComponent<KeyLogic>();
                keylogic.SetAlpha(0.7f);
            }
        }
    }
}
