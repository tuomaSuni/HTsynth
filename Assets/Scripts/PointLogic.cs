using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLogic : MonoBehaviour
{
    // HashSet to store key values. Checking if an element exists in a HashSet is typically an O(1) operation,
    // meaning it is very fast regardless of the number of elements in the collection.
    // In a HashSet, elements are not stored in any specific order, so you cannot directly get the index of an element in the HashSet
    // as you would in an array or a list.
    private float averageSpeed;
    private Vector3 previousLocation = new Vector3(0, 0, 0);
    private Queue<float> speedQueue;
    private int averageFrames = 4; // Number of frames to average the speed over
    private HashSet<GameObject> triggeredCircles = new HashSet<GameObject>();
    public GameObject[] circles;
    private float sphereRadius = 0.5f;
    private float circleWorldRadius = 0.315f;
    private void Start()
    {
        speedQueue = new Queue<float>(averageFrames);
    }

    private void Update()
    {
        DetectOcclusion();
        DetectSpeed();
    }


    private void DetectOcclusion()
    {
        Vector3 sphereScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 sphereEdgeWorldPos = transform.position + transform.forward * sphereRadius;
        Vector3 sphereEdgeScreenPos = Camera.main.WorldToScreenPoint(sphereEdgeWorldPos);
        float projectedSphereRadius = Vector3.Distance(sphereScreenPos, sphereEdgeScreenPos);

        foreach (var circle in circles)
        {
            
            Vector3 circleScreenPos = Camera.main.WorldToScreenPoint(circle.transform.position);
            
            Vector3 circleEdgeWorldPos = circle.transform.position + Vector3.right * circleWorldRadius;
            Vector3 circleEdgeScreenPos = Camera.main.WorldToScreenPoint(circleEdgeWorldPos);
            float projectedCircleRadius = Vector3.Distance(circleScreenPos, circleEdgeScreenPos);
            
            float distance = Vector2.Distance(new Vector2(sphereScreenPos.x, sphereScreenPos.y), new Vector2(circleScreenPos.x, circleScreenPos.y));
            bool isOverlapping = distance < (projectedSphereRadius + projectedCircleRadius);

            if (isOverlapping)
            {
                if (triggeredCircles.Add(circle))
                {
                    ADSR adsr = circle.GetComponent<ADSR>();
                    adsr.PlayNote(averageSpeed);
                    Debug.Log(averageSpeed);
                    circle.GetComponent<KeyLogic>().SetAlpha(0.5f);
                }
            }
            else
            {
                if (triggeredCircles.Remove(circle))
                {
                    circle.GetComponent<KeyLogic>().SetAlpha(0.7f);
                }
            }
        }
    }

    private float DetectSpeed()
    {
        // Calculate displacement
        Vector3 displacement = transform.position - previousLocation;
        float currentSpeed = displacement.magnitude / Time.deltaTime;

        // Update previousLocation after calculating speed
        previousLocation = transform.position;

        // Add current speed to the queue
        speedQueue.Enqueue(currentSpeed);

        // Remove the oldest speed from the queue if we've reached the limit
        if (speedQueue.Count > averageFrames)
        {
            speedQueue.Dequeue();
        }

        // Calculate the average speed
        averageSpeed = 0f;

        foreach (float spd in speedQueue)
        {
            averageSpeed += spd;
        }
        averageSpeed /= speedQueue.Count;

        averageSpeed = Mathf.Clamp01(averageSpeed);
        return averageSpeed;
    }
}
