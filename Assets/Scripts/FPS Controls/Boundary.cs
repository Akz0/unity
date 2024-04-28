using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    [SerializeField]
    public Area area;
    private void OnTriggerEnter(Collider other)
    {
        Debug.LogFormat("{0} on triggered the function and is on the layer {1} and is on the area {2}", other.gameObject.name, other.gameObject.layer.ToString(), area.ToString());
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.Instance.SetShootingArea(area);
        };
    }
}
