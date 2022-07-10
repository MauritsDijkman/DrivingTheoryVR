using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LetinkDesign.Systems.TeleportSystem
{
    [RequireComponent(typeof(TP_Core))]
    /// <summary>
    /// Used to toggle the Object that visualises the line's origin (like a VR-controller or a hand)
    /// </summary>
    public class TP_LaserBase : MonoBehaviour
    {
        [SerializeField, Tooltip("Array containing the Laser-Base objects. These are probably the controllers. Using the thumbstick, these controllers will be toggled on/off")]
        GameObject[] laserBaseObjects = null;

        [Space(8)]
        [SerializeField, Tooltip("The button on the Oculus Quest controller that'll trigger the Visibility-Toggle functionality. (GetButtonDown function). \nPrimaryThumbstick is recommended.")]
        OVRInput.Button visibilityToggleButton = OVRInput.Button.PrimaryThumbstick;

        void Update()
        {
            ToggleInput();
        }

        void ToggleInput()
        {
            //Toggle to true or false based on the current state.
            if (OVRInput.GetDown(visibilityToggleButton) || Input.GetKeyDown(KeyCode.B))
            {
                foreach (GameObject g in laserBaseObjects)
                    g.SetActive(!g.activeSelf);
            }
        }
    }
}