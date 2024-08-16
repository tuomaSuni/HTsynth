using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLogic : MonoBehaviour
{

    private Vector3 previousLocation;
    private float force;

    public IEnumerator CalculateForce()
    {
        previousLocation = transform.position;

        yield return new WaitForSeconds(0.05f);

        force = (transform.position - previousLocation).magnitude / Time.deltaTime;

        Debug.Log(force);
    }
/*
    private void Update()
    {
        previousLocation = transform.position;

        force = (transform.position - previousLocation).magnitude / Time.deltaTime;

        Debug.Log(force);
    }
    */
}
