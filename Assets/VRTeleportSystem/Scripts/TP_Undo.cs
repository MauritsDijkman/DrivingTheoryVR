using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LetinkDesign.Systems.TeleportSystem
{
    [RequireComponent(typeof(TP_Core))]
    public class TP_Undo : MonoBehaviour
    {
        TP_Core tpCore = null;
        Transform playerT = null;

        //Positions/Eulers will be saved in the list and taken out when Undo is requested.
        List<Vector3> allPositions = new List<Vector3>();
        List<Vector3> allEulers = new List<Vector3>();

        [SerializeField, Tooltip("If this button is pressed while the laser is inactive, it'll undo the last Teleport. \nMaking this the same as Laser-Cancel button is recommended.(Otherwise I would recommend One AND Two).")]
        OVRInput.Button undoButton = OVRInput.Button.One | OVRInput.Button.Two;


        public void AddToUndoLists()
        {
            allPositions.Add(playerT.position);
            allEulers.Add(playerT.eulerAngles);
        }

        void Start()
        {
            tpCore.TPEvent += AddToUndoLists;
        }

        void Awake()
        {
            tpCore = GetComponent<TP_Core>();
            playerT = tpCore.playerT;
        }
        void Update()
        {
            UndoInput();
        }

        /// <summary>
        /// Check if the UndoButton gets pressed and if it's allowed to Undo now.
        /// </summary>
        void UndoInput()
        {
            //Undo not allowed when laser is active atm.
            if (tpCore.laserActive)
                return;

            //When user lets-go of the button, do Undo.
            if (OVRInput.GetDown(undoButton) || Input.GetKeyDown(KeyCode.U))
            {
                //Can't go below 0.
                if (allPositions.Count < 1)
                    return;

                UndoTP();
            }
        }

        /// <summary>
        /// Called when UndoButton is pressed & conditions are met. Will undo the teleport, reposition the player to it's previous position and rotation.
        /// </summary>
        void UndoTP()
        {
            //Reset the player back to his previous position and euler.
            int _lastIndex = allPositions.Count - 1;
            playerT.position = allPositions[_lastIndex];
            playerT.eulerAngles = allEulers[_lastIndex];

            //These values aren't needed in their list anymore. Bye bye!
            allPositions.RemoveAt(_lastIndex);
            allEulers.RemoveAt(_lastIndex);
        }
    }
}