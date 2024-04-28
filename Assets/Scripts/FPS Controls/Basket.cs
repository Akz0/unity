using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Basket : MonoBehaviour
{

    Material basketMaterial;
    private void Awake()
    {
        basketMaterial = GetComponent<Renderer>().material;
    }
    bool touch = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ball") && touch == false)
        {
            touch = true;
            basketMaterial.SetColor(Shader.PropertyToID("_Color"), Color.green);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ball") && touch == true)
        {
            touch = false;
            GameManager.Instance.UpdateScore();
        }
        basketMaterial.SetColor(Shader.PropertyToID("_Color"), Color.red);
    }


}
