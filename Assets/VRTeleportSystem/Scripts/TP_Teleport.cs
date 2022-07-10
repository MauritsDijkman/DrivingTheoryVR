using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace LetinkDesign.Systems.TeleportSystem
{
    [RequireComponent(typeof(TP_Core))]
    public class TP_Teleport : MonoBehaviour
    {
        TP_Core tpCore = null;

        void Awake()
        {
            tpCore = GetComponent<TP_Core>();
        }

        void Start()
        {
            tpCore.laserHitEvent += Hit;
            tpCore.triggerUpEvent += TP_Request;
        }


        /// <summary>
        /// Constantly called as long as laser is active and hitting an object.
        /// </summary>
        void Hit()
        {

        }

        /// <summary>
        /// Called when the Teleport-Axis stops being held. (Button up but for an Axis).
        /// </summary>
        void TP_Request()
        {
            //Only go through with the teleport if everything lines up.
            if (!tpCore.tooSteep && tpCore.hit.transform != null && tpCore.tpAllowed && !tpCore.tpCancelled)
                Teleport();
            else
                print("Teleport invalid. Teleport was either too steep, not allowed or cancelled manually");
        }

        /// <summary>
        /// Called by TP_Request. Transports the player by setting it's position and rotation. Also invokes tpCore's TpEvent
        /// </summary>
        void Teleport()
        {
            tpCore.TPEvent.Invoke();

            //Set the player on the hit-position.
            tpCore.playerT.position = tpCore.hit.point;
            //Vector3.up = (0, 1, 0). the 1 will get the multiplier, effectively shortening the line.                
            tpCore.playerT.eulerAngles += Vector3.up * tpCore.radialInput;
        }
    }
}
