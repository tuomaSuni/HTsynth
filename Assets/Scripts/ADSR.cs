using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADSR : Envelope
{
    private Parameters parameters;
    private float force;

    private void Start()
    {
        InitializeParameters();    
    }

    private void InitializeParameters()
    {
        parameters = transform.parent.gameObject.GetComponent<Parameters>();
    }
    
    public IEnumerator Attack(float InitialForce)
    {
        force = InitialForce;

        while (amplitude < parameters.attackTime * force)
        {
            amplitude += Time.deltaTime * parameters.attackSpeed;
            yield return null;
        }

        StartCoroutine(Decay(force));
    }

    private IEnumerator Decay(float force)
    {
        while (amplitude > (parameters.attackTime - parameters.decayTime))
        {
            amplitude -= Time.deltaTime * parameters.decaySpeed * force;
            yield return null;
        }

        StartCoroutine(Sustain(force));
    }

    private IEnumerator Sustain(float force)
    {
        yield return new WaitForSeconds(parameters.sustainTime);
        
        StartCoroutine(Release(force));
    }

    private IEnumerator Release(float force)
    {
        while (amplitude > 0.0f)
        {
            amplitude -= Time.deltaTime * parameters.releaseTime * force;
            yield return null;
        }
    }
}