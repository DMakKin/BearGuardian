using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class Dragging : MonoBehaviour
{
    //    private Vector3 offset;          // �������� �� ����� ������� �� ������ �������
    //    private Camera mainCamera;       // ������, ������������ ��� �������������� ���������

    //    void Start()
    //    {
    //        mainCamera = Camera.main;    // �������� �������� ������
    //    }

    //    void OnMouseDown()
    //    {
    //        // ��������� �������� �� ����� ����� �� ������ �������
    //        offset = transform.position - GetMouseWorldPosition();       

    //    }

    //    void OnMouseDrag()
    //    {
    //        // ���������� ������ � ������ ��������
    //        transform.position = GetMouseWorldPosition() + offset;      

    //    }

    //    private Vector3 GetMouseWorldPosition()
    //    {
    //        // �������� ���������� ���� � ������� ������������
    //        Vector3 mouseScreenPosition = Input.mousePosition;
    //        mouseScreenPosition.z = mainCamera.WorldToScreenPoint(transform.position).z; // ������� ������� ������������ ������
    //        return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    //    }

    private Rigidbody rbObj;
    [SerializeField] private Transform hands;
    private Camera mainCamera;
    private bool isDragging;
    private Vector3 dragOffset;
    [SerializeField] private float dragForce = 1f; // ����������� ���� ��������������, ����� ���������
    private Vector3 dragPoint;

    void Start()
    {
        rbObj = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        // ������� � ����� ��������������
        isDragging = true;

        // �������� ���������� ����� ������� �� ������� � ������� ������������
        //dragPoint = GetMouseWorldPosition();
        dragPoint = hands.position;

        // ��������� �������� ������������ ����� �������, ����� ������ �� ������������ �����
        dragOffset = dragPoint - transform.position;

        // ��������� �������� ������� ��� ��������������
        rbObj.angularDrag = 5f;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            // ��������� ������� ����� ������� �� ������ ��������� ����
            //dragPoint = GetMouseWorldPosition() - dragOffset;
            dragPoint = hands.position - dragOffset;

            // ��������� ����������� � ����
            Vector3 forceDirection = dragPoint - transform.position;

            // ��������� ���� � ������� � ����������� �������
            rbObj.AddForce(forceDirection * dragForce, ForceMode.Acceleration);
        }
    }

    void OnMouseUp()
    {
        // ��������� ����� �������������� � ���������� ��������
        isDragging = false;
        rbObj.angularDrag = 0.05f;
    }

    //private Vector3 GetMouseWorldPosition()
    //{
    //    // ����������� �������� ���������� ���� � ������� ����������
    //    Vector3 mouseScreenPosition = Input.mousePosition;
    //    mouseScreenPosition.z = mainCamera.WorldToScreenPoint(transform.position).z;
    //    return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    //}
}
