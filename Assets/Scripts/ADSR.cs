using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADSR : ADR
{
    private KeyLogic keylogic;
    private SetModulator setModulator;
    private bool isActive;

    protected override void Start()
    {
        base.Start();

        InitializeKey();
        InitializeModulator();
    }

    private void InitializeKey()
    {
        keylogic = GetComponent<KeyLogic>();

        if (keylogic == null)
        {
            Debug.LogError("KeyLogic component is missing.");
        }
    }

    private void InitializeModulator()
    {
        setModulator = transform.parent.gameObject.GetComponent<SetModulator>();

        if (setModulator == null)
        {
            Debug.LogError("SetModulator component is missing.");
        }
    }

    protected override IEnumerator Decay()
    {
        while (amplitude > (parameters.attackTime - parameters.decayTime))
        {
            amplitude -= Time.deltaTime * parameters.decaySpeed * force;
            
            yield return null;
        }

        currentEnvelopeCoroutine = StartCoroutine(Sustain());
    }

    private IEnumerator Sustain()
    {
        if (setModulator.sustainMode == SetModulator.Modes.Infinite)
        {
            while (keylogic.isActive == true)
            {
                yield return null;
            }
        }
        else
        {
            yield return new WaitForSeconds(parameters.sustainTime);
        }

        currentEnvelopeCoroutine = StartCoroutine(Release());
    }
}