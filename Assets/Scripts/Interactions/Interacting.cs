using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacting : MonoBehaviour
{

    [SerializeField]
    KeyCode interactionKey = KeyCode.E;

    [SerializeField]
    float interactionDistance = 1;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            AttemptInteraction();
        }
    }

    void AttemptInteraction()
    {
        var ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;

        var everythingExceptPlayers = ~(1 << LayerMask.NameToLayer("Player"));
        var layerMask = Physics.DefaultRaycastLayers & everythingExceptPlayers;

        if (Physics.Raycast(ray, out hit, interactionDistance, layerMask))
        {
            var interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.Interact(this.gameObject);
            }
        }
    }
}
