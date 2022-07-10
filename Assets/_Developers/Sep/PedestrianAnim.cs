using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianAnim : MonoBehaviour
{
    Animator anim = null;
    [SerializeField]ParticleSystem hitParticles = null;

    void Awake()
    {
        anim = GetComponent<Animator>();    
    }
    public void StartWalking()
    {
        anim.SetBool("Walking", true);
    }

    public void StartIdle()
    {
        anim.SetBool("Walking", false);
    }

    public void StartLaunch()
    {
        anim.SetTrigger("Launch");
        hitParticles?.Play();
    }

    public void ResetAnim()
    {
        anim.SetTrigger("RESET");        
    }    
}