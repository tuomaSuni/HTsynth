using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetModulator : MonoBehaviour
{
    [HideInInspector] public enum Envelopes
    {
        None,
        ADR,
        ADSR
    }

    [Header("Select Envelope:")]
    [Tooltip("Select an envelope to be used in audio generation.")] public Envelopes envelope;

    [HideInInspector] public enum Modes
    {
        Infinite,
        Timed
    }

    [HideInInspector] public System.Type type = null;

    [Tooltip("Note plays throughout the time it is being active.")] public Modes sustainMode;
    
    private void Awake()
    {
        switch (envelope)
        {
            case Envelopes.None:
                break;
            case Envelopes.ADR:
                type = typeof(ADR);
                break;
            case Envelopes.ADSR:
                type = typeof(ADSR);
                break;
            default:
                break;
        }

        foreach (Transform child in transform)
        {
            child.gameObject.AddComponent(type);
        }

        // Add the component to each child if the type is not null
        if (type != null)
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.GetComponent(type) == null) // Avoid adding the same component multiple times
                {
                    child.gameObject.AddComponent(type);
                }
            }
        }
    }
}
