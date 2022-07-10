using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LetinkDesign.Systems.TeleportSystem
{
    [RequireComponent(typeof(TP_Core))]
    [RequireComponent(typeof(TP_Laser))]
    public class TP_TargetPlacer : MonoBehaviour
    {
        TP_Core tpCore = null;
        [SerializeField, Tooltip("This object shows up at the target-position when TP'ing is allowed. \nMake sure it's aimed forward so that the rotation is visible.")]
        GameObject tpAllowedT = null;
        [SerializeField, Tooltip("This object shows up at the target-position when TP'ing is NOT allowed. \nMake sure it's aimed forward so that the rotation is visible.")]
        GameObject tpDisallowedT = null;


        void Awake()
        {
            tpCore = GetComponent<TP_Core>();

            //tpCore.laserHitEvent_singleCall += Hit_Single;
            tpCore.laserHitEvent += Hit;
            tpCore.laserMissedEvent_singleCall += Missed_Single;
            tpCore.TPEvent += DisableTargets;
            tpCore.TPCancelledEvent += DisableTargets;
            tpCore.triggerUpEvent += DisableTargets;
        }

        //void Hit_Single()
        //{                

        //}

        void Hit()
        {
            Vector3 _hitNormal = tpCore.hit.normal;

            //SLOPE IS NOT TOO STEEP
            if (_hitNormal.y > tpCore.maxSlopeSteepness)
            {
                tpCore.tooSteep = false;

                //Set the position on the target point.
                tpAllowedT.transform.position = tpCore.hit.point;

                //Align the object with the laser and set the Y-rotation based on the radialInput.
                Vector3 _newRot = Quaternion.LookRotation(tpCore.laserT.forward, Vector3.up).eulerAngles;
                _newRot = new Vector3(0, _newRot.y + tpCore.radialInput, 0);

                tpAllowedT.transform.eulerAngles = _newRot;

            }
            //SLOPE IS TOO STEEP
            else
            {
                tpCore.tooSteep = true;

                //Set the position on the target point.
                tpDisallowedT.transform.position = tpCore.hit.point;

                //Set the rotation based on the hit's normal.
                Vector3 _newRot = _hitNormal;
                tpDisallowedT.transform.rotation = Quaternion.LookRotation(Vector3.forward, _hitNormal);

                Debug.DrawRay(tpCore.hit.point, _hitNormal * 50, Color.magenta);
            }

            //Enable/Disable based on if the steepness is too high/low.
            tpAllowedT.SetActive(!tpCore.tooSteep);
            tpDisallowedT.SetActive(tpCore.tooSteep);
        }


        void Missed_Single()
        {
            DisableTargets();
        }

        void DisableTargets()
        {
            tpAllowedT.gameObject.SetActive(false);
            tpDisallowedT.gameObject.SetActive(false);
        }
    }
}