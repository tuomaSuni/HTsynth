using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADSR : Envelope
{
    // ADSR Time and Speed Variables
    private float attackTime = 1.0f;
    private float attackSpeed = 2.0f;
    private float decayTime = 0.5f;
    private float decaySpeed = 1.30f;
    private float releaseTime = 1.10f;
    private bool logic = true;
    
    public IEnumerator Attack()
    {
        while (amplitude < attackTime)
        {
            amplitude += Time.deltaTime * attackSpeed;
            yield return null;
        }

        StartCoroutine(Decay());
    }

    private IEnumerator Decay()
    {
        while (amplitude > (attackTime - decayTime))
        {
            amplitude -= Time.deltaTime * decaySpeed;
            yield return null;
        }

        StartCoroutine(Sustain());
    }

    private IEnumerator Sustain()
    {
        yield return null;
        StartCoroutine(Release());
    }

    private IEnumerator Release()
    {
        while (amplitude > 0.0f)
        {
            amplitude -= releaseTime;
            yield return null;
        }
    }
}