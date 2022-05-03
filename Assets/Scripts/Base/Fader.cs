using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class Fader : MonoBehaviour
{
    public LayerMask FaderMaks;
    public float FadeTime = 1f;
    public float FadeAmount = 0.25f;
    public Material FaderMat;
    // public Renderer[] renders;
    public List<Renderer> renders = new List<Renderer>();
    private void OnTriggerEnter(Collider other)
    {
        if ((FaderMaks.value & (1 << other.gameObject.layer)) > 0)
        {
            Debug.Log("Fade Starting For " + other.name);

            var renderx = other.GetComponentsInChildren<Renderer>();
            renders.Clear();
            foreach (var item in renderx)
            {
                if (item as MeshRenderer)
                {
                    var text = item.GetComponent<TextMeshPro>();
                    if (text)
                    {
                        text.DOFade(0, FadeTime);
                        continue;
                    }
                }

                renders.Add(item);
            }

            // if (renders == null) return;

            // foreach (var item in renders)
            // {
            //     foreach (var mat in item.materials)
            //     {
            //         if (!item.gameObject.activeSelf) break;
            //         // item.SetFloat("_Mode",3);

            //         // item.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            //         // item.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            //         // item.EnableKeyword("_ALPHATEST_ON");
            //         // item.renderQueue = 2000;

            //         var material = mat;
            //         var color = material.color;
            //         material = FaderMat;
            //         material.SetColor("_Color", color);

            //         mat.DOFade(FadeAmount, FadeTime);
            //     }
            // }


            for (int i = 0; i < renders.Count; i++)
            {
                renders[i].materials = Changer(renders[i].materials);
                //FaderGoBrr(renders[i].materials);
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

    private Material[] Changer(Material[] a)
    {
        for (int i = 0; i < a.Length; i++)
        {
            // // mat.SetColor("_Color", a[i].color);
            // var newMat =  new Material(FaderMat);
            // newMat.color = a[i].GetColor("Color");
            // a[i] = new Material(FaderMat);
            a[i].DOFade(FadeAmount, FadeTime);
        }

        return a;
    }

    // private void FaderGoBrr(Material[] a)
    // {
    //     for (int i = 0; i < a.Length; i++)
    //     {
    //         a[i].DOFade(FadeAmount, FadeTime);
    //     }
    // }
}


