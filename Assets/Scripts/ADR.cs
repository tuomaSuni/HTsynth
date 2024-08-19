using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADR : Envelope
{
    protected Parameters parameters;
    protected float force;
    protected Coroutine currentEnvelopeCoroutine;

    protected virtual void Start()
    {
        InitializeParameters();
    }

    private void InitializeParameters()
    {
        parameters = transform.parent.gameObject.GetComponent<Parameters>();
    }

    public void PlayNote(float InitialForce)
    {
        force = InitialForce;

        // Stop any currently running envelope coroutine
        if (currentEnvelopeCoroutine != null)
        {
            StopCoroutine(currentEnvelopeCoroutine);
        }
        // Start the Attack coroutine and keep a reference to it
        currentEnvelopeCoroutine = StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        while (amplitude < parameters.attackTime * force)
        {
            amplitude += Time.deltaTime * parameters.attackSpeed;
            yield return null;
        }
        
        currentEnvelopeCoroutine = StartCoroutine(Decay());
    }

    protected virtual IEnumerator Decay()
    {
        while (amplitude > (parameters.attackTime - parameters.decayTime))
        {
            amplitude -= Time.deltaTime * parameters.decaySpeed * force;
            
            yield return null;
        }

        currentEnvelopeCoroutine = StartCoroutine(Release());
    }

    protected IEnumerator Release()
    {
        while (amplitude > 0.0f)
        {
            amplitude -= Time.deltaTime * parameters.releaseTime * force;
            yield return null;
        }
    }
}
