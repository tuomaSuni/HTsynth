using UnityEngine;

public class LimitFPS : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
