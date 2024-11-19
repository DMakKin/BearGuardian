using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractableObject : MonoBehaviour, IInteractable
{
    private Rigidbody grabbedObjRig;
    [SerializeField] private Transform hands;
    private bool isDragging;
    private Vector3 dragOffset;
    [SerializeField] private float dragForce = 1f; // Коэффициент силы перетаскивания, можно настроить
    private Vector3 grabPoint;
    //[SerializeField] private KeyCode button = KeyCode.E;

    // Start is called before the first frame update
    void Start()
    {
        grabbedObjRig = GetComponent<Rigidbody>();        
    }
   
    
    public void Interact()
   {
        Debug.Log("Catch!");
        // Переход в режим перетаскивания
        isDragging = true;

        // Получаем координаты точки захвата на объекте в мировом пространстве        
        grabPoint = hands.position;

        // Вычисляем смещение относительно точки захвата, чтобы тянуть за определенную точку
        dragOffset = grabPoint - transform.position;

        //// Отключаем вращение объекта при перетаскивании
        grabbedObjRig.angularDrag = 5f;
        Debug.Log("isDragging" + isDragging);
   }

    

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
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
            // Обновляем позицию точки захвата на основе положения рук           
            grabPoint = hands.position - dragOffset;

            // Вычисляем направление и силу
            Vector3 forceDirection = grabPoint - transform.position;

            // Применяем силу к объекту в направлении захвата
            grabbedObjRig.AddForce(forceDirection * dragForce, ForceMode.Acceleration);


            //// Новая позиция для точки захвата (с учетом смещения)
            //Vector3 targetPosition = hands.position - dragOffset;

            //// Жестко перемещаем объект к точке захвата
            //grabbedObjRig.MovePosition(targetPosition);
        }
    }
}
