using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    private char[] keys = { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p' };
    [SerializeField] private GameObject instrument;
    private KeyLogic keylogic;
    private System.Type type;
    void Start()
    {
        type = instrument.GetComponent<SetModulator>().type;
    }

    void Update()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKeyDown(keys[i].ToString()))
            {
                Component modulator = instrument.transform.GetChild(i).GetComponent(type);
                modulator.GetType().GetMethod("PlayNote").Invoke(modulator, new object[] { 1.0f });

                keylogic = instrument.transform.GetChild(i).GetComponent<KeyLogic>();
                keylogic.SetActive(true, 1.0f);
            }
            if (Input.GetKeyUp(keys[i].ToString()))
            {
                keylogic = instrument.transform.GetChild(i).GetComponent<KeyLogic>();
                keylogic.SetActive(false, 0.7f);
            }
        }
    }
}
