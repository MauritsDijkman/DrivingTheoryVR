using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LetinkDesign.Systems.TeleportSystem
{
    [RequireComponent(typeof(TP_Core))]
    public class TP_RotationInput : MonoBehaviour
    {
        TP_Core tpCore = null;
        [SerializeField, Tooltip("The Axis/Input that determines the additional rotation when teleporting (the arrow). \nUsing only the PrimaryThumbstick is recommended.")]
        //OVRInput.Axis2D stickInput = OVRInput.Axis2D.PrimaryThumbstick;
        OVRInput.Axis2D stickInput = OVRInput.Axis2D.SecondaryThumbstick;

        void Awake()
        {
            tpCore = GetComponent<TP_Core>();
            //if(gameObject.activeSelf == false)
                
        }

        void Update()
        {
            GetInput();
        }

        void GetInput()
        {
            //Save the Horizontal and Vertical inputs.
            Vector2 _inputs = new Vector2(OVRInput.Get(stickInput).x, OVRInput.Get(stickInput).y);

            //When in a build, use  the Oculus Quest inputs, when in Editor, use the regular Axis-inputs (for debugging)
#if UNITY_EDITOR
            _inputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
#endif
            //When no input is given, keep the current orientation (...by not updating the input.)
            if (_inputs == Vector2.zero)
                return;

            //Turn the 2 normalized values into a radial value, making 360 the greatest value.
            float _radInput = FindDegree(_inputs.x, _inputs.y);
            tpCore.radialInput = _radInput;
        }


        /// <summary>
        /// Turn 2 normalized values into 1 radial value.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public float FindDegree(float x, float y)
        {
            //Get the degree of the 2 values and make sure it's > 0.
            float value = (float)((Mathf.Atan2(x, y) / Math.PI) * 180f);
            if (value < 0)
                value += 360f;

            return value;
        }
    }
}
