using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LetinkDesign.Systems.TeleportSystem
{
    [RequireComponent(typeof(TP_Core))]
    ///<summary>
    ///Controls the laser functionality (raycasting) as wel as dislpaying it on a LineRenderer.
    ///</summary>
    public class TP_Laser : MonoBehaviour
    {
        TP_Core tpCore = null;

        LineRenderer laserLR = null;
        Transform laserT = null;

        Transform savedHit = null;

        [Space(8)]
        [Header("Laser Materials")]
        [SerializeField, Tooltip("The material that shows the user that TP'ing is allowed with the current conditions. \nThis'll most likely be a blue or green color.")]
        Material laserMat_allowed = null;
        [SerializeField, Tooltip("The material that shows the user that TP'ing is NOT allowed with the current conditions. \nThis'll most likely be a red color.")]
        Material laserMat_disallowed = null;

        [Space(8)]
        [SerializeField, Tooltip("Laser shall not be longer than this value when not hitting an object. (Hitting an object further away than this line is still possible).")]
        float maxLaserDistance = 40f;

        [Space(8)]
        [SerializeField, Tooltip("Decides what layers should or shouldn't be ignored.")]
        LayerMask tpLaserMask = 2;

        [Space(8)]
        [SerializeField, Tooltip("When pointing, press this/these button(s) to cancel the Teleport-Laser. \nButton One AND Button Two is recommended.")]
        OVRInput.Button laserCancelButton = OVRInput.Button.One | OVRInput.Button.Two;


        void Awake()
        {
            //Get References in Awake.
            tpCore = GetComponent<TP_Core>();

            laserLR = tpCore.laserLR;
            laserT = laserLR.transform;
        }
        void Start()
        {
            //Make sure the laser is in World-Space. This makes setting the positions easier.
            laserLR.useWorldSpace = true;

            //Add functions to tpCore's events.
            tpCore.laserHitEvent += LaserHit;
            tpCore.laserMissedEvent += LaserMissed;
            tpCore.laserHitEvent_singleCall += LaserHit_Single;
            tpCore.laserMissedEvent_singleCall += LaserMissed_Single;

            tpCore.triggerDownEvent += ActivateLaser;
            tpCore.triggerUpEvent += DisableLaser;

            tpCore.TPCancelledEvent += DisableLaser;

            DisableLaser();
        }

        void Update()
        {
            if (!tpCore.laserActive)
                return;

            SetLaser();
            CheckLaserCancel();
        }

        void SetLaser()
        {
            //Set the line's origin to the object's position.
            laserLR.SetPosition(0, laserT.position);

            //Cast a ray from the player's hand forward. It'll go for a specified amount of time and ignores triggers.
            RaycastHit _hit;
            Physics.Raycast(laserT.position, laserT.forward, out _hit, maxLaserDistance, tpLaserMask, QueryTriggerInteraction.Ignore);
            //Debug.DrawRay(laserT.position, laserT.forward * maxLaserDistance, Color.magenta);

            //Save the hit for public use between TP-scripts.
            tpCore.hit = _hit;


            //HIT SOMETHING
            if (_hit.transform != null)
            {
                //The TP_Core will command all TP-scripts to do their "Laser hit" functions
                tpCore.Hit();

                if (_hit.transform != savedHit)
                {
                    savedHit = _hit.transform;
                    TargetChanged();
                }
            }
            //DID NOT HIT SOMETHING
            else
            {
                //The TP_Core will command all TP-scripts to do their "Laser missed" functions
                tpCore.Miss();
            }
        }

        void CheckLaserCancel()
        {
            if (OVRInput.Get(laserCancelButton) || Input.GetKeyDown(KeyCode.M))
            {
                tpCore.tpCancelled = true;
                tpCore.TPCancelledEvent.Invoke();
            }
        }

        /// <summary>
        /// Called when laser STARTS hitting something (within the layermask).
        /// </summary>
        void LaserHit_Single()
        {
            laserLR.material = laserMat_allowed;
        }

        /// <summary>
        /// Constantly called when the laser hits something (within the layermask)
        /// </summary>
        void LaserHit()
        {
            //Set the line's end to the hit object.
            laserLR.SetPosition(1, tpCore.hit.point);

            if (tpCore.tooSteep)
                laserLR.material = laserMat_disallowed;
        }

        /// <summary>
        /// Called when laser STARTS missing after hitting something (within the layermask).
        /// </summary>
        void LaserMissed_Single()
        {
            laserLR.material = laserMat_disallowed;
        }

        //Constantly called when the laser doesn't hit anything (within the layermask)
        void LaserMissed()
        {
            //Start from the laser's origin and move the maximum distance forward: that's where the line should end.
            Vector3 _laserEnd = laserT.position + (laserT.forward * maxLaserDistance);
            laserLR.SetPosition(1, _laserEnd);
        }

        //Gets called when, while still hitting something, the laser hits a different target.
        void TargetChanged()
        {
            if (!tpCore.tooSteep)
                laserLR.material = laserMat_allowed;
            else
                laserLR.material = laserMat_disallowed;
        }


        public void ActivateLaser()
        {
            tpCore.laserActive = true;
            laserLR.enabled = true;
        }
        public void DisableLaser()
        {
            tpCore.laserActive = false;
            laserLR.enabled = false;
        }



        #region Why isn't laserLR material changed in Update?
        //1. Because constantly changing materials (even if you change it to the same value) isn't very efficient (afaik),
        //2. This gives reason to code better, as there are now more options to expand the script.
        #endregion
    }
}