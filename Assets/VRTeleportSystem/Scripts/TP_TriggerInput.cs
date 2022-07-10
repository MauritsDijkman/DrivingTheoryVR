using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LetinkDesign.Systems.TeleportSystem
{
    [RequireComponent(typeof(TP_Core))]
    public class TP_TriggerInput : MonoBehaviour
    {
        [SerializeField, Tooltip("The trigger that manages the Teleporting-Laser. \nI'd recommend setting this to the same hand as the Laser. By default this is right.")]
        //OVRInput.RawAxis1D trigger = OVRInput.RawAxis1D.RIndexTrigger;
        //OVRInput.Axis1D trigger = OVRInput.Axis1D.SecondaryIndexTrigger;
        OVRInput.Button teleportButton = OVRInput.Button.SecondaryIndexTrigger;
        //Keep track of when the trigger is pressed.s
        bool triggerPressed;
        public delegate void Trigger();

        TP_Core tpCore = null;

        void TriggerCheck()
        {
            //Get the right input for the current platform.
            //float _input = OVRInput.Get(trigger);

            bool buttonDown = OVRInput.GetDown(teleportButton);
            bool buttonUp = OVRInput.GetUp(teleportButton);
            bool buttonGet = OVRInput.Get(teleportButton);


            //buttonDown = Input.GetKeyDown(KeyCode.C);
            //buttonDown = Input.GetKeyUp(KeyCode.C);
            //buttonDown = Input.GetKey(KeyCode.C);


            if (!triggerPressed && (buttonDown || Input.GetKeyDown(KeyCode.C)))
            {
                tpCore.Trigger_Down();
            }
            else if (!triggerPressed && (buttonUp || Input.GetKeyUp(KeyCode.C)))
            {
                tpCore.Trigger_Up();
            }




            ////If trigger wasn't pressed before but is now
            //if (!triggerPressed && _input > 0f)
            //{
            //    //Call the DOWN-event (if not null)
            //    tpCore.Trigger_Down();
            //}

            ////If trigger was pressed before but isn't now.
            //if (triggerPressed && _input == 0)
            //{
            //    //Call the UP-event
            //    tpCore.Trigger_Up();
            //}

            //If trigger was pressed and still is, return.            
            if (triggerPressed && (buttonGet || Input.GetKey(KeyCode.C)))
                return;

            triggerPressed = false;
            //while (_input > 0f && triggerPressed)
            //{
            //    return;
            //}
            //triggerPressed = false;
        }


        void Update()
        {
            TriggerCheck();
        }

        void Awake()
        {
            tpCore = GetComponent<TP_Core>();
        }
    }
}