using System;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    [SerializeField] GameObject control_Teleport = null;
    [SerializeField] GameObject control_Pointer = null;
    [SerializeField] GameObject control_Grab = null;
    [SerializeField] GameObject control_Movement = null;
    [SerializeField] GameObject control_Rotation = null;

    [NonSerialized] public bool teleportEnabled = true;

    [Flags]
    public enum PlayerControl       //Binary
    {
        Teleport = 1,               //000001
        Pointer = 2,                //000010
        Grab = 4,                   //000100
        Movement = 8,               //001000
        Rotation = 16,              //010000
    }

    public void SetPlayerControl(PlayerControl newControl)
    {
        control_Teleport.SetActive(false);
        control_Pointer.SetActive(false);
        control_Grab.SetActive(false);
        control_Movement.SetActive(false);
        control_Rotation.SetActive(false);


        if (newControl.HasFlag(PlayerControl.Teleport))
        {
            control_Teleport.SetActive(true);
        }
        if (newControl.HasFlag(PlayerControl.Pointer))
        {
            control_Pointer.SetActive(true);
        }
        if (newControl.HasFlag(PlayerControl.Grab))
        {
            control_Grab.SetActive(true);
        }
        if (newControl.HasFlag(PlayerControl.Movement))
        {
            control_Movement.SetActive(true);
        }
        if (newControl.HasFlag(PlayerControl.Rotation))
        {
            control_Rotation.SetActive(true);

        }
    }
}
