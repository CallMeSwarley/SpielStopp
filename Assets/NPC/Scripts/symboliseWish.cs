using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class symboliseWish : MonoBehaviour
{
    public NPC_master npc_master;
    public GameObject cylinder, sphere;
    [Range(0.1f, 1.0f)]
    public float fadeSpeed = 1f;

    private bool hasWish;
    private bool active;//if the mark is active
    private Material cylinder_Material, sphere_Material; 
    private Color cylinder_Color;

    // Start is called before the first frame update
    void Start()
    {
        hasWish = npc_master.hasWish;
        cylinder_Material = cylinder.GetComponent<Renderer>().material;
        sphere_Material = sphere.GetComponent<Renderer>().material;
        cylinder_Color = cylinder_Material.color;
        //cylinder_Color = cylinder_Material.color;
        if (hasWish)
        {
            activation(true);
            active = true;
            StartCoroutine(alphaFade());
        }
        else
        {
            activation(false);
            active = false;
        }
    }

    //Update is called once per frame
    void Update()
    {
        hasWish = npc_master.hasWish;
        if (hasWish && !active)
        {
            activation(true);
            StartCoroutine(alphaFade());
        }
        else if (!hasWish && active)//nicht mehr anzeigen
        {
            activation(false);
            StopCoroutine(alphaFade());
        }
    }

    void activation(bool value)
    {
        active = value;
        cylinder.SetActive(value);
        sphere.SetActive(value);
    }

    // adapted from https://answers.unity.com/questions/968423/material-that-can-fade-from-opaque-to-transparent.html
    IEnumerator alphaFade()
    {
        // Alpha start value.
        float alpha = 1.0f;

        // Loop until aplha is below zero (completely invisalbe)
        while (true)
        {
            while (alpha > 0.0f)//1->0
            {
                alpha -= fadeSpeed * Time.deltaTime;
                setNewAlpha(alpha);
                yield return null;
            }
            while (alpha <= 1.0f)//0->1
            {
                alpha += fadeSpeed * Time.deltaTime;
                setNewAlpha(alpha);
                yield return null;
            }
        }
    }
    void setNewAlpha(float alpha)
    {
        Color color = new Color(cylinder_Color.r, cylinder_Color.g, cylinder_Color.b, alpha);
        sphere_Material.color = color;
        cylinder_Material.color = color;
    }
}
