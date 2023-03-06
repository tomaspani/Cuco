using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineScript : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private float outlineScaleFactor;
    [SerializeField] private Color outlineColor;
    private Renderer outlineRenderer;
    public GameObject hips;

    void Start()
    {
        outlineRenderer = CreateOutline(outlineMaterial, outlineScaleFactor, outlineColor);
        //outlineRenderer.enabled = true;
    }


    Renderer CreateOutline(Material outlineMat, float scaleFactor, Color color)
    {
        GameObject outlineObject = Instantiate(this.gameObject, transform.position, transform.rotation, transform);

        if (hips)
        {
            outlineObject.transform.SetParent(hips.transform);
        }
        
        outlineObject.transform.localScale = new Vector3(1f, 1f, 1f);
        Renderer rend = outlineObject.GetComponent<Renderer>();

        rend.material = outlineMat;
        rend.material.SetColor("_OutlineColor", color);
        rend.material.SetFloat("_Scale", scaleFactor);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        outlineObject.GetComponent<OutlineScript>().enabled = false;
        if(outlineObject.GetComponent<SeeThrough>())
            outlineObject.GetComponent<SeeThrough>().enabled = false;
        outlineObject.GetComponent<Collider>().enabled = false;

        rend.enabled = false;

        return rend;
    }

    public void Disable()
    {
        outlineRenderer.enabled = false;
    }

    public void Enable()
    {
        outlineRenderer.enabled = true;
    }



    private void CoordinateAnimations()
    {
        //float animTime = firstAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;

        //outlineAnim.Play("", 0, animTime);
    }
}