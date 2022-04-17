using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ParticleCamera : MonoBehaviour
{
    public Transform confetti;
    public static Action<CameraParticle> PlayCameraParticle;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        PlayCameraParticle += PlayParticle;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        PlayCameraParticle -= PlayParticle;
    }


    public void PlayParticle(CameraParticle particle)
    {
        switch (particle)
        {
            case CameraParticle.Confetti:
                for (int i = 0; i < confetti.childCount; i++)
                {
                    confetti.GetChild(i).GetComponent<ParticleSystem>().Play();
                }
                
                break;

            default:
                break;
        }

    }




}
