using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLogic : MonoBehaviour
{
    // HashSet to store key values. Checking if an element exists in a HashSet is typically an O(1) operation,
    // meaning it is very fast regardless of the number of elements in the collection.
    // In a HashSet, elements are not stored in any specific order, so you cannot directly get the index of an element in the HashSet
    // as you would in an array or a list.
    
    private HashSet<GameObject> triggeredCircles = new HashSet<GameObject>();

    private Vector3 previousLocation;
    private float force;
    public GameObject[] circles;
    private float sphereRadius = 0.5f;
    private float circleWorldRadius = 0.315f;
    void Update()
    {
        DetectOcclusion();
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
                    circle.GetComponent<KeyLogic>().OnEnter();
                }
            }
            else
            {
                if (triggeredCircles.Remove(circle))
                {
                    circle.GetComponent<KeyLogic>().OnExit();
                }
            }
        }
    }
    public IEnumerator CalculateForce()
    {
        previousLocation = transform.position;

        yield return new WaitForSeconds(0.02f);

        force = (transform.position - previousLocation).magnitude / Time.deltaTime;
    }
}
