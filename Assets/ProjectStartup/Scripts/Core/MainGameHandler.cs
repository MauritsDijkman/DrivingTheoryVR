using System.Collections;
using System.Collections.Generic;
using Unity.XR.Oculus;
using UnityEngine;

public class MainGameHandler : MonoBehaviour
{
    public static MainGameHandler instance = null;
    public SceneHandler sceneHandler = null;

    [HideInInspector]public int volume = 88;


    void Awake()
    {                
        //Make sure only one game handler exists
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SetVolume(int newVolume)
    {
        volume = newVolume;
    }
}