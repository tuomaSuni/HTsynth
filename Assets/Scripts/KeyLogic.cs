using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLogic : MonoBehaviour
{
    public bool isPlaying = false;
    private Vector3 previousLocation;
    private float speed;
    private ADSR adsr;
    private void Start()
    {
        adsr = GetComponent<ADSR>();
        previousLocation = transform.position;
    }

    private void InitializeADSR()
    {
        adsr = GetComponent<ADSR>();
        if (adsr == null)
        {
            gameObject.AddComponent<ADSR>();
            return;
        }
    }
    // This could be Optimized to not run on update.
    private float CalculateForce()
    {
        // Calculate distance moved since the last frame
        float distanceMoved = Vector3.Distance(transform.position, previousLocation);

        // Calculate speed (distance moved per second)
        speed = distanceMoved / Time.deltaTime;

        // Store the current position for the next frame
        previousLocation = transform.position;
        

        return speed;
    }

    private void Update()
    {
        CalculateForce();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        adsr.StartCoroutine(adsr.Attack());
        isPlaying = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isPlaying = false;
    }
}