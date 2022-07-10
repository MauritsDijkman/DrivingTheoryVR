using UnityEngine;
using OVR;

[RequireComponent(typeof(LineCore))]
///<summary>
///Controls the laser functionality (raycasting) as wel as dislpaying it on a LineRenderer.
///</summary>
public class Laser : MonoBehaviour
{
    [SerializeField] OVRInput.Axis1D inputButton = OVRInput.Axis1D.PrimaryHandTrigger;

    LineCore tpCore = null;

    LineRenderer laserLR = null;
    [SerializeField] private GameObject hitSphere;
    Transform laserT = null;

    Transform savedHit = null;

    [Space(8)]
    [SerializeField, Tooltip("Laser shall not be longer than this value when not hitting an object. (Hitting an object further away than this line is still possible).")]
    float maxLaserDistance = 40f;

    [Space(8)]
    [SerializeField, Tooltip("Decides what layers should or shouldn't be ignored.")]
    LayerMask tpLaserMask = 2;

    [SerializeField] private Material laserMat_allowed = null;
    [SerializeField] private Material laserMat_disallowed = null;    

    [SerializeField] private Color selectColor;
    private Color originalColor;

    private void Awake()
    {
        //Get References in Awake.
        tpCore = GetComponent<LineCore>();

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

        ActivateLaser();
    }

    void FixedUpdate()
    {
        if (!tpCore.laserActive)
            return;

        SetLaser();
        //ChangeColour();
    }

    void SetLaser()
    {
        //Set the line's origin to the object's position.
        laserLR.SetPosition(0, laserT.position);

        //Cast a ray from the player's hand forward. It'll go for a specified amount of time and ignores triggers.
        Physics.Raycast(laserT.position, laserT.forward, out RaycastHit _hit, maxLaserDistance, tpLaserMask, QueryTriggerInteraction.Ignore);

        //Save the hit for public use between TP-scripts.
        tpCore.hit = _hit;


        //HIT SOMETHING
        if (_hit.transform != null)
        {
            //The TP_Core will command all TP-scripts to do their "Laser hit" functions
            tpCore.Hit();

            //Position the laser end at the hitpoint.
            laserLR.SetPosition(1, _hit.point);
            hitSphere.transform.position = _hit.point;

            //
            if (_hit.transform != savedHit)
                savedHit = _hit.transform;
        }
        //DID NOT HIT SOMETHING
        else
        {
            hitSphere.SetActive(false);

            //The TP_Core will command all TP-scripts to do their "Laser missed" functions
            tpCore.Miss();
            TargetChanged();
        }
    }

    void TargetChanged()
    {
        if (!tpCore.tooSteep)
            laserLR.material = laserMat_allowed;
        else
            laserLR.material = laserMat_disallowed;
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

    private bool orginalColorIsSet = false;

    private void ChangeColour()
    {
        if (tpCore.hit.transform == null)
            return;

        if (tpCore.hit.transform.gameObject.layer == 5)
        {
            if (!orginalColorIsSet)
            {
                print("HIT");
                //originalColor = _hit.transform.gameObject.GetComponent<Renderer>().material.color;                
                orginalColorIsSet = true;
            }

            //_hit.transform.gameObject.GetComponent<Renderer>().material.color = selectColor;
        }
    }
}
