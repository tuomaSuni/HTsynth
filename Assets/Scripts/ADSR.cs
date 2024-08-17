using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADSR : Envelope
{
    private Parameters parameters;
    private void Start()
    {
        InitializeParameters();    
    }

    private void InitializeParameters()
    {
        parameters = transform.parent.gameObject.GetComponent<Parameters>();
    }
    
    public IEnumerator Attack()
    {
        while (amplitude < parameters.attackTime)
        {
            amplitude += Time.deltaTime * parameters.attackSpeed;
            yield return null;
        }

        StartCoroutine(Decay());
    }

    private IEnumerator Decay()
    {
        while (amplitude > (parameters.attackTime - parameters.decayTime))
        {
            amplitude -= Time.deltaTime * parameters.decaySpeed;
            yield return null;
        }

        StartCoroutine(Sustain());
    }

    private IEnumerator Sustain()
    {
        yield return new WaitForSeconds(parameters.sustainTime);
        
        StartCoroutine(Release());
    }

    private IEnumerator Release()
    {
        while (amplitude > 0.0f)
        {
            amplitude -= parameters.releaseTime;
            yield return null;
        }
    }
}