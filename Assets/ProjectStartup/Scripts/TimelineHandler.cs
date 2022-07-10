using UnityEngine.Playables;
using UnityEngine;

public class TimelineHandler : MonoBehaviour
{
    PlayableDirector director = null;

    [SerializeField] bool debugAnimInput = false;

    [SerializeField] PlayableAsset a = null;
    [SerializeField] PlayableAsset b = null;
    [SerializeField] PlayableAsset c = null;

    void Update()
    {
        if (!debugAnimInput)
            return;

        //DEBUG
        if (Input.GetKeyDown(KeyCode.Z))
            StartTimeline("A");
        else if (Input.GetKeyDown(KeyCode.X))
            StartTimeline("B");
        else if (Input.GetKeyDown(KeyCode.C))
            StartTimeline("C");
    }

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    public void StartTimeline(string givenAnswer)
    {
        if (givenAnswer == "A")
        {
            director.playableAsset = a;
            director.Play();
        }
        else if (givenAnswer == "B")
        {
            director.playableAsset = b;
            director.Play();
        }
        else if (givenAnswer == "C")
        {
            director.playableAsset = c;
            director.Play();
        }
    }
}
