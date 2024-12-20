using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dragging : MonoBehaviour, IInteractable
{
    private Rigidbody grabbedObjRig;
    [SerializeField] private Transform hands;
    private bool isDragging;
    private Vector3 dragOffset;
    [SerializeField] private float dragForce = 1f; // ����������� ���� ��������������, ����� ���������
    private Vector3 grabPoint;

    [SerializeField] float valueOfAngularDrag;
    //[SerializeField] private KeyCode button = KeyCode.E;

    // Start is called before the first frame update
    void Start()
    {
        grabbedObjRig = GetComponent<Rigidbody>();        
    }
   
    
    public void Interact()
   {
        Debug.Log("Catch!");
        // ������� � ����� ��������������
        isDragging = true;

        // �������� ���������� ����� ������� �� ������� � ������� ������������        
        grabPoint = hands.position;

        // ��������� �������� ������������ ����� �������, ����� ������ �� ������������ �����
        dragOffset = grabPoint - transform.position;

        //// ��������� �������� ������� ��� ��������������
        grabbedObjRig.angularDrag = valueOfAngularDrag;
        Debug.Log("isDragging" + isDragging);
   }

    

    private void Update()
    {
        if (Input.GetButtonUp("Interact"))
        {
            isDragging = false;
            grabbedObjRig.angularDrag = 0.05f;
            Debug.Log("isDragging" + isDragging);
        }
    }

    private void FixedUpdate() 
    {
        if (isDragging)
        {
            // ��������� ������� ����� ������� �� ������ ��������� ���           
            grabPoint = hands.position - dragOffset;

            // ��������� ����������� � ����
            Vector3 forceDirection = grabPoint - transform.position;

            // ��������� ���� � ������� � ����������� �������
            grabbedObjRig.AddForce(forceDirection * dragForce, ForceMode.Acceleration);


            //// ����� ������� ��� ����� ������� (� ������ ��������)
            //Vector3 targetPosition = hands.position - dragOffset;

            //// ������ ���������� ������ � ����� �������
            //grabbedObjRig.MovePosition(targetPosition);
        }
    }
}
