using UnityEngine;

//[RequireComponent(typeof(Interactable))]
public class Interactable_TPPlayer : MonoBehaviour
{
    private Interactable interactable = null;
    [SerializeField] private Transform tpPos = null;
    private Transform player = null;
    private CamFade camfade = null;

    private void Start()
    {
        player = FindObjectOfType<PlayerCore>().transform;
        interactable = GetComponent<Interactable>();
        camfade = interactable.camfade;
    }

    public void DoTeleport()
    {
        camfade.incidentalFadeAction += TeleportToTPPos;
        camfade.Start_FadeIn();
    }

    public void TeleportToTPPos()
    {
        player.position = tpPos.position;
        player.rotation = tpPos.rotation;
    }
}
