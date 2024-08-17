using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    // HashSet to store key values. Checking if an element exists in a HashSet is typically an O(1) operation,
    // meaning it is very fast regardless of the number of elements in the collection.
    // In a HashSet, elements are not stored in any specific order, so you cannot directly get the index of an element in the HashSet
    // as you would in an array or a list.
    // private HashSet<char> keys = new HashSet<char> { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p' };
    private char[] keys = { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p' };
    [SerializeField] private GameObject instrument;
    private ADSR adsr;
    private KeyLogic keylogic;
    void Update()
    {
        // Iterate over the array to find the pressed key and get its index
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
