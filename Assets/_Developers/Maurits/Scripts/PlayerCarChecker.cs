using System;
using UnityEngine;

public class PlayerCarChecker : MonoBehaviour
{
    [NonSerialized] public bool playerIsInCar = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            if (!playerIsInCar)
                playerIsInCar = true;

            Debug.Log("Player is in car");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (playerIsInCar)
            playerIsInCar = false;

        Debug.Log("Player is not in car");
    }
}
