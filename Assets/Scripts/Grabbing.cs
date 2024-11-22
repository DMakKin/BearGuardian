using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

interface IInteractable
{
    public void Interact();
}

public class Grabbing : MonoBehaviour
{
    [SerializeField] private Transform InteractorSource;
    private SphereCollider viewingRadius;
    //private MovementInput1 thisMI1;

    private PlayerController controller;

    //private Rigidbody thisRigidbody;
    private IInteractable interactObj;    
    private Rigidbody grabbedObjRig;

    //[SerializeField] private float InteractRange;
    private float denominator;
    //private float originalVelocity;
    private float originalSpeed;

    private void Start()
    {
        viewingRadius = GetComponent<SphereCollider>();
        //thisMI1 = gameObject.GetComponent<MovementInput1>();
        //originalVelocity = thisMI1.Velocity;

        controller = GetComponent<PlayerController>();  
        originalSpeed = controller.moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactObj))
        {
            Debug.Log("I see something to catch!");

            this.interactObj = interactObj;
            grabbedObjRig = other.gameObject.GetComponent<Rigidbody>(); 
        }
    }

    void OnTriggerExit(Collider other)
    {
        Relise();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") & interactObj != null)
        {

            interactObj.Interact();            
            denominator = grabbedObjRig.mass + originalSpeed;
            controller.moveSpeed = controller.moveSpeed / denominator;

            Debug.Log("Speed = " + controller.moveSpeed); 

            //Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            //if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            //{
            //    if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            //    {
            //        interactObj.Interact();

            //        grabbedObjRig = hitInfo.collider.attachedRigidbody;
            //        denominator = grabbedObjRig.mass + originalVelocity;                    

            //        thisMI1.Velocity = thisMI1.Velocity / denominator;

            //        Debug.Log("Velocity = " + thisMI1.Velocity);
            //    }
            //}

        }

        if (Input.GetButtonUp("Interact"))
        {
            //thisMI1.Velocity = originalVelocity;
            //Debug.Log("Velocity = " + thisMI1.Velocity);
            Relise();
            controller.moveSpeed = originalSpeed;
            Debug.Log("Speed = " + controller.moveSpeed);
        }

    }

    private void Relise()
    {
        Debug.Log("Nothing to catch!");
        grabbedObjRig = null;
        interactObj = null;
    }
}
