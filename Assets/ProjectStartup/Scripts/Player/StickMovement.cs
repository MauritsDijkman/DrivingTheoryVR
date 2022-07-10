using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb = null;        
    [SerializeField] Transform objectToMove = null;
    Transform forwardLooker = null;                     //Object that'll look in the same direction as the player but ignores the vertical axis.
    [SerializeField] float acceleration = 3f;
    [SerializeField] float deceleration = 3;
    [SerializeField] float maxSpeed = 1;


    OVRInput.Axis2D ovrInput = OVRInput.Axis2D.SecondaryThumbstick;
    Vector2 inputXY = new Vector2();

    void Awake()
    {
        SetForwardLooker();
    }

    void SetForwardLooker()
    {
        forwardLooker = new GameObject("ForwardLooker").transform;
        forwardLooker.parent = objectToMove;
        forwardLooker.localPosition = Vector3.zero;
        forwardLooker.localEulerAngles = Vector3.zero;
    }
    void Update()
    {
        inputXY = OVRInput.Get(ovrInput);
        //inputXY = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }



    void FixedUpdate()
    {
        //DoMove(inputXY);
        DoMove(inputXY);
    }
    public void DoMove(Vector2 _input)
    {
        forwardLooker.eulerAngles = new Vector3(0, forwardLooker.eulerAngles.y, forwardLooker.eulerAngles.z);
        //Save velocity in a new vec2 to gain more control before applying the changes.
        Vector3 velo = rb.velocity;
        velo = forwardLooker.InverseTransformVector(velo);
        //-----        

        //Apply velocity input
        //velo = ApplyInput(velo, _input);

        if (_input != Vector2.zero)
            velo = new Vector3(_input.x * acceleration, 0, _input.y * acceleration);
        else
        {
            velo = Vector3.zero;
        }


        //Make sure the player has a speed cap.
        Vector2 clampedVelo = Vector2.ClampMagnitude(new Vector2(velo.x, velo.z), maxSpeed);
        velo = new Vector3(clampedVelo.x, velo.y, clampedVelo.y);

        //-----        
        //Apply the velocity without changing the y-velocity.
        velo = forwardLooker.TransformVector(velo);
        rb.velocity = velo;
    }

    /// <summary>
    /// Used to apply input to player's velocity.
    /// Also used for deaccelerating when there's no input for either X or Y.
    /// </summary>
    /// <param name="_velo">Velocity before applying input.</param>
    /// <param name="_input">Input on which the velocity change will be based.</param>
    /// <returns></returns>
    Vector3 ApplyInput(Vector3 _velo, Vector2 _input)
    {
        //Only normalize if input exceeds the max (which would've made the player go faster).
        //This would've otherwise made joysticks cap at 0.7.
        if (_input.magnitude > 1)
            _input.Normalize();

        //HORIZONTAL INPUT        
        if (_input.x == 0)
            //No input. Deaccelerate to 0
            _velo.x = Mathf.Lerp(_velo.x, 0, deceleration * Time.fixedDeltaTime);
        else
            //Input. Accelerate.
            _velo.x += acceleration * _input.x * Time.fixedDeltaTime;

        //VERTICAL INPUT
        //If there's input, then accelerate. If not, deaccelerate.
        if (_input.y == 0)
            //No input. Deaccelerate to 0
            _velo.z = Mathf.Lerp(_velo.z, 0, deceleration * Time.fixedDeltaTime);
        else
            //Input. Accelerate.
            _velo.z += acceleration * _input.y * Time.fixedDeltaTime;

        return _velo;
    }
}
