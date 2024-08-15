using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTracker : MonoBehaviour
{

    LineRenderer lineRenderer;

    public Transform origin;
    public Transform destination;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.10f;
        lineRenderer.endWidth = 0.10f;
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetPosition(1, destination.position);
    }
}