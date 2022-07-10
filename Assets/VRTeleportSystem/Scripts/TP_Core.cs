using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LetinkDesign.Systems.TeleportSystem
{
    /// <summary>
    /// This class is what holds the Teleport (TP) components together.
    /// </summary>
    public class TP_Core : MonoBehaviour
    {
        [Tooltip("The Player's Transform component. \nWhen using VR, this should be the root-object of the Camera-Rig (move this object and the player moves)")]
        public Transform playerT = null;

        [HideInInspector]
        public bool laserActive = false;
        [Tooltip("The LineRenderer component used to visualise the raycast. \nThis should be on either hand/controller's child-object")]
        public LineRenderer laserLR = null;
        [HideInInspector]
        public Transform laserT = null;


        [HideInInspector]
        public RaycastHit hit;
        [HideInInspector]
        public bool tpAllowed = false;
        [HideInInspector]
        public bool tpCancelled = false;


        [Range(0, 1), Tooltip("Decides how steep a slope can be before it's too steep. Between 0.97f and 1 is recommended.")]
        public float maxSlopeSteepness = 0.98f;

        [HideInInspector]
        public bool tooSteep = false;

        [HideInInspector]
        public float radialInput = 0f;

        #region Laser events & TP events
        public delegate void LaserEvent();
        public LaserEvent laserHitEvent = null;
        public LaserEvent laserMissedEvent = null;

        public LaserEvent laserHitEvent_singleCall = null;
        public LaserEvent laserMissedEvent_singleCall = null;

        public delegate void Trigger();
        public Trigger triggerUpEvent = null;
        public Trigger triggerDownEvent = null;


        public delegate void Teleport();
        public Teleport TPEvent = null;
        public Teleport TPCancelledEvent = null;

        #endregion


        void Awake()
        {
            laserT = laserLR.transform;
        }

        //Called by TP_Laser when the laser hits.
        public void Hit()
        {
            //Only do the SingleCall the first frame the laser STARTS hitting something
            if (!tpAllowed)
                laserHitEvent_singleCall.Invoke();

            tpAllowed = true;
            //Invoke all functions that've been added to this event.
            laserHitEvent.Invoke();
        }
        //Called by TP_Laser when the laser misses.
        public void Miss()
        {
            //Only do the SingleCall the first frame the laser STOPS hitting someting.
            if (tpAllowed)
                laserMissedEvent_singleCall.Invoke();

            tpAllowed = false;
            //Invoke all functions that've been added to this event.
            laserMissedEvent.Invoke();
        }



        public void Trigger_Down()
        {
            triggerDownEvent.Invoke();
            tpCancelled = false;
            laserActive = true;
        }
        public void Trigger_Up()
        {
            triggerUpEvent.Invoke();

            //laserMissedEvent_singleCall();        
            laserActive = false;
        }
    }
}