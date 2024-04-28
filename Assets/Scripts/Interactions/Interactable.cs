using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    public void Interact(GameObject fromObject)
    {
        Debug.LogFormat(
            "Interaction from : {0} with : {1}", fromObject.name, this.name
        );
        bool t = this.gameObject.activeSelf;
        this.gameObject.SetActive(!t);
    }



}
