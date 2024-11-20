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
    private CapsuleCollider viewingRadius;
    private MovementInput1 thisMI1;

    //private Rigidbody thisRigidbody;
    private IInteractable interactObj;    
    private Rigidbody grabbedObjRig;

    [SerializeField] private float InteractRange;
    private float denominator;
    private float originalVelocity;

    private void Start()
    {
        viewingRadius = GetComponent<CapsuleCollider>();
        thisMI1 = gameObject.GetComponent<MovementInput1>();
        originalVelocity = thisMI1.Velocity;
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
        Debug.Log("Nothing to catch!");
        grabbedObjRig = null;
        interactObj = null; 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) & interactObj != null)
        {

            interactObj.Interact();            
            denominator = grabbedObjRig.mass + originalVelocity;
            thisMI1.Velocity = thisMI1.Velocity / denominator;

            Debug.Log("Velocity = " + thisMI1.Velocity); 

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

        if (Input.GetKeyUp(KeyCode.Space))
        {
            thisMI1.Velocity = originalVelocity;
            Debug.Log("Velocity = " + thisMI1.Velocity);
        }
       
    }
}
