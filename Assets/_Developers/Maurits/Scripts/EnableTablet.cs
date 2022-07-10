using System;
using UnityEngine;

public class EnableTablet : MonoBehaviour
{
    [SerializeField] private GameObject tablet;

    OVRInput.Axis1D triggerL = OVRInput.Axis1D.PrimaryIndexTrigger;
    [NonSerialized] public bool enableTablet = false;

    private MovementUpdater movementUpdater;
    private PlayerCore playerCore;

    [SerializeField] private PlayerCore.PlayerControl teleportMovement = PlayerCore.PlayerControl.Teleport & PlayerCore.PlayerControl.Rotation & PlayerCore.PlayerControl.Grab;
    [SerializeField] private PlayerCore.PlayerControl stickMovement = PlayerCore.PlayerControl.Movement & PlayerCore.PlayerControl.Rotation & PlayerCore.PlayerControl.Grab;
    [SerializeField] private PlayerCore.PlayerControl noMovement = PlayerCore.PlayerControl.Rotation & PlayerCore.PlayerControl.Grab;

    private void Awake()
    {
        movementUpdater = FindObjectOfType<MovementUpdater>();
        playerCore = FindObjectOfType<PlayerCore>();
    }

    private void Update()
    {
        //if (OVRInput.Get(triggerL) > 0)
        //    enableTablet = true;
        //else
        //    enableTablet = false;

        //HandleTablet();

        if (!movementUpdater.playerIsInCar)
        {
            if (OVRInput.Get(triggerL) > 0)
                Enable_Tablet();
            else
                Disable_Tablet();
        }
    }

    private void Enable_Tablet()
    {
        if (!tablet.activeSelf)
        {
            tablet.SetActive(true);

            playerCore.SetPlayerControl(noMovement);

            //Debug.Log("Ik wordt aangeroepen!");
        }
    }

    private void Disable_Tablet()
    {
        if (tablet.activeSelf)
        {
            tablet.SetActive(false);

            //Debug.Log($"Player is in car:{movementUpdater.playerIsInCar}");

            if (PlayerPrefs.GetInt("Movement") == 0)
                playerCore.SetPlayerControl(teleportMovement);
            else if (PlayerPrefs.GetInt("Movement") == 1)
                playerCore.SetPlayerControl(stickMovement);
        }
    }

    //private void HandleTablet()
    //{
    //    if (enableTablet && !tablet.activeSelf)
    //    {
    //        tablet.SetActive(true);
    //        movementUpdater.EnterCar();
    //    }
    //    else if (!enableTablet && tablet.activeSelf)
    //    {
    //        tablet.SetActive(false);

    //        Debug.Log(movementUpdater.playerIsInCar);

    //        if (!movementUpdater.playerIsInCar)
    //        {
    //            movementUpdater.ExitCar();
    //            Debug.Log("Movement is adjusted");
    //        }
    //    }
    //}
}
