using UnityEngine;

public class TargetFPS : MonoBehaviour
{
    [SerializeField] private float targetFPS = 120.0f;

    private void Start()
    {
        OVRPlugin.systemDisplayFrequency = targetFPS;
    }
}
