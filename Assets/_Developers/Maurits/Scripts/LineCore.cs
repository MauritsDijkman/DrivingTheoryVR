using UnityEngine;
using System;

public class LineCore : MonoBehaviour
{
    [SerializeField] private Transform playerT = null;

    public LineRenderer laserLR = null;
    WorldUIButton currentButton = null;

    [NonSerialized] public RaycastHit hit;

    [NonSerialized] public bool laserActive = false;
    [NonSerialized] public bool tpCancelled = false;
    [NonSerialized] public bool tooSteep = false;

    private bool tpAllowed = false;

    private Transform laserT = null;

    private float radialInput = 0f;


    #region Laser events & TP events

    public delegate void LaserEvent();
    public LaserEvent laserHitEvent = null;
    public LaserEvent laserMissedEvent = null;

    public LaserEvent laserHitEvent_singleCall = null;
    public LaserEvent laserMissedEvent_singleCall = null;

    public delegate void Trigger();
    public Trigger triggerUpEvent = null;
    public Trigger triggerDownEvent = null;

    public delegate void Teleport();
    public Teleport TPEvent = null;
    public Teleport TPCancelledEvent = null;

    #endregion


    private void Awake()
    {
        laserT = laserLR.transform;
        Trigger_Down();
    }

    //Called by TP_Laser when the laser hits.
    public void Hit()
    {
        //Only do the SingleCall the first frame the laser STARTS hitting something
        if (!tpAllowed)
            laserHitEvent_singleCall.Invoke();
        tpAllowed = true;
        
        //Invoke all functions that've been added to this event.
        laserHitEvent.Invoke();
    }

    //Called by TP_Laser when the laser misses.
    public void Miss()
    {
        //Only do the SingleCall the first frame the laser STOPS hitting someting.
        if (tpAllowed)
            laserMissedEvent_singleCall?.Invoke();

        tpAllowed = false;
        
        //Invoke all functions that've been added to this event.
        laserMissedEvent.Invoke();
    }

    public void Trigger_Down()
    {
        triggerDownEvent?.Invoke();
        tpCancelled = false;
        laserActive = true;
    }

    public void Trigger_Up()
    {
        triggerUpEvent?.Invoke();

        //laserMissedEvent_singleCall();        
        laserActive = false;
    }
}
