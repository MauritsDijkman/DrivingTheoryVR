using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [HideInInspector] public CamFade camfade = null;
    [HideInInspector] public bool selected = false;

    [Header("HIGHLIGHTING")]
    [SerializeField] UnityEvent onTriggerEnter = null;
    [SerializeField] UnityEvent onTriggerExit = null;

    [Header("FUNCTION")]
    [SerializeField] UnityEvent onPress = null;
    [SerializeField] UnityEvent onGrab = null;

    private void Awake()
    {
        //Ensure the gameobject is on the "interactable" layer.
        gameObject.layer = 7;
        camfade = FindObjectOfType<CamFade>();
    }

    public void ONTRIGGERENTER()
    {
        selected = true;
        onTriggerEnter?.Invoke();
    }

    public void ONTRIGGEREXIT()
    {
        selected = false;
        onTriggerExit?.Invoke();
    }

    public void ONPRESS()
    {
        onPress?.Invoke();
    }

    public void ONGRAB()
    {
        onGrab?.Invoke();
    }
}
