using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFade : MonoBehaviour
{
    public static CamFade instance = null;
    //Fading in = fading to black, starting transition. Static_in = transition done -> screen is now black.
    //Fading out = returning to visible state. Static_out = transition done -> normal vision.
    public enum FadeState { Fading_In, Fading_Out, None };
    FadeState currentState = FadeState.None;

    [SerializeField] Canvas canvas = null;
    [SerializeField] CanvasGroup canvasGroup = null;
    public float targetAlpha = 1;

    [Header("FadeSpeed")]
    [SerializeField] [Range(0.1f, 100f)] float fadeInSpeed = 25;
    [SerializeField] [Range(0.1f, 100f)] float fadeOutSpeed = 25;
    float fadeSpeed = 25f;


    //The 2 actions that can happen after fading in/out.    
    [HideInInspector] public Action incidentalFadeAction = null;
    [HideInInspector] public Action fadeIn_DoneAction = null;
    [HideInInspector] public Action fadeOut_DoneAction = null;

    //Fade out right after fading in?
    [Header("Fade out after fading in.")]
    [SerializeField] bool forceFadeOut = true;
    [SerializeField] [Range(0, 5f)] float delayFadeOutTime = 0.8f;



    void Start()
    {
        canvasGroup.alpha = 0;
        currentState = FadeState.None;
        fadeOut_DoneAction = DisableCanvas;
    }

    void EnableCanvas()
    {
        //if (canvas == null)
        //    canvas = canvasGroup.GetComponent<Canvas>();
        canvas.enabled = true;
    }
    void DisableCanvas()
    {
        //if (canvas == null)
        //    canvas = canvasGroup.GetComponent<Canvas>();
        canvas.enabled = false;
    }

    //Fade to black
    public void Start_FadeIn()
    {
        EnableCanvas();
        targetAlpha = 1;
        fadeSpeed = fadeInSpeed;
        currentState = FadeState.Fading_In;

        if (forceFadeOut)
            StartCoroutine(DelayedFadeOut(delayFadeOutTime));
    }
    //Fade back to camera view
    public void Start_FadeOut()
    {
        EnableCanvas();
        targetAlpha = 0;
        fadeSpeed = fadeOutSpeed;
        currentState = FadeState.Fading_Out;
    }


    void FadingTimer()
    {
        //NOT FADING
        if (currentState == FadeState.None)
            return;

        //FADING
        canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, Time.deltaTime * fadeSpeed);
        if (Mathf.Abs(canvasGroup.alpha - targetAlpha) < 0.05f)
        {
            canvasGroup.alpha = targetAlpha;
            Action fadeDone = GetFadeAction(currentState);
            currentState = FadeState.None;
            fadeDone?.Invoke();
        }
    }

    //Return proper fading action, after fading in or out.
    Action GetFadeAction(FadeState myState)
    {
        if (myState == FadeState.Fading_In)
        {
            {
                incidentalFadeAction?.Invoke();
                incidentalFadeAction = null;
                return fadeIn_DoneAction;

            }
        }

        return fadeOut_DoneAction;
    }

    //Delay the fade-out. Action has already been called.
    IEnumerator DelayedFadeOut(float time)
    {
        yield return new WaitForSeconds(time);
        Start_FadeOut();
    }

    void Update()
    {
        FadingTimer();


        //DEBUGGING 
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Start_FadeIn();
            fadeIn_DoneAction += DoSceneLoading;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Start_FadeIn();
            fadeIn_DoneAction += DoSceneLoading;
        }
    }

    //DEBUG. Loads between scene 1 or 2.
    public void DoSceneLoading()
    {
        int index = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        if (index == 0)
            MainGameHandler.instance.sceneHandler.LoadScene(1);
        else if (index == 1)
            MainGameHandler.instance.sceneHandler.LoadScene(0);
    }
}