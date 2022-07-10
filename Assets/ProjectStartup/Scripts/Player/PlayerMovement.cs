using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject player = null;    
    bool hasCooldown = false;

    [SerializeField]float moveDistance = 3f;


    [Space(8)]
    [SerializeField] LayerMask lmask = 0;
    [SerializeField] float playerHeight = 2f;
    [SerializeField, Tooltip("Player won't be able to walk down drops bigger than this. (like a deep cliff)")] float maxDropHeight = 0.3f;
    [SerializeField, Tooltip("Player won't be able to walk up a step bigger than this. (like a high wall)")] float maxStepHeight = 0.4f;

    void Update()
    {
        if (player == null)
            return;

        Vector3 playerHeadPos = player.transform.position + new Vector3(0, playerHeight, 0);
        Vector3 dir = GetDir(false);
        Vector3 checkPos = playerHeadPos + dir;
        Debug.DrawRay(checkPos, Vector3.down * (playerHeight + maxDropHeight), Color.magenta);



        float input = Input.GetAxisRaw("Vertical"); // A/D or thumbstick 
        if (input > 0 && !hasCooldown)
        {
            hasCooldown = true;
            CheckRay(GetDir(false));
        }
        else if (input < 0 && !hasCooldown)
        {
            hasCooldown = true;
            CheckRay(GetDir(true));
        }

        if (input == 0 && hasCooldown)
            hasCooldown = false;
    }
    Vector3 GetDir(bool isBackward)
    {
       Vector3 dir = player.transform.forward * moveDistance;
        if (isBackward)
            dir = -dir;

        return dir;
    }


    void CheckRay(Vector3 dir)
    {
        //NOTE: This code will only check downwards from the destination angle, meaning the player could go through a tall wall, if the target position is not too high (behind the wall)
        //Shouldn't be too huge of an issue for this project, but something we will have to keep in mind


        bool moveAllowed = false;
        Vector3 playerHeadPos = player.transform.position + new Vector3(0, playerHeight, 0);
        Vector3 checkPos = playerHeadPos + dir;
        float maxDistance = playerHeight + maxDropHeight;

        Physics.Raycast(checkPos, Vector3.down, out RaycastHit _hit, maxDistance, lmask, QueryTriggerInteraction.Ignore);
        if(_hit.transform != null)
        {
            float length = checkPos.y - _hit.point.y;

            float maxStepPos = player.transform.position.y + maxStepHeight;

            float diff1 = playerHeadPos.y - maxStepPos;            

            if(length > diff1)
            {
                print("Collider detected: " + _hit.transform.name + "\nWill teleport onto it now.");
                player.transform.position = _hit.point;                
            }
            else
            {
                print("Too high");
            }
        }        
    }
}