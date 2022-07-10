using UnityEngine;
using OVRTouchSample;

public class OutlineToggle : MonoBehaviour
{
    //[SerializeField] private KeyCode toggleButton = KeyCode.P;

    private Outline outlineScript;
    private bool outlineEnabled = true;

    [SerializeField] private OVRInput.Button visibilityToggleButton = OVRInput.Button.Three;

    private void Awake()
    {
        outlineScript = this.GetComponent<Outline>();

        if (outlineScript == null)
            throw new System.Exception("No Outline script found on GameObject where OutlineToggle script is located.");
    }

    private void Update()
    {
        if (outlineScript != null)
        {
            HandleOutline();

            /// if (Input.GetKeyDown(toggleButton))
            if (OVRInput.GetDown(visibilityToggleButton))
            {
                if (outlineEnabled)
                    outlineEnabled = false;
                else if (!outlineEnabled)
                    outlineEnabled = true;
            }
        }
    }

    private void HandleOutline()
    {
        if (outlineEnabled)
            outlineScript.enabled = true;
        else if (!outlineEnabled)
            outlineScript.enabled = false;
    }
}
