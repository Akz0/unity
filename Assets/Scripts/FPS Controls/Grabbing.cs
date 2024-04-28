using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]

public class Grabbing : MonoBehaviour
{
    [SerializeField] float grabbingRange = 3;
    [SerializeField] float pullingRange = 20;

    [SerializeField] Transform holdpoint = null;
    [SerializeField] KeyCode grabKey = KeyCode.G;
    [SerializeField] KeyCode throwKey = KeyCode.Mouse0;

    [SerializeField] float maxThrowForce = 100f;
    [SerializeField] float maxPullForce = 100;
    [SerializeField] float maxGrabBreakingForce = 100f;
    [SerializeField] float grabBreakingTorque = 100f;

    FixedJoint grabJoint;
    Rigidbody grabbedRigidbody;

    private float mouseButtonDownTimer;

    private void Awake()
    {
        if (holdpoint == null)
        {
            Debug.LogError("Grab hold point must not be null");
        }

        if (holdpoint.IsChildOf(transform) == false)
        {
            Debug.LogError("Grab hold point must be a child of this object.");
        }

        var playerCollider = GetComponentInParent<Collider>();

        playerCollider.gameObject.layer = LayerMask.NameToLayer("Player");

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(grabKey) && grabJoint == null)
        {
            AttemptPull();
        }
        else if (Input.GetKeyDown(grabKey) && grabJoint != null)
        {
            Drop();
        }
        else if (Input.GetKeyDown(throwKey) && grabJoint != null)
        {
            mouseButtonDownTimer = Time.time;
        }
        else if (Input.GetKeyUp(throwKey) && grabJoint != null)
        {
            float totalTimePressed = Time.time - mouseButtonDownTimer;
            float forcePercentage = totalTimePressed / 2;
            float force = Mathf.Lerp(1, maxThrowForce, forcePercentage);
            Throw(force);
        }

    }


    void Throw(float f)
    {
        if (grabbedRigidbody == null)
        {
            return;
        }

        var thrownBody = grabbedRigidbody;

        var force = transform.forward * f;

        thrownBody.AddForce(force);

        Drop();

    }


    void AttemptPull()
    {
        var ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        var everythingExceptPlayers = ~(1 << LayerMask.NameToLayer("Player"));

        var layerMask = Physics.DefaultRaycastLayers & everythingExceptPlayers;

        var hitSomething = Physics.Raycast(ray, out hit, pullingRange, layerMask);

        if (hitSomething == false) return;

        grabbedRigidbody = hit.rigidbody;
        if (grabbedRigidbody == null || grabbedRigidbody.isKinematic) return;


        if (hit.distance < grabbingRange)
        {
            grabbedRigidbody.transform.position = holdpoint.position;

            grabJoint = gameObject.AddComponent<FixedJoint>();
            grabJoint.connectedBody = grabbedRigidbody;
            grabJoint.breakForce = maxGrabBreakingForce;
            grabJoint.breakTorque = grabBreakingTorque;

            foreach (var myCollider in GetComponentsInParent<Collider>())
            {
                Physics.IgnoreCollision(myCollider, hit.collider, true);
            }
        }
        else
        {
            var pull = -transform.forward * this.maxPullForce;
            grabbedRigidbody.AddForce(pull);
        }

    }


    void Drop()
    {
        if (grabJoint != null)
        {
            Destroy(grabJoint);
        }

        if (grabbedRigidbody == null) return;

        foreach (var myCollider in GetComponentsInParent<Collider>())
        {
            Physics.IgnoreCollision(myCollider, grabbedRigidbody.GetComponent<Collider>(), false);
        }

        grabbedRigidbody = null;
    }




    private void OnDrawGizmos()
    {
        if (holdpoint == null)
        {
            return;
        }

        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(holdpoint.position, 0.3f);
    }


    private void OnJointBreak(float breakForce)
    {
        Drop();
    }
}
