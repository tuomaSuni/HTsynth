using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    // HashSet to store key values. Checking if an element exists in a HashSet is typically an O(1) operation,
    // meaning it is very fast regardless of the number of elements in the collection.
    // In a HashSet, elements are not stored in any specific order, so you cannot directly get the index of an element in the HashSet
    // as you would in an array or a list.
    private HashSet<char> keys = new HashSet<char> { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p' };
    
    void Update()
    {
        int index = 0; // iterates over the HashSet and manually tracks the index during iteration.

        foreach (char key in keys)
        {
            if (Input.GetKeyDown(key.ToString()))
            {
                Debug.Log(index);
            }
        }
    }
}
