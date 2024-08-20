using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLogic : MonoBehaviour
{
    // HashSet to store key values. Checking if an element exists in a HashSet is typically an O(1) operation,
    // meaning it is very fast regardless of the number of elements in the collection.
    // In a HashSet, elements are not stored in any specific order, so you cannot directly get the index of an element in the HashSet
    // as you would in an array or a list.
    private float speed;
    private Vector3 previousLocation = new Vector3(0, 0, 0);
    private Queue<float> speedQueue;
    private int frames = 10; // Number of frames to average the speed over
    private HashSet<GameObject> triggeredKeys = new HashSet<GameObject>();
    [SerializeField] private GameObject[] keys;
    private float sphereRadius = 0.5f;
    private float keyWorldRadius = 0.250f;
    private System.Type type;
    [SerializeField] private GameObject instrument;

    private void Start()
    {
        speedQueue = new Queue<float>(frames);
        type = instrument.GetComponent<SetModulator>().type;
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

        foreach (var key in keys)
        {
            
            Vector3 keyScreenPos = Camera.main.WorldToScreenPoint(key.transform.position);
            
            Vector3 keyEdgeWorldPos = key.transform.position + Vector3.right * keyWorldRadius;
            Vector3 keyEdgeScreenPos = Camera.main.WorldToScreenPoint(keyEdgeWorldPos);
            float projectedKeyRadius = Vector3.Distance(keyScreenPos, keyEdgeScreenPos);
            
            float distance = Vector2.Distance(new Vector2(sphereScreenPos.x, sphereScreenPos.y), new Vector2(keyScreenPos.x, keyScreenPos.y));
            bool isOverlapping = distance < (projectedSphereRadius + projectedKeyRadius);

            if (isOverlapping)
            {
                if (triggeredKeys.Add(key))
                {
                    KeyLogic keylogic = key.GetComponent<KeyLogic>();
                    Debug.Log(keylogic.isActive);
                    if (keylogic.isActive == false)
                    {
                        Debug.Log("here");
                        keylogic.SetActive(true, 1.0f);
                        
                        Component modulator = key.GetComponent(type);
                        modulator.GetType().GetMethod("PlayNote").Invoke(modulator, new object[] { speed });
                    }
                }
            }
            else
            {
                if (triggeredKeys.Remove(key))
                {
                    KeyLogic keylogic = key.GetComponent<KeyLogic>();
                    keylogic.SetActive(false, 0.7f);
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
        if (speedQueue.Count > frames)
        {
            speedQueue.Dequeue();
        }

        // Calculate the average speed
        speed = 0f;

        foreach (float spd in speedQueue)
        {
            speed += spd;
        }
        speed /= speedQueue.Count;

        speed = Mathf.Clamp01(speed / 20f);
        
        return speed;
    }
}
