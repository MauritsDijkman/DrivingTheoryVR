using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementUpdater : MonoBehaviour
{
    private PlayerCarChecker positionChecker = null;
    private EnableTablet tabletChecker = null;

    [SerializeField] private GameObject control_Teleport = null;
    [SerializeField] private GameObject control_Movement = null;

    private PlayerCore playerCore;

    [SerializeField] private PlayerCore.PlayerControl teleportMovement = PlayerCore.PlayerControl.Teleport & PlayerCore.PlayerControl.Rotation & PlayerCore.PlayerControl.Grab;
    [SerializeField] private PlayerCore.PlayerControl stickMovement = PlayerCore.PlayerControl.Movement & PlayerCore.PlayerControl.Rotation & PlayerCore.PlayerControl.Grab;
    [SerializeField] private PlayerCore.PlayerControl noMovement = PlayerCore.PlayerControl.Rotation & PlayerCore.PlayerControl.Grab;
    [SerializeField] private PlayerCore.PlayerControl pointerMovement = PlayerCore.PlayerControl.Rotation & PlayerCore.PlayerControl.Pointer;

    [NonSerialized] public bool playerIsInCar;

    private void Awake()
    {
        positionChecker = FindObjectOfType<PlayerCarChecker>();
        tabletChecker = FindObjectOfType<EnableTablet>();
        playerCore = FindObjectOfType<PlayerCore>();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            playerCore.SetPlayerControl(pointerMovement);
            //Debug.Log("Scene is main menu");
            //Debug.Log(pointerMovement);
        }
        else if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            //if (getState() == 0)
            //{
            //    if (!control_Teleport.activeSelf)
            //        control_Teleport.SetActive(true);
            //    if (control_Movement.activeSelf)
            //        control_Movement.SetActive(false);
            //}
            //if (getState() == 1)
            //{
            //    if (control_Teleport.activeSelf)
            //        control_Teleport.SetActive(false);
            //    if (!control_Movement.activeSelf)
            //        control_Movement.SetActive(true);
            //}


            //if (getState() == 0)
            //    playerCore.SetPlayerControl(teleportMovement);
            //else if (getState() == 1)
            //    playerCore.SetPlayerControl(stickMovement);

            EnterCar();
        }
    }

    //private void Update()
    //{
    //    // if (SceneManager.GetActiveScene().buildIndex != 0)
    //    //   PositionChecker();
    //}

    //private bool isChanged1 = false;
    //private bool isChanged2 = false;

    //private void PositionChecker()
    //{
    //    Debug.Log($"Player is in car: {positionChecker.playerIsInCar} || Tablet is enabled: {tabletChecker.enableTablet}");

    //    if ((positionChecker.playerIsInCar || tabletChecker.enableTablet) && !isChanged1)
    //    {
    //        playerCore.SetPlayerControl(noMovement);
    //        isChanged1 = true;
    //        isChanged2 = false;

    //        //if (control_Teleport.activeSelf)
    //        //{
    //        //    control_Teleport.SetActive(false);
    //        //    Debug.Log("Teleport is turned off");
    //        //}
    //        //else if (control_Movement.activeSelf)
    //        //{
    //        //    control_Movement.SetActive(false);
    //        //    Debug.Log("Stick is turned off");
    //        //}
    //    }
    //    else
    //    {
    //        isChanged1 = false;

    //        if (PlayerPrefs.GetInt("Movement") == 0 && !isChanged2)
    //        {
    //            //if (!control_Teleport.activeSelf)
    //            //{
    //            //    control_Teleport.SetActive(true);
    //            //    Debug.Log("Teleport is turned on");
    //            //}

    //            playerCore.SetPlayerControl(teleportMovement);
    //            isChanged2 = true;
    //        }
    //        else if (PlayerPrefs.GetInt("Movement") == 1 && !isChanged2)
    //        {
    //            //if (!control_Movement.activeSelf)
    //            //{
    //            //    control_Movement.SetActive(true);
    //            //    Debug.Log("Stick is turned on");
    //            //}

    //            playerCore.SetPlayerControl(stickMovement);
    //            isChanged2 = true;
    //        }
    //    }
    //}

    public void ExitCar()
    {
        if (PlayerPrefs.GetInt("Movement") == 0)
            playerCore.SetPlayerControl(teleportMovement);
        else if (PlayerPrefs.GetInt("Movement") == 1)
            playerCore.SetPlayerControl(stickMovement);

        playerIsInCar = false;
        Debug.Log($"Player is in car should be adjusted to false. State: {playerIsInCar}");
    }

    public void EnterCar()
    {
        playerCore.SetPlayerControl(noMovement);

        playerIsInCar = true;
        Debug.Log($"Player is in car should be adjusted to true. State: {playerIsInCar}");
    }

    //private int getState()
    //{
    //    return PlayerPrefs.GetInt("Movement", 0);
    //}
}
