using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADSR : ADR
{
    private KeyLogic keylogic;
    private bool isActive;

    protected override void Start()
    {
        base.Start();

        InitializeKey(); 
    }

    private void InitializeKey()
    {
        keylogic = GetComponent<KeyLogic>();
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
        if (parameters.sustainMode == Parameters.Modes.Infinite)
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