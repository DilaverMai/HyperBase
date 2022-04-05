using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fader : MonoBehaviour
{
    public LayerMask FaderMaks;
    public float FadeTime = 1f;
    public float FadeAmount = 0.25f;

    private void OnTriggerEnter(Collider other)
    {
        if ((FaderMaks.value & (1 << other.gameObject.layer)) > 0)
        {
            Debug.Log("Fade Starting For " + other.name);

            var render = other.GetComponent<Renderer>();
            foreach (var item in render.materials)
            {
                if (!render.gameObject.activeSelf) break;
                // item.SetFloat("_Mode",3);
        
                // item.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                // item.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                // item.EnableKeyword("_ALPHATEST_ON");
                // item.renderQueue = 2000;

    
                item.DOFade(FadeAmount, FadeTime);
            }

        }
    }

    // private void OnTriggerExit(Collider other)
    // {
    //     if ((FaderMaks.value & (1 << other.gameObject.layer)) > 0)
    //     {
    //         Debug.Log("Fade Starting For " + other.name);
    //         var render = other.GetComponent<Renderer>();
    //         foreach (var item in render.materials)
    //         {
    //             if (!render.gameObject.activeSelf) break;
    //             item.DOFade(0, FadeTime);
    //         }

    //     }
    // }
}
