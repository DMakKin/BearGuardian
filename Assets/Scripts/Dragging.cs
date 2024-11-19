using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class Dragging : MonoBehaviour
{
    //    private Vector3 offset;          // Смещение от точки захвата до центра объекта
    //    private Camera mainCamera;       // Камера, используемая для преобразования координат

    //    void Start()
    //    {
    //        mainCamera = Camera.main;    // Получаем основную камеру
    //    }

    //    void OnMouseDown()
    //    {
    //        // Вычисляем смещение от точки клика до центра объекта
    //        offset = transform.position - GetMouseWorldPosition();       

    //    }

    //    void OnMouseDrag()
    //    {
    //        // Перемещаем объект с учетом смещения
    //        transform.position = GetMouseWorldPosition() + offset;      

    //    }

    //    private Vector3 GetMouseWorldPosition()
    //    {
    //        // Получаем координаты мыши в мировом пространстве
    //        Vector3 mouseScreenPosition = Input.mousePosition;
    //        mouseScreenPosition.z = mainCamera.WorldToScreenPoint(transform.position).z; // Глубина объекта относительно камеры
    //        return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    //    }

    private Rigidbody rbObj;
    [SerializeField] private Transform hands;
    private Camera mainCamera;
    private bool isDragging;
    private Vector3 dragOffset;
    [SerializeField] private float dragForce = 1f; // Коэффициент силы перетаскивания, можно настроить
    private Vector3 dragPoint;

    void Start()
    {
        rbObj = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        // Переход в режим перетаскивания
        isDragging = true;

        // Получаем координаты точки захвата на объекте в мировом пространстве
        //dragPoint = GetMouseWorldPosition();
        dragPoint = hands.position;

        // Вычисляем смещение относительно точки захвата, чтобы тянуть за определенную точку
        dragOffset = dragPoint - transform.position;

        // Отключаем вращение объекта при перетаскивании
        rbObj.angularDrag = 5f;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            // Обновляем позицию точки захвата на основе положения мыши
            //dragPoint = GetMouseWorldPosition() - dragOffset;
            dragPoint = hands.position - dragOffset;

            // Вычисляем направление и силу
            Vector3 forceDirection = dragPoint - transform.position;

            // Применяем силу к объекту в направлении захвата
            rbObj.AddForce(forceDirection * dragForce, ForceMode.Acceleration);
        }
    }

    void OnMouseUp()
    {
        // Отключаем режим перетаскивания и сбрасываем свойства
        isDragging = false;
        rbObj.angularDrag = 0.05f;
    }

    //private Vector3 GetMouseWorldPosition()
    //{
    //    // Преобразуем экранные координаты мыши в мировые координаты
    //    Vector3 mouseScreenPosition = Input.mousePosition;
    //    mouseScreenPosition.z = mainCamera.WorldToScreenPoint(transform.position).z;
    //    return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    //}
}
