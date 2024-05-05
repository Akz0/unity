using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    [SerializeField] Color fromColor = Color.white;
    [SerializeField] Color toColor = Color.red;

    [SerializeField] float speed = 1f;

    new Renderer renderer;


    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float t = Mathf.Sin(Time.time * speed);

        t += 1;
        t /= 2;
        var newColor = Color.Lerp(fromColor, toColor, t);
        renderer.material.color = newColor;
    }
}
