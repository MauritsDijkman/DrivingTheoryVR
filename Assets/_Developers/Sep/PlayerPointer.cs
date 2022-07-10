using UnityEngine;

public class PlayerPointer : MonoBehaviour
{
    //INPUT
    [SerializeField] OVRInput.Axis1D inputButton = OVRInput.Axis1D.SecondaryIndexTrigger;

    //Laser & end    
    Transform laserT = null;
    [SerializeField] LineRenderer laserLR = null;

    [SerializeField] GameObject hitSphere;

    RaycastHit currentHit = new RaycastHit();
    WorldUIButton currentButton = null;
    float maxLaserDistance = 40f;
    [SerializeField] LayerMask tpLaserMask = 2;

    [Space(8)]
    [SerializeField] private Color selectColor;
    private Color originalColor;


    void Awake()
    {
        laserT = laserLR.transform;
    }
    void Start()
    {
        laserLR.useWorldSpace = true;
        ActivateLaser();
    }

    private void FixedUpdate()
    {
        //Set the line's origin to the object's position.
        laserLR.SetPosition(0, laserT.position);
        //Cast a ray from the player's hand forward. It'll go for a specified amount of time and ignores triggers.
        Physics.Raycast(laserT.position, laserT.forward, out RaycastHit _hit, maxLaserDistance, tpLaserMask, QueryTriggerInteraction.Ignore);

        //HIT
        if (_hit.transform != null)
        {
            hitSphere.SetActive(true);

            //If New Target
            if (_hit.transform != currentHit.transform)
            {
                currentButton?.OnHoverExit();
                currentHit = _hit;
                currentButton = null;
            }
            //Same target
            else
            {
                //If the new hit is a button, do HoverEnter.
                WorldUIButton newButton = _hit.transform.GetComponent<WorldUIButton>();
                if (newButton != null)
                {
                    currentButton = newButton;
                    currentButton?.OnHoverEnter();
                }
            }


            //Set the laser's endpoint.
            laserLR.SetPosition(1, _hit.point);
            hitSphere.transform.position = _hit.point;
        }
        //NO HIT
        else
        {
            hitSphere.SetActive(false);

            LaserMissed();
            currentButton?.OnHoverExit();
            currentButton = null;
        }
    }

    private void Update()
    {

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.G))
        {
            currentButton?.OnPressed();
            return;
        }
#endif

        //if (OVRInput.Get(inputButton) > 0 || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            currentButton?.OnPressed();
        }
    }


    //Constantly called when the laser doesn't hit anything (within the layermask)
    private void LaserMissed()
    {
        //Start from the laser's origin and move the maximum distance forward: that's where the line should end.
        Vector3 _laserEnd = laserT.position + (laserT.forward * maxLaserDistance);
        laserLR.SetPosition(1, _laserEnd);
    }

    public void ActivateLaser()
    {
        laserLR.enabled = true;
    }

    public void DisableLaser()
    {
        laserLR.enabled = false;
        hitSphere.SetActive(false);
    }
}
