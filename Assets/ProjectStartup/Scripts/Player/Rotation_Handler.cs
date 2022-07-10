using UnityEngine;

public class Rotation_Handler : MonoBehaviour
{
    [SerializeField] GameObject TPCameraRig;
    bool cooldown = false;
    OVRInput.Axis2D ovrInput = OVRInput.Axis2D.PrimaryThumbstick;
    [SerializeField] bool useDebugInput = false;

    void Update()
    {
        if (TPCameraRig != null)
        {
            float input = Input.GetAxisRaw("Horizontal"); // A/D or thumbstick
            float debugInput = 0;

            if (useDebugInput)
                DoRotation(debugInput);
            else
                DoRotation(input);
        }
    }
    void DoRotation(float input)
    {
        if (input > 0 && !cooldown)
        {
            cooldown = true;
            TPCameraRig.transform.eulerAngles += new Vector3(0, 30, 0);
        }
        else if (input < 0 && !cooldown)
        {
            cooldown = true;
            TPCameraRig.transform.eulerAngles += new Vector3(0, -30, 0);
        }

        if (input == 0 && cooldown)
            cooldown = false;
    }
}
