using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform grabPoint; // Точка, где игрок держит объект
    [SerializeField] private float grabDistance;
    private GameObject heldObject;


    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            Debug.Log("Left shift down");
            if (heldObject == null)
            {
                TryGrab();
            }
            else
            {
                Release();
            }
        }
    }

    void TryGrab()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, grabDistance))
        {
            Debug.Log("Hit something");
            if (hit.collider.CompareTag("Grabbable"))
            {
                Debug.Log("Grabbed!");
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                heldObject.transform.position = grabPoint.position;
                heldObject.transform.SetParent(grabPoint);
            }
        }
    }

    void Release()
    {
        if (heldObject != null)
        {
            heldObject.GetComponent<Rigidbody>().isKinematic = false;
            heldObject.transform.SetParent(null);
            heldObject = null;
        }
    }
}
