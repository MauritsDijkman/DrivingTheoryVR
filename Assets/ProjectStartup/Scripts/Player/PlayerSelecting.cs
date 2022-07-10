using System;
using UnityEngine;

public class PlayerSelecting : MonoBehaviour
{
    public static PlayerSelecting instance = null;
    [SerializeField] Collider[] cols = new Collider[2];
    HandCollider[] handcols = new HandCollider[2];

    OVRInput.Button triggerL = OVRInput.Button.PrimaryIndexTrigger;
    OVRInput.Button triggerR = OVRInput.Button.SecondaryIndexTrigger;

    OVRInput.Button grabL = OVRInput.Button.PrimaryHandTrigger;
    OVRInput.Button grabR = OVRInput.Button.SecondaryHandTrigger;

    void Awake()
    {
        //Ensure the hand collider's can't interact with eachother.
        Physics.IgnoreCollision(cols[0], cols[1], true);

        //Setting up hand colliders.
        handcols[0] = cols[0].gameObject.AddComponent<HandCollider>();
        handcols[0].playerselecting = this;
        handcols[1] = cols[1].gameObject.AddComponent<HandCollider>();
        handcols[1].playerselecting = this;
    }

    private void Update()
    {

        //Check for input and call onpress in all selections.  

        if (OVRInput.GetDown(triggerL) && handcols[0].interactable != null)
            handcols[0].interactable?.ONPRESS();
        else if (OVRInput.GetDown(triggerR) && handcols[1].interactable != null)
            handcols[1].interactable?.ONPRESS();

        if (OVRInput.Get(grabL) && handcols[0].interactable != null)
            handcols[0].interactable?.ONGRAB();
        else if (OVRInput.Get(grabR) && handcols[1].interactable != null)
            handcols[1].interactable?.ONGRAB();
    }
}

public class HandCollider : MonoBehaviour
{
    public Interactable interactable = null;
    public Action handAction = null;
    [HideInInspector] public PlayerSelecting playerselecting = null;

    void OnTriggerEnter(Collider other)
    {
        interactable = other.gameObject.GetComponent<Interactable>();
        interactable?.ONTRIGGERENTER();
    }

    void OnTriggerExit(Collider other)
    {
        interactable?.ONTRIGGEREXIT();
        interactable = null;
    }
}
